using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameAssets.Scripts.DanceBattle
{
    public class ActionZone : MonoBehaviour
    {
        [SerializeField] private Image _ring;
        [SerializeField] private Image _innerCircle;
        [SerializeField] private ActionZoneSettings _settings;
        [SerializeField] private GameObject _successParticles;
        [SerializeField] private GameObject _failParticles;
        
        private Sequence _sequence;
        private Action _failed;
        private float _startTime;

        public bool IsFailed { get; private set; }

        public void Init(Action failed)
        {
            _failed = failed;
        }

        public bool CheckReady()
        {
            return (Time.time - _startTime > _settings.PlayThreshold) && !IsFailed;
        }

        private void Start()
        {
            ResetState();
        }

        private void ResetState()
        {
            HideAllParticles();
            _innerCircle.color = _settings.DefaultInnerCircleColor;
            IsFailed = false;
            _ring.transform.localScale = new Vector3(1, 1, 1) * _settings.StartScale;
            _ring.color = _settings.DefaultRingColor;
            _ring.gameObject.SetActive(false);
        }

        [ContextMenu("Play Start Animation")]
        public void PlayStartAnimation()
        {
            ResetState();
            _ring.gameObject.SetActive(true);
            _startTime = Time.time;
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(_ring.transform
                .DOScale(_settings.EndScale, _settings.AnimationDuration))
                .InsertCallback(_settings.PlayThreshold, () => _innerCircle.color = _settings.ReadyStateInnerCircleColor)
                .OnComplete(OnAnimationComplete);
        }
        
        public void PlaySuccessAnimation()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(_ring.DOColor(_settings.SuccessColor, _settings.ResultAnimationDuration))
                .Append(_ring.DOFade(0f, _settings.FadeDuration))
                .AppendCallback(() => RestartParticles(_successParticles))
                .AppendInterval(2)
                .AppendCallback(() => SetParticlesActive(_successParticles, false))
                .OnComplete(ResetState);
        }
        
        public void PlayFailedAnimation()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(_ring.DOColor(_settings.FailColor, _settings.ResultAnimationDuration))
                .Append(_ring.DOFade(0f, _settings.FadeDuration))
                .AppendCallback(() => RestartParticles(_failParticles))
                .AppendInterval(2)
                .AppendCallback(() => SetParticlesActive(_failParticles, false))
                .OnComplete(ResetState);
        }

        private void OnAnimationComplete()
        {
            _innerCircle.color = _settings.DefaultInnerCircleColor;
            RestartParticles(_failParticles);
            IsFailed = true;
            _failed?.Invoke();
        }

        private void HideAllParticles()
        {
            SetParticlesActive(_successParticles, false);
            SetParticlesActive(_failParticles, false);
        }

        private static void SetParticlesActive(GameObject particles, bool isActive)
        {
            particles.SetActive(isActive);
        }

        private static void RestartParticles(GameObject particles)
        {
            particles.SetActive(false);
            particles.SetActive(true);
        }
    }
}