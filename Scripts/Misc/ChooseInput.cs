using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc
{
    public class ChooseInput : MonoBehaviour
    {
        public void SelectMouse()
        {
            SceneManager.LoadScene("SplashMouse");
        }
        public void SelectWiiBalance()
        {
            SceneManager.LoadScene("SplashWiiBalance");
        }
    }
}