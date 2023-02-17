using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        public enum Game
        {
            Landfill,
            Park,
            Centre
        }
        public Game _game;

        [Header("Start menu")]
        [SerializeField] Button _startGameButton;
        [SerializeField] Button _exitGameButton;
        [SerializeField] Animator _animatorMenuStart;
        [SerializeField] Animator _cameraMenuStart;

        // Start is called before the first frame update
        void Awake()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _exitGameButton.onClick.AddListener(ExitGame);

        }

        void StartGame()
        {
            _animatorMenuStart.Play("MainMenuFade");
            switch (_game)
            {
                case Game.Landfill:
                    _cameraMenuStart.Play("CameraMenuLandfill");
                    break;
                case Game.Park:
                    _cameraMenuStart.Play("CameraMenuPark");
                    break;
                case Game.Centre:
                    _cameraMenuStart.Play("CameraMenuRecycle");
                    break;
            }
            StartCoroutine(Countdown());
        }
        void ExitGame()
        {
            Application.Quit();
        }

        IEnumerator Countdown()
        {
            yield return new WaitForSeconds(3f);
            MasterSceneManager.Instance.LoadSceneGamePlay();

        }

    }
}
