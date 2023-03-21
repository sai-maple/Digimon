using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.Monster
{
    // spawnerにくっつける
    public sealed class LoopMoveView : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;

        private void Awake()
        {
            // todo 左右をぴょんぴょん移動するアニメーション
            transform.DOPath(targets.Select(target => target.transform.position).ToArray(), targets.Length,
                    PathType.CatmullRom, PathMode.Sidescroller2D)
                .SetOptions(true)
                .SetLookAt(0.05f, Vector3.forward)
                .SetLoops(-1)
                .SetLink(gameObject);
        }
    }
}