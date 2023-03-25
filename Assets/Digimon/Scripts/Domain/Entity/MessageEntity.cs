using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using EventType = Digimon.Digimon.Scripts.Applications.Enums.EventType;
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
            fileName = isClash ? "Events/Training/Clash" : fileName;

            ToEvent(fileName).Forget();
        }

        public void RandomEvent(EventType eventType)
        {
            var textAssets = Resources.LoadAll<TextAsset>($"Events/Events/{eventType}");
            var textAsset = textAssets[Random.Range(0, textAssets.Length)];
            _message.OnNext(new StringReader(textAsset.text));
        }

        public void Result(BattleState state)
        {
            var result = state == BattleState.Win ? "Win" : "Lose";
            var fileName = $"Events/Battle/Result/{result}";
            ToEvent(fileName).Forget();
        }

        public async UniTaskVoid ToEvent(string fileName)
        {
            var textAsset = await Resources.LoadAsync(fileName) as TextAsset;
            if (textAsset != null) _message.OnNext(new StringReader(textAsset.text));
        }

        public void Battle(BattleState state, int damage)
        {
            // todo 名前決め
            var name = state == BattleState.MyTurn ? "あなた" : "大精霊";
            var text = $"{name}の攻撃,damage,{damage}\n{damage}のダメージ,battle,";
            _message.OnNext(new StringReader(text));
        }

        public void Dispose()
        {
            _message?.OnCompleted();
            _message?.Dispose();
        }
    }
}