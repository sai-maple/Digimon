using System;
using System.Collections.Generic;
using System.Linq;
using Digimon.Digimon.Scripts.Applications.Enums;
using TMPro;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.UI
{
    public sealed class StatusView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI rankText;
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private Rank _rank;

        public void Display(int previous, int current)
        {
            if (previous != current)
            {
                var isPlus = current - previous > 0;
                animator.SetTrigger(isPlus ? "Plus" : "Minus");
            }

            statusText.text = $"{current}";
            var rank = _rank.Get(current);
            rankText.text = rank.Label();
            rankText.color = rank.Color();
        }

        [Serializable]
        private sealed class Rank
        {
            [SerializeField] private List<Value> _values;

            public StatusRank Get(int value)
            {
                return _values.Where(v => v.Threshold <= value).OrderBy(v => v.Threshold).First().View;
            }
        }

        [Serializable]
        private sealed class Value
        {
            public int Threshold;
            public StatusRank View;
        }
    }
}