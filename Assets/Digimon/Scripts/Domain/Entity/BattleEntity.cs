using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class BattleEntity : IDisposable
    {
        private readonly Subject<BattleState> _state = new();
        private readonly ReactiveProperty<int> _selfHp = new();
        private readonly ReactiveProperty<int> _enemyHp = new();

        public BattleState Value { get; private set; }
        public int EnemyHp => _enemyHp.Value;

        private int _selfAtk;
        private int _selfDef;
        private int _selfSpeed;
        private int _selfSkill;
        private int _damage;

        private const int EnemyAtk = 80;
        private const int EnemyDef = 70;
        private const int EnemySpeed = 75;
        private const int EnemySkill = 5;

        // observable 受け取ったら　ダメージ演出
        public IObservable<(int Hp, int Damage)> OnSelfHpChangedAsObservable()
        {
            return _selfHp.Select(value => (value, _damage));
        }

        public IObservable<(int Hp, int Damage)> OnEnemyHpChangedAsObservable()
        {
            return _enemyHp.Select(value => (value, _damage));
        }

        public IObservable<BattleState> OnStateChangedAsObservable()
        {
            return _state.Share();
        }

        public void OnPresent(int hp, int atk, int def, int speed, int skill)
        {
            _selfHp.Value = hp;
            _selfAtk = atk;
            _selfDef = def;
            _selfSpeed = speed;
            _selfSkill = skill;
            _enemyHp.Value = 250;

            _state.OnNext(BattleState.Intro1);
            Value = BattleState.Intro1;
        }

        // コマンドで呼ばれる パラメータは空の時がある(バトル外の進行)　TakeDamage -> OnNextの順
        public void TakeDamage(string damageString)
        {
            if (!int.TryParse(damageString, out var damage)) return;
            switch (Value)
            {
                case BattleState.MyTurn:
                    _enemyHp.Value = Mathf.Max(_enemyHp.Value - damage, 0);
                    break;
                case BattleState.EnemyTurn:
                    _selfHp.Value = Mathf.Max(_selfHp.Value - damage, 0);
                    break;
            }
        }

        // コマンドで呼ばれる バトル開始だけViewに関わるので別
        public void OnNext()
        {
            if (Value == BattleState.BattleStart)
            {
                // すばやさで決定
                Value = _selfSpeed >= EnemySpeed ? BattleState.MyTurn : BattleState.EnemyTurn;
            }
            else
            {
                Value = Value.Next();
            }

            if (Value is BattleState.MyTurn or BattleState.EnemyTurn)
            {
                if (_selfHp.Value <= 0) Value = BattleState.Lose;
                if (_enemyHp.Value <= 0) Value = BattleState.Win;
            }

            _state.OnNext(Value);
        }

        public int CalcDamage()
        {
            var atk = Value == BattleState.MyTurn ? _selfAtk : EnemyAtk;
            var def = Value == BattleState.MyTurn ? _selfDef : EnemyDef;
            var skill = Value == BattleState.MyTurn ? _selfSkill : EnemySkill;

            var damage = (atk * (1 + skill / 10f) - def) * Random.Range(0.85f, 1f);
            Debug.Log($"Damage :{damage}");
            damage = Mathf.Max(damage, 1);
            return Mathf.FloorToInt(damage);
        }

        public void Dispose()
        {
            _state.OnCompleted();
            _state.Dispose();
            _selfHp.Dispose();
            _enemyHp.Dispose();
        }
    }
}