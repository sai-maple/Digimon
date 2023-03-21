using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class MonsterAnimationEntity : IDisposable
    {
        private readonly Subject<MonsterReaction> _reaction = new();

        public IObservable<MonsterReaction> OnReactionAsObservable()
        {
            return _reaction.Share();
        }

        public void InvokeCommand(string parameter)
        {
            if (!Enum.TryParse(parameter, out MonsterReaction reaction))
            {
                Debug.Log($"{parameter} can not parse to MonsterReaction.");
                return;
            }

            _reaction.OnNext(reaction);
        }

        public void Dispose()
        {
            _reaction?.OnCompleted();
            _reaction?.Dispose();
        }
    }
}