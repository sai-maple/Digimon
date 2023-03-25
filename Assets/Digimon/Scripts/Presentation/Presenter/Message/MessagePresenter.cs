using System;
using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Domain.UseCase;
using Digimon.Digimon.Scripts.Presentation.View.Message;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.Message
{
    public sealed class MessagePresenter : IInitializable, IDisposable
    {
        private readonly MessageEntity _messageEntity;
        private readonly CommandUseCase _commandUseCase;
        private readonly MessageView _messageView;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cancellation = new();

        public MessagePresenter(MessageEntity messageEntity, CommandUseCase commandUseCase, MessageView messageView)
        {
            _messageEntity = messageEntity;
            _commandUseCase = commandUseCase;
            _messageView = messageView;
        }

        public void Initialize()
        {
            _messageView.Initialize();
            _messageEntity.OnReadFileAsObservable()
                .Subscribe(reader => MessageAsync(reader).Forget());
        }

        private async UniTaskVoid MessageAsync(StringReader reader)
        {
            await _messageView.PresentAsync(_cancellation.Token);
            while (reader.Peek() != -1)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line)) continue;
                var messageAndCommand = line.Split(',');
                await _messageView.MessageAsync(messageAndCommand[0], _cancellation.Token);
                if (_cancellation.IsCancellationRequested) return;
                _commandUseCase.InvokeCommand(messageAndCommand.Skip(1).ToArray());
            }

            _messageView.DismissAsync(_cancellation.Token).Forget();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}