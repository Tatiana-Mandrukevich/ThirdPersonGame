using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameAssets.Scripts.DanceBattle
{
    public class GameCore : MonoBehaviour
    {
        private const int ADD_SCORE_VALUE = 10;
        private const int SPECIAL_SCORE_VALUE = 50;
        
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private InputService _inputService;
        [SerializeField] private ActionZone[] _zones;

        private int _score;
        private float _startTime;
        
        public event Action<int> ScoreChanged;

        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreChanged?.Invoke(value);
            }
        }

        private void Start()
        {
            Score = 0;
            
            _zones[Random.Range(0, _zones.Length)].PlayStartAnimation();
            _startTime = Time.time;
        }

        private void OnEnable()
        {
            _inputService.SpecialClick += OnSpecialClick;
        }

        private void OnDisable()
        {
            _inputService.SpecialClick -= OnSpecialClick;
        }

        private void OnSpecialClick(int number)
        {
            if (IsSuccessfulClick(number))
            {
                _zones[number - 1].PlayPressedAnimation();
                Score += ADD_SCORE_VALUE;

                if (Score % SPECIAL_SCORE_VALUE == 0)
                {
                    _animator.PlaySpecial();
                    //transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
                }
            }
        }

        private bool IsSuccessfulClick(int number)
        {
            if (Time.time - _startTime > 1.2f)
            {
                return true;
            }

            return false;
        }
    }
}