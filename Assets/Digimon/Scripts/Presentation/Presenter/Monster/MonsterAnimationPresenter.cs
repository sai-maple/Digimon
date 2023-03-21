using System;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.Monster
{
    public sealed class MonsterAnimationPresenter : IInitializable, IDisposable
    {
        private readonly MonsterAnimationEntity _animationEntity;
        private readonly MonsterAnimationView _animationView;

        private readonly CompositeDisposable _disposable = new();

        public MonsterAnimationPresenter(MonsterAnimationEntity animationEntity, MonsterAnimationView animationView)
        {
            _animationEntity = animationEntity;
            _animationView = animationView;
        }

        public void Initialize()
        {
            _animationEntity.OnReactionAsObservable()
                .Subscribe(_animationView.SetTrigger)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}