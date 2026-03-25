using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameAssets.Scripts.DanceBattle
{
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly int[] _specialDances =
        {
            AnimatorParameters.SpecialDance1,
            AnimatorParameters.SpecialDance2
        };
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlaySpecial()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                _animator.SetTrigger(_specialDances[Random.Range(0, _specialDances.Length)]);
            }
            
            //transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
        }
    }
}