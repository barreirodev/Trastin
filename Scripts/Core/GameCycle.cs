using UnityEngine;
using UI;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Box;

namespace Core
{
    public class GameCycle : MonoBehaviour
    {
        public enum GameState
        {
            Menu,
            Playing,
            Score
        }
        public GameState _gameState;

        [Header("GUI")]
        [SerializeField] GameObject _menus;
        [SerializeField] CounterBox _counterBox;
        [SerializeField] Color _errorTextColor;
        [SerializeField] Color _starTextColor;
        Color _normalColor;
        Vector3 _normalScale;

        [Header("Rewards")]
        Transform _playerTransform;
        [SerializeField] ParticleSystem _star;
        [SerializeField] ParticleSystem _starParticles;
        [SerializeField] Transform _scoreIcon;
        [SerializeField] GameObject _exclamation;

        int _score;
        float _time;
        bool _newPoint;
        public static GameCycle Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Configuration();
            DOTween.Init();
        }

        public void Configuration()
        {
            
            _score = 0;
            _time = GameConfiguration.Instance.duration;
            _menus.GetComponent<Menu>()._scoreText.text = _score.ToString();
            _menus.GetComponent<Menu>()._timeText.text = _time.ToString();
            _normalColor = _menus.GetComponent<Menu>()._scoreText.color;
            _normalScale = _menus.GetComponent<Menu>()._scoreText.gameObject.transform.localScale;
        }

        private void Update()
        {
            if (_gameState == GameState.Playing) {
                _time -= Time.deltaTime;
                _menus.GetComponent<Menu>()._timeText.text = Mathf.RoundToInt(_time).ToString();
                if (_time <= 0)
                {
                    _gameState = GameState.Score;
                    StartCoroutine(_menus.GetComponent<Menu>().FinishScore());
                    AudioEffectsManager.Instance.EndGame();
                    GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
                    foreach (GameObject box in boxes)
                    {
                        box.GetComponent<BoxMediator>().HideBox();
                    }
                    
                }
            }
        }

        public void AddScore(int point)
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if (point > 0)
            {
                _star.transform.position = new Vector3(_playerTransform.position.x, _star.transform.position.y, _playerTransform.position.z);
                _star.Play();
                _starParticles.Stop();
                _starParticles.Clear();
                _starParticles.transform.position = new Vector3(_star.transform.position.x, _star.transform.position.y, _star.transform.position.z); ;
                _starParticles.Play();

                _starParticles.transform.DOMove(_scoreIcon.position, 1.0f).OnComplete(() => UpdateScore(point)) ;
            }
            else
            {
                _exclamation.transform.position = new Vector3(_playerTransform.position.x, _exclamation.transform.position.y, _playerTransform.position.z);
                Sequence exclamationSequence = DOTween.Sequence();
                exclamationSequence.Append(_exclamation.transform.DOScale(2.0f,0.25f)).
                    AppendInterval(0.5f).
                    Append(_exclamation.transform.DOScale(0,0.25f)).
                    InsertCallback(0, ()=>UpdateScore(point));
            }
           
            _counterBox.AddNewBox();
        }

        private void UpdateScore(int point)
        {
            _score += point;
            if (_score < 0) _score = 0;
            TMP_Text scoreText = _menus.GetComponent<Menu>()._scoreText;
            scoreText.text = _score.ToString();

            Color colorNeeded;
            Vector3 scaleNeeded;
            if (point > 0)
            {
                colorNeeded = _starTextColor;
                scaleNeeded = new Vector3(1.15f,1.15f,1.15f);
            }
            else
            {
                colorNeeded = _errorTextColor;
                scaleNeeded = new Vector3(0.85f,0.85f,0.85f);
            }           

            Sequence textSequence =  DOTween.Sequence();
            textSequence.Append(scoreText.DOColor(colorNeeded, 0.5f).SetEase(Ease.OutExpo)).
                Join(scoreText.gameObject.transform.DOScale(scaleNeeded,0.25f).SetEase(Ease.InOutBack)).
                Append(scoreText.DOColor(_normalColor, 0.5f).SetEase(Ease.OutExpo)).
                Join(scoreText.gameObject.transform.DOScale(_normalScale, 0.25f).SetEase(Ease.InOutBack));
        }
    }
}