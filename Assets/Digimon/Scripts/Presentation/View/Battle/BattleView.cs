using Coffee.UIExtensions;
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
        [SerializeField] private PlayableDirector _battleStart;
        [SerializeField] private PlayableDirector _battleFinish;
        [SerializeField] private UIParticle _particle;
 
        public async UniTask InitializeAsync(MonsterName monsterName, int selfHp, int enemyHp)
        {
            _self.Initialize(selfHp, -50);
            _enemy.Initialize(enemyHp, 550);
            // 画面外に
            _particle.Stop();
            await _selfMonster.SpawnAsync(monsterName);
        }

        public async UniTask PresentAsync()
        {
            await _enemy.PresentAsync();
        }
        
        public async UniTask BattleStartAsync()
        {
            _self.DoFadeUi(1);
            _enemy.DoFadeUi(1);
            await _battleStart.PlayAsync();
            _particle.Play();
        }

        public async UniTask BattleFinishAsync()
        {
            _particle.Stop();
            _self.DoFadeUi(0);
            _enemy.DoFadeUi(0);
            await _battleFinish.PlayAsync();
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