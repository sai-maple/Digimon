using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using EventType = UnityEngine.EventType;
using Random = UnityEngine.Random;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class MessageEntity : IDisposable
    {
        private readonly Subject<StringReader> _message = new();

        public IObservable<StringReader> OnReadFileAsObservable()
        {
            return _message.Share();
        }

        public void Training(TrainingType trainingType, bool isClash)
        {
            var isSpecial = Random.Range(0, 100) < 10 ? "Sp" : "";

            var fileName = $"Events/Training/Training{trainingType}{isSpecial}";
            fileName = isClash ? "Training/Clash" : fileName;

            ToEvent(fileName).Forget();
        }

        public void RandomEvent(EventType eventType)
        {
            var textAssets = Resources.LoadAll<TextAsset>($"{eventType}");

            var textAsset = textAssets[Random.Range(0, textAssets.Length)];
            _message.OnNext(new StringReader(textAsset.text));
        }

        public void Result(bool isWin)
        {
            var result = isWin ? "Win" : "Lose";
            var fileName = $"Events/Battle/Result/{result}";
            ToEvent(fileName).Forget();
        }

        public async UniTaskVoid ToEvent(string fileName)
        {
            var textAsset = await Resources.LoadAsync(fileName) as TextAsset;
            if (textAsset != null) _message.OnNext(new StringReader(textAsset.text));
        }

        public void Battle(bool myTurn, int damage)
        {
            // todo 名前決め Entityにメッセージ作ってもらったほうがいいかも
            var name = myTurn ? "あなた" : "あいて";
            var text = $"{name}の攻撃,battle,{damage}\n{damage}のダメージ";
            _message.OnNext(new StringReader(text));
        }

        public void Dispose()
        {
            _message?.OnCompleted();
            _message?.Dispose();
        }
    }
}