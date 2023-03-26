using System;
using System.Linq;
using DG.Tweening;
using Digimon.Digimon.Scripts.Applications.Enums;
using UnityEngine;

namespace Digimon.Digimon.Scripts.Presentation.View.Audio
{
    public sealed class AudioView : MonoBehaviour
    {
        public static AudioView Instance { get; private set; }

        [SerializeField] private AudioSource[] _bgmAudios;
        [SerializeField] private AudioSource _seAudio;
        [SerializeField] private BgmPair[] _bgmPairs;
        [SerializeField] private SePair[] _sePairs;
        private int _index = 0;

        private float _volume = 0.4f;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayBgm(Bgm bgm)
        {
            if (bgm == Bgm.Empty)
            {
                foreach (var bgmAudio in _bgmAudios)
                {
                    bgmAudio.DOFade(0, 1f).OnComplete(() => bgmAudio.Stop());
                }
                return;
            }
            
            var clip = _bgmPairs.FirstOrDefault(pair => pair.Bgm == bgm);
            if(clip == null) return;
            _bgmAudios[_index % 2].clip = clip.AudioClip;
            _bgmAudios[_index % 2].Play();
            _bgmAudios[_index % 2].volume = 0;
            _bgmAudios[_index % 2].DOFade(_volume, 1f);
            _index++;
            var previous = _bgmAudios[_index % 2];
            previous.DOFade(0, 1f).OnComplete(() => previous.Stop());
        }

        public void PlaySe(Se se)
        {
            var clip = _sePairs.FirstOrDefault(pair => pair.Se == se);
            if(clip == null) return;
            _seAudio.PlayOneShot(clip.AudioClip);
        }

        public void OnChangeBgmVolume(float value)
        {
            foreach (var bgmAudio in _bgmAudios)
            {
                bgmAudio.volume = value;
            }

            _volume = value;
        }

        public void OnChangeSeVolume(float value)
        {
            _seAudio.volume = value;
        }

        [Serializable]
        private class BgmPair
        {
            public Bgm Bgm;
            public AudioClip AudioClip;
        }

        [Serializable]
        private class SePair
        {
            public Se Se;
            public AudioClip AudioClip;
        }
    }
}