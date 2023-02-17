using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Core;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public enum Game
        {
            Landfill,
            Park,
            Recycle
        }
        public Game _game;

        [Header("Start menu")]
        [SerializeField] GameFacade _gameFacade;
        [SerializeField] TMP_Text _countdown;
        [SerializeField] Image _fade;

        [Header("GUI")]
        public TMP_Text _timeText;
        public TMP_Text _scoreText;

        [Header("Finish menu")]
        public GameObject _finishMenu;
        public TMP_Text _scoreRanking;
        [SerializeField] Button _againButton;
        [SerializeField] Button _exitButton;
        [SerializeField] GameObject[] _stars = new GameObject[3];
        [SerializeField] GameObject _ballons;

        [Header("Other")]
        [SerializeField] CounterBox _counterBox;

        // Start is called before the first frame update
        void Awake()
        {
            _againButton.onClick.AddListener(Again);
            _exitButton.onClick.AddListener(Exit);
        }
        private void Start()
        {
            GameCycle.Instance._gameState = GameCycle.GameState.Menu;

            switch (_game)
            {
                case Game.Landfill:
                    StartCoroutine(CountdownLandfill());
                    break;
                case Game.Park:
                    StartCoroutine(CountdownPark());
                    break;
                case Game.Recycle:
                    StartCoroutine(CountdownRecycle());
                    break;
                default:
                    break;  
            }
           
        }

        IEnumerator CountdownLandfill()
        {
            _gameFacade.ShowLandfills();
            _gameFacade.ShowPlayer();
            _timeText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            
            _fade.gameObject.SetActive(false);
            _countdown.gameObject.SetActive(true);

            AudioEffectsManager.Instance.CountDown();
            for (int i = 0; i < 3; ++i)
            {
                _countdown.text = (3 - i).ToString();
                yield return new WaitForSeconds(1f);
            }
            _countdown.gameObject.SetActive(false);
            _gameFacade.ShowBox();
            _gameFacade.InteractWithLandfills();
            GameCycle.Instance._gameState = GameCycle.GameState.Playing;

        }
        IEnumerator CountdownPark()
        {
            _gameFacade.ShowPlayer();
            _timeText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);

            _fade.gameObject.SetActive(false);
            _countdown.gameObject.SetActive(true);

            AudioEffectsManager.Instance.CountDown();
            for (int i = 0; i < 3; ++i)
            {
                _countdown.text = (3 - i).ToString();
                yield return new WaitForSeconds(1f);
            }
            _countdown.gameObject.SetActive(false);
            GameCycle.Instance._gameState = GameCycle.GameState.Playing;
        }

        IEnumerator CountdownRecycle()
        {
            _gameFacade.ShowPlayer();
            _timeText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);

            _fade.gameObject.SetActive(false);
            _countdown.gameObject.SetActive(true);

            AudioEffectsManager.Instance.CountDown();
            for (int i = 0; i < 3; ++i)
            {
                _countdown.text = (3 - i).ToString();
                yield return new WaitForSeconds(1f);
            }
            _countdown.gameObject.SetActive(false);
            _gameFacade.ShowBox();
            GameCycle.Instance._gameState = GameCycle.GameState.Playing;

        }

        void Again()
        {
            MasterSceneManager.Instance.LoadSceneGamePlay();
        }

        public void Exit()
        {
            MasterSceneManager.Instance.LoadSceneMainMenu();
        }

        public IEnumerator FinishScore()
        {
            _finishMenu.SetActive(true);
            foreach(GameObject star in _stars)
            {
                star.SetActive(false);
            }

            _againButton.gameObject.SetActive(false);
            _exitButton.gameObject.SetActive(false);
            yield return new WaitForSeconds(5.0f);
            _scoreRanking.text = _scoreText.text;
            _scoreRanking.gameObject.SetActive(true);
            AudioEffectsManager.Instance.Ranking();
            yield return new WaitForSeconds(2.0f);
            bool atLeastOneStar = float.Parse(_scoreRanking.text) / _counterBox.GetNumberBox >= 0.33f;
            if (atLeastOneStar)
            {
                if (atLeastOneStar)
                {
                    _stars[0].SetActive(true);
                    AudioEffectsManager.Instance.NewStar();
                    yield return new WaitForSeconds(1.0f);
                }
                if (float.Parse(_scoreRanking.text) / _counterBox.GetNumberBox >= 0.5f)
                {
                    _stars[1].SetActive(true);
                    AudioEffectsManager.Instance.NewStar();
                    yield return new WaitForSeconds(1.0f);
                }
                if (float.Parse(_scoreRanking.text) / _counterBox.GetNumberBox >= 0.66f)
                {
                    _stars[2].SetActive(true);
                    AudioEffectsManager.Instance.NewStar();
                    _ballons.SetActive(true);
                    AudioEffectsManager.Instance.Ballons();
                }
                yield return new WaitForSeconds(2.0f);
            }
           
            _againButton.gameObject.SetActive(true);
            _exitButton.gameObject.SetActive(true);
        }
    }
}
