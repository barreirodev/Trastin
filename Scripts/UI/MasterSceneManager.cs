using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MasterSceneManager : MonoBehaviour
{
    public enum Game
    {
        Landfill,
        Park,
        Recycle
    }
    [SerializeField] Game _game;
    [SerializeField] Image[] _imageGames;
    [SerializeField] Camera camera;
    [SerializeField] CanvasGroup _canvasFade;
    public Canvas _canvas;
#if UNITY_EDITOR
    float _timeToSeeInstructions = 0.5f;
#else
    float _timeToSeeInstructions = 6f;
#endif
    Scene currentScene;
    float _currentTime = 0f;
    float _startTime = 0f;
    Scene sceneToLoad;  
    public bool withMouse;
    public TMP_Text loadingText;
    bool _dotsLoading;
    [Header("Wii balance")]
    public start_connect _startConnect;
    public static MasterSceneManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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
        _startTime = Time.realtimeSinceStartup;
        switch (_game)
        {
            case Game.Landfill:
                _imageGames[0].gameObject.SetActive(true);
                break;
            case Game.Park:
                _imageGames[1].gameObject.SetActive(true);
                break;
            case Game.Recycle:
                _imageGames[2].gameObject.SetActive(true);
                break;
        }
        if (withMouse)
        {
            LoadSceneMainMenu();
        }
    }

    public void LoadScene(string _sceneToLoad)
    {
        StartCoroutine(LoadSceneCoroutine(_sceneToLoad));
    }

    IEnumerator LoadingTextDots()
    {
        loadingText.text = "Cargando...";
        while (_dotsLoading)
        {
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Cargando.";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Cargando..";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "Cargando...";
        }
    }

    IEnumerator LoadSceneCoroutine(string _sceneToLoad)
    {
        _dotsLoading = true;
        StartCoroutine(LoadingTextDots());
        _canvas.gameObject.SetActive(true);
        switch (_game)
        {
            case Game.Landfill:
                _imageGames[0].gameObject.SetActive(true);
                break;
            case Game.Park:
                _imageGames[1].gameObject.SetActive(true);
                break;
            case Game.Recycle:
                _imageGames[2].gameObject.SetActive(true);
                break;
        }
        yield return null;

        _canvasFade.gameObject.SetActive(true);
        _canvasFade.alpha = 0f;

        foreach (Camera c in Camera.allCameras)
        { if (c != camera) { c.gameObject.SetActive(false); } }
        camera.gameObject.SetActive(true);

        if (currentScene.isLoaded)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            while (!unloadOperation.isDone)
            { yield return null; }
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        sceneToLoad = SceneManager.GetSceneByName(_sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        _currentTime = Time.realtimeSinceStartup;
        float diff = _currentTime - _startTime;
        float timeLeft = _timeToSeeInstructions - diff;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        while (_canvasFade.alpha < 1f)
        {
            _canvasFade.alpha += Time.deltaTime;
            yield return null;
        }

        //Activate the Scene
        asyncOperation.allowSceneActivation = true;
        _dotsLoading = false;

        currentScene = SceneManager.GetSceneAt(1);



        switch (_game)
        {
            case Game.Landfill:
                _imageGames[0].gameObject.SetActive(false);
                break;
            case Game.Park:
                _imageGames[1].gameObject.SetActive(false);
                break;
            case Game.Recycle:
                _imageGames[2].gameObject.SetActive(false);
                break;
        }


        _canvas.gameObject.SetActive(false);

        asyncOperation.allowSceneActivation = true;

        while (asyncOperation.progress < 1.0f)
        {
            
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad));
        camera.gameObject.SetActive(false);

        while (_canvasFade.alpha > 0f)
        {
            _canvasFade.alpha -= Time.deltaTime;
            yield return null;
        }
        _canvasFade.gameObject.SetActive(false);
    }

    public void LoadSceneGamePlay()
    {
        _startTime = Time.realtimeSinceStartup;
        switch (_game)
        {
            case Game.Landfill:
                LoadScene("Landfill");
                break;
            case Game.Park:
                LoadScene("Park");
                break;
            case Game.Recycle:
                LoadScene("Recycle");
                break;
        }
    }

    public void LoadSceneMainMenu()
    {
        _startTime = Time.realtimeSinceStartup;
        switch (_game)
        {
            case Game.Landfill:
                LoadScene("MainMenuLandfill");
                break;
            case Game.Park:
                LoadScene("MainMenuPark");
                break;
            case Game.Recycle:
                LoadScene("MainMenuRecycle");
                break;
        }
    }

    public void SelectSceneToInstantiate(GameObject go)
    {
        SceneManager.MoveGameObjectToScene(go, sceneToLoad);
    }
}

