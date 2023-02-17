using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public class CounterBox : MonoBehaviour
    {
        int _numberBoxes;
        public int GetNumberBox => _numberBoxes;
        public void AddNewBox() { _numberBoxes++; }
    }
}