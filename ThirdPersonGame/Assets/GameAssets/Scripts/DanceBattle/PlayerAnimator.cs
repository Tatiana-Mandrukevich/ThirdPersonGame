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
        }

        public bool IsSpecialDancePlaying()
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            if (IsSpecialState(currentState))
            {
                return true;
            }

            if (_animator.IsInTransition(0))
            {
                AnimatorStateInfo nextState = _animator.GetNextAnimatorStateInfo(0);
                return IsSpecialState(nextState);
            }

            return false;
        }

        private static bool IsSpecialState(AnimatorStateInfo stateInfo)
        {
            return stateInfo.IsName("SpecialDance1") || stateInfo.IsName("SpecialDance2");
        }
    }
}