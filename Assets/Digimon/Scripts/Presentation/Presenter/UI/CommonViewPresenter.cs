using System;
using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using UniRx;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class CommonViewPresenter : IInitializable, IDisposable
    {
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly StatusEntity _statusEntity;
        private readonly StaminaEntity _staminaEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly CommonView _commonView;
        private readonly MonsterSpawner _monsterSpawner;

        private readonly CompositeDisposable _disposable = new();

        public CommonViewPresenter(DateTimeEntity dateTimeEntity, StatusEntity statusEntity,
            StaminaEntity staminaEntity, MonsterTypeEntity monsterTypeEntity, CommonView commonView,
            MonsterSpawner monsterSpawner)
        {
            _dateTimeEntity = dateTimeEntity;
            _statusEntity = statusEntity;
            _staminaEntity = staminaEntity;
            _monsterTypeEntity = monsterTypeEntity;
            _commonView = commonView;
            _monsterSpawner = monsterSpawner;
        }

        public void Initialize()
        {
            _commonView.Initialize(_statusEntity.Hp, _statusEntity.Atk, _statusEntity.Def, _statusEntity.Speed,
                _statusEntity.SkillLevel);
            _monsterSpawner.SpawnAsync(_monsterTypeEntity.Value).Forget();

            // 進化ウインドウの裏で差し替える
            _monsterTypeEntity.OnEvolutionAsObservable()
                .Subscribe(monster => _monsterSpawner.SpawnAsync(monster).Forget());

            // 時間系
            _dateTimeEntity.OnDateChangedAsObservable()
                .Subscribe(_commonView.OnDateChanged)
                .AddTo(_disposable);
            _dateTimeEntity.OnGameTimeChangedAsObservable()
                .Subscribe(_commonView.OnTimeChanged)
                .AddTo(_disposable);

            // スタミナ
            _staminaEntity.OnValueChangedAsObservable()
                .Subscribe(_commonView.OnStaminaChanged)
                .AddTo(_disposable);

            // パラメータ系
            _statusEntity.OnHpChangedAsObservable()
                .Subscribe(value => _commonView.OnHpChanged(value.previous, value.current))
                .AddTo(_disposable);
            _statusEntity.OnAtkChangedAsObservable()
                .Subscribe(value => _commonView.OnAtkChanged(value.previous, value.current))
                .AddTo(_disposable);
            _statusEntity.OnDefChangedAsObservable()
                .Subscribe(value => _commonView.OnDefChanged(value.previous, value.current))
                .AddTo(_disposable);
            _statusEntity.OnSpeedChangedAsObservable()
                .Subscribe(value => _commonView.OnSpeedChanged(value.previous, value.current))
                .AddTo(_disposable);
            _statusEntity.OnSkillLevelChangedAsObservable()
                .Subscribe(value => _commonView.OnSkillLevelChanged(value.previous, value.current))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}