using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Playables;

namespace Digimon.Digimon.Scripts.Extension
{
    public static class PlayableDirectorExtension
    {
        public static async UniTask PlayAsync(this PlayableDirector self, CancellationToken cancellationToken = default)
        {
            self.Play();

            if (cancellationToken.IsCancellationRequested) return;

            while (!IsFinished(self))
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                if (cancellationToken.IsCancellationRequested) return;
            }

            if (self.extrapolationMode == DirectorWrapMode.Hold && IsFinished(self)) return;

            self.Stop();
        }

        private static bool IsFinished(PlayableDirector playableDirector)
        {
            if (playableDirector == default)
            {
                return true;
            }

            return playableDirector.extrapolationMode switch
            {
                DirectorWrapMode.Hold => playableDirector.playableGraph.IsValid() &&
                                         playableDirector.duration - playableDirector.time < double.Epsilon,
                DirectorWrapMode.Loop => false,
                DirectorWrapMode.None => !playableDirector.playableGraph.IsValid() &&
                                         playableDirector.state == PlayState.Paused &&
                                         playableDirector.time < double.Epsilon,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}