using Cysharp.Threading.Tasks;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Extension;
using Digimon.Digimon.Scripts.Presentation.View.Monster;
using UnityEngine;
using UnityEngine.Playables;

namespace Digimon.Digimon.Scripts.Presentation.View.Battle
{
    public sealed class BattleView : MonoBehaviour
    {
        [SerializeField] private MonsterSpawner _selfMonster;
        [SerializeField] private MonsterView _self;
        [SerializeField] private MonsterView _enemy;
        [SerializeField] private PlayableDirector _presentEnemy;
        [SerializeField] private PlayableDirector _battleStart;

        public async UniTask InitializeAsync(MonsterName monsterName, int selfHp, int enemyHp)
        {
            _self.Initialize(selfHp);
            _enemy.Initialize(enemyHp);
            // 画面外に
            _enemy.transform.localPosition = new Vector3(0, 500, 0);
            await _selfMonster.SpawnAsync(monsterName);
        }

        public async UniTask PresentAsync()
        {
            await _presentEnemy.PlayAsync();
        }
        
        public async UniTask BattleStartAsync()
        {
            await _battleStart.PlayAsync();
        }

        public void Attack(int damage, int enemyHp)
        {
            _self.Attack();
            _enemy.TakeDamage(damage, enemyHp);
        }
        
        public void TakeDamage(int damage, int selfHp)
        {
            _enemy.Attack();
            _self.TakeDamage(damage, selfHp);
        }
    }
}