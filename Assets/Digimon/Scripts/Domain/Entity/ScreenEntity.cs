using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using Random = UnityEngine.Random;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class ScreenEntity : IDisposable
    {
        private readonly ReactiveProperty<Screens> _screen = new(Screens.Non);
        public Screens Value => _screen.Value;

        public IObservable<Screens> OnChangedAsObservable()
        {
            return _screen;
        }

        public void OnNext(Screens screens)
        {
            _screen.Value = screens;
        }

        public void Event()
        {
            var screen = Random.Range(0, 100) < 50 ? Screens.EventShop : Screens.EventPark;
            _screen.Value = screen;
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