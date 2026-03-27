using UnityEngine;

namespace GameAssets.Scripts.DanceBattle
{
    [CreateAssetMenu(menuName = nameof(DanceBattle) + "/" + nameof(ActionZoneSettings),
        fileName = nameof(ActionZoneSettings))]
    public class ActionZoneSettings : ScriptableObject
    {
        [SerializeField] private float _animationDuration = 3f;
        [SerializeField] private float _playThreshold = 1.2f;
        [SerializeField] private float _startScale = 1.75f;
        [SerializeField] private float _endScale = 0.8f;
        [SerializeField] private float _resultAnimationDuration = 0.3f;
        [SerializeField] private float _fadeDuration = 0.2f;
        [SerializeField] private Color _defaultRingColor = Color.white;
        [SerializeField] private Color _defaultInnerCircleColor = Color.thistle;
        [SerializeField] private Color _readyStateInnerCircleColor = Color.violet;
        [SerializeField] private Color _successColor = Color.green;
        [SerializeField] private Color _failColor = new(1f, 0f, 0f, 0f);
        
        public float AnimationDuration => _animationDuration;
        public float PlayThreshold => _playThreshold;
        public float StartScale => _startScale;
        public float EndScale => _endScale;
        public float ResultAnimationDuration => _resultAnimationDuration;
        public float FadeDuration => _fadeDuration;
        public Color DefaultRingColor => _defaultRingColor;
        public Color DefaultInnerCircleColor => _defaultInnerCircleColor;
        public Color ReadyStateInnerCircleColor => _readyStateInnerCircleColor;
        public Color SuccessColor => _successColor;
        public Color FailColor => _failColor;
    }
}