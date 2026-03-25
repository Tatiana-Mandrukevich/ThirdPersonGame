using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameAssets.Scripts.DanceBattle
{
    public class ActionZone : MonoBehaviour
    {
        private const float ANIMATION_DURATION = 3f;
        private const float START_SCALE = 1.75f;
        private const float END_SCALE = 0.8f;
        
        [SerializeField] private Image _ring;
        
        private Tweener _tween;

        public event Action Failed;

        private void Start()
        {
            ResetState();
            _ring.gameObject.SetActive(false);
        }

        private void ResetState()
        {
            _ring.transform.localScale = new Vector3(1, 1, 1) * START_SCALE;
            _ring.color = Color.white;
        }

        public void PlayStartAnimation()
        {
            ResetState();
            _ring.gameObject.SetActive(true);
            _tween?.Kill();
            _tween = _ring.transform.DOScale(END_SCALE, ANIMATION_DURATION)
                .OnComplete(OnAnimationComplete);
        }
        
        public void PlayPressedAnimation()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = _ring.DOColor(Color.green, 0.3f);
            }
        }

        private void OnAnimationComplete()
        {
            Failed?.Invoke();
        }
    }
}