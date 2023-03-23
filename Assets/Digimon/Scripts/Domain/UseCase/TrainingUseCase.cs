using System;
using Digimon.Digimon.Scripts.Applications.Enums;
using Digimon.Digimon.Scripts.Domain.Entity;
using Random = UnityEngine.Random;

namespace Digimon.Digimon.Scripts.Domain.UseCase
{
    public sealed class TrainingUseCase
    {
        private readonly StaminaEntity _staminaEntity;
        private readonly StatusEntity _statusEntity;

        public TrainingUseCase(StaminaEntity staminaEntity, StatusEntity statusEntity)
        {
            _staminaEntity = staminaEntity;
            _statusEntity = statusEntity;
        }

        public bool IsCrash(TrainingType trainingType)
        {
            var status = trainingType switch
            {
                TrainingType.Hp => _statusEntity.Hp,
                TrainingType.Atk => _statusEntity.Atk,
                TrainingType.Def => _statusEntity.Def,
                TrainingType.Speed => _statusEntity.Speed,
                _ => throw new ArgumentOutOfRangeException(nameof(trainingType), trainingType, null)
            };
            var late = _staminaEntity.ClashRate(trainingType, status);

            return Random.Range(0, 100) < late;
        }
    }
}