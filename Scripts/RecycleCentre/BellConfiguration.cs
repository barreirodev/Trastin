using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Landfill;

namespace Recycle
{
    [CreateAssetMenu]
    public class BellConfiguration : ScriptableObject
    {
        [SerializeField] OriginConfiguration _origin;

        public OriginConfiguration GetOrigin => _origin;

    }
    }