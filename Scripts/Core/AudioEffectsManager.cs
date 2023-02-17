using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEffectsManager : MonoBehaviour
    {
        [SerializeField] AudioClip _countdown;
        [SerializeField] AudioClip _correct;
        [SerializeField] AudioClip _error;
        [SerializeField] AudioClip _rankingStars;
        [SerializeField] AudioClip _hasNewStar;
        [SerializeField] AudioClip _endGame;
        [SerializeField] AudioClip _balloons;
        [SerializeField] AudioClip _showLandfills;
        [SerializeField] AudioClip _showBox;
        [SerializeField] AudioClip _openLandfill;
        [SerializeField] AudioClip _closeLandfill;
        [SerializeField] AudioClip _boxThroughTunel;
        AudioSource _audioSourcePlayer;
        AudioSource[] _audioSource;

        public static AudioEffectsManager Instance { get; private set; }
        void Awake()
        {          
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            _audioSource = GetComponents<AudioSource>();
        }
        public void SetAudioSourcePlayer()
        {
            _audioSourcePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            AudioEffectsManager.Instance.RunPlayer();
        }
        public void CountDown()
        {
            PlaySound(_countdown);
        }
        public void Correct()
        {
            PlaySound(_correct);
        }
        public void Error()
        {
            PlaySound(_error);
        }
        public void Ranking()
        {
            PlaySound(_rankingStars);
        }
        public void NewStar()
        {
            PlaySound(_hasNewStar);
        }
        public void EndGame()
        {
            PlaySound(_endGame);
            StopPlayer();
        }
        public void Ballons()
        {
            PlaySound(_balloons);
        }
        public void ShowLandfills()
        {
            PlaySound(_showLandfills);
        }
        public void ShowPlayer()
        {
            PlaySound(_showBox);
        }
        public void ShowBox()
        {
            PlaySound(_showBox);
        }
        public void OpenLandfill()
        {
            PlaySound(_openLandfill);
        }
        public void CloseLandfill()
        {
            PlaySound(_closeLandfill);
        }
        public void IsOpenLandfillSound()
        {
            if (_audioSource[0].clip.Equals(_openLandfill) &&_audioSource[0].isPlaying)
            {
                _audioSource[0].clip = _closeLandfill;
                _audioSource[0].Play();
            }
            else if (_audioSource[1].clip.Equals(_openLandfill) && _audioSource[1].isPlaying)
            {
                _audioSource[1].clip = _closeLandfill;
                _audioSource[1].Play();
            }
        }
        public void RunPlayer()
        {
            if (!_audioSourcePlayer.isPlaying)
            {
                _audioSourcePlayer.Play();
            }         
        }
        public void StopPlayer()
        {
            if (_audioSourcePlayer.isPlaying)
            {
                _audioSourcePlayer.Stop();
            }           
        }
        public void BoxThroughtTunel()
        {
            PlaySound(_boxThroughTunel);
        }
        public void PlaySound(AudioClip audio)
        {
            if (!_audioSource[0].isPlaying)
            {
                _audioSource[0].clip = audio;
                _audioSource[0].Play();
            }
            else if(!_audioSource[1].isPlaying)
            {
                _audioSource[1].clip = audio;
                _audioSource[1].Play();
            }
            else
            {
                _audioSource[2].clip = audio;
                _audioSource[2].Play();
            }

        }

    }
}
