using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_connect : MonoBehaviour
{
    [HideInInspector]
    public WBBManager wii;
    private void Start()
    {
        wii = WBBManager.wbbManager;
        StartCoroutine(isConnected());
    }
    IEnumerator isConnected()
    {
        MasterSceneManager.Instance.loadingText.text = "¡Conecta la Wii Balance para empezar!";
        while (wii.getWiiState() != 0)
        {
            
            yield return null;
        }
        MasterSceneManager.Instance.LoadSceneMainMenu();
    }

    public Vector2 GetMovement() => new Vector2(wii.getCoP().x / wii.getWbbSize().x, wii.getCoP().y / wii.getWbbSize().y);
}
