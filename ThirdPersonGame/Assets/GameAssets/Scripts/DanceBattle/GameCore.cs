using System;
using System.Collections;
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
        [SerializeField] private GameObject _particles;
        
        private int _score;
        
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
            _particles.SetActive(false);

            for (int i = 0; i < _zones.Length; i++)
            {
                int index = i;
                _zones[i].Init(() => HandleFail(index));
            }

            StartCoroutine(TestGame());
        }

        private IEnumerator TestGame()
        {
            var repeatInterval = new Vector2(2, 4);
            float repeatTime = 0;
            float elapsedTime = 0;

            while (true)
            {
                if (elapsedTime >= repeatTime)
                {
                    _zones[Random.Range(0, _zones.Length)].PlayStartAnimation();
                    repeatTime = Random.Range(repeatInterval.x, repeatInterval.y);
                    elapsedTime = 0;
                }
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private void HandleFail(int index)
        {
            _zones[index].PlayFailedAnimation();
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
            int index = number - 1;
            
            if (IsSuccessfulClick(number))
            {
                _zones[index].PlaySuccessAnimation();
                Score += ADD_SCORE_VALUE;

                if (Score % SPECIAL_SCORE_VALUE == 0)
                {
                    _particles.SetActive(true);
                    _animator.PlaySpecial();
                }
            }
            else
            {
                _zones[index].PlayFailedAnimation();
            }
        }

        private bool IsSuccessfulClick(int number)
        {
            int index = number - 1;
            return _zones[index].CheckReady();
        }
    }
}