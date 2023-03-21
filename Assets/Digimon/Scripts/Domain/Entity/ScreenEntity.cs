using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using Random = UnityEngine.Random;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class ScreenEntity : IDisposable
    {
        private readonly ReactiveProperty<Screens> _screen = new();

        public IObservable<Screens> OnChangedAsObservable()
        {
            return _screen;
        }

        public void OnNext(Screens screens)
        {
            _screen.Value = screens;
        }

        public bool TryCommonEvent()
        {
            var screen = Random.Range(0, 100) < 50 ? Screens.Menu : Screens.EventCommon;
            _screen.Value = screen;
            return screen == Screens.EventCommon;
        }

        public void Dispose()
        {
            _screen?.Dispose();
        }
    }
}