using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Digimon.Digimon.Scripts.Presentation.View.UI;
using naichilab;
using VContainer.Unity;

namespace Digimon.Digimon.Scripts.Presentation.Presenter.UI
{
    public sealed class BattleWinScreenPresenter : IInitializable
    {
        private readonly DateTimeEntity _dateTimeEntity;
        private readonly ScreenEntity _screenEntity;
        private readonly MonsterTypeEntity _monsterTypeEntity;
        private readonly MessageEntity _messageEntity;
        private readonly EvolutionView _evolutionView;
        private readonly ScreenView _screenView;
        private readonly Screens _screens;
        
        public void Initialize()
        {
            
            RankingLoader.Instance.SendScoreAndShowRanking (_dateTimeEntity.Date);
        }
    }
}