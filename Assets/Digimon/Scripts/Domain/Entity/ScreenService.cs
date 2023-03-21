using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class ScreenService : IDisposable
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

        public void Dispose()
        {
            _screen?.Dispose();
        }
    }
}