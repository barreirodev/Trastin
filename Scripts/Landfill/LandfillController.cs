using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Landfill {
    public class LandfillController : MonoBehaviour
    {
        List<Vector3> _positions = new List<Vector3>(6);
        List<Quaternion> _rotations = new List<Quaternion>(6);
        public List<LandfillMediator> _landfills = new List<LandfillMediator>();

        public void ShuffleLandfill()
        {
            for (int i = 0; i < _landfills.Count; ++i)
            {
                _positions.Add(_landfills[i].transform.position);
                _rotations.Add(_landfills[i].transform.rotation);
            }
            System.Random r = new System.Random();
            for (int i = _positions.Count - 1; i > 0; i--)
            {
                int j = r.Next(0, i + 1);

                Vector3 temp = _positions[i];
                _positions[i] = _positions[j];
                _positions[j] = temp;
                Quaternion temp2 = _rotations[i];
                _rotations[i] = _rotations[j];
                _rotations[j] = temp2;
            }
            for (int i = 0; i < _landfills.Count; ++i)
            {
                _landfills[i].transform.position = _positions[i];
                _landfills[i].transform.rotation = _rotations[i];
            }
        }

        public void InteractWithLandfills()
        {
            foreach (LandfillMediator landfill in _landfills)
            {
                landfill.GetComponent<Animator>().enabled = false;
            }
        }
    }
}