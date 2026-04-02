using System;
using UnityEngine;

namespace GameAssets.Scripts.DanceBattle
{
    [Serializable]
    public class ClipData
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float[] _times;
        
        public AudioClip Clip => _clip;
        public float[] Times => _times;
    }
}