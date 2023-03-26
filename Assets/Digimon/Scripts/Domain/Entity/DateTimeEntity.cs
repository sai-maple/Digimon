using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class DateTimeEntity : IDisposable
    {
        private readonly ReactiveProperty<int> _date = new(9);
        private readonly ReactiveProperty<GameTime> _gameTime = new();

        public int Date => _date.Value;

        public IObservable<int> OnDateChangedAsObservable()
        {
            return _date;
        }

        public IObservable<GameTime> OnGameTimeChangedAsObservable()
        {
            return _gameTime;
        }

        // 午前午後の行動終わりにコールする
        public void Next()
        {
            _gameTime.Value = _gameTime.Value.Next();
            //　翌日になったら1日進める
            if (_gameTime.Value != GameTime.Morning) return;
            _date.Value++;
        }

        public void Lose()
        {
            // 敗北後は強制的に翌日に
            _gameTime.Value = GameTime.Morning;
            _date.Value++;
        }

        public void Dispose()
        {
            _date?.Dispose();
            _gameTime?.Dispose();
        }
    }
}