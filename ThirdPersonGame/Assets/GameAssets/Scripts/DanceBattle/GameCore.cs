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
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ClipData[] _clipData;
        [SerializeField] private GameObject _gameResult;
        
        private int _score;
        
        public event Action<int> ScoreChanged;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                ScoreChanged?.Invoke(value);
            }
        }

        private void Start()
        {
            Score = 0;
            _particles.SetActive(false);
            _gameResult.SetActive(false);

            for (int i = 0; i < _zones.Length; i++)
            {
                int index = i;
                _zones[i].Init(() => HandleFail(index));
            }

            StartCoroutine(TestGame());
        }

        private IEnumerator TestGame()
        {
            var clipData = _clipData[Random.Range(0, _clipData.Length)];
            _audioSource.clip = clipData.Clip;
            _audioSource.Play();
            
            float elapsedTime = 0;
            int index = 0;
            float clipDuration = clipData.Clip.length;
            
            while (elapsedTime <= clipDuration)
            {
                if (_animator.IsSpecialDancePlaying())
                {
                    yield return null;
                    continue;
                }

                if (index < clipData.Times.Length)
                {
                    float timeMarker =  clipData.Times[index];
                    
                    if (elapsedTime >= timeMarker)
                    {
                        //Debug.Log($"{index}: {timeMarker} - {clipData.Times[index]}");
                        index++;
                        ActionZone zone;
                        int attempts = 0;
                        do
                        {
                            zone = _zones[Random.Range(0, _zones.Length)];
                            attempts++;
                        } while (zone.IsStartAnimationPlaying && attempts < _zones.Length * 2);
                        if (!zone.IsStartAnimationPlaying)
                        {
                            zone.PlayStartAnimation();
                        }
                    }
                }
                else
                {
                    yield return new WaitForSeconds(5f);
                    break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _gameResult.SetActive(true);
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

        private void TestException1()
        {
            
            var thirdPartService = new ThirdPartService();

            try
            {
                thirdPartService.DoAction1();
            }
            catch (Exception e)
            {
                Debug.LogError("Exception: " + e.Message);
                //throw;
            }

            int index = 2;

            try
            {
                thirdPartService.DoAction2(index);
            }
            catch (IndexOutOfRangeException e)
            {
                string data = "";

                foreach (object key in e.Data.Keys)
                {
                    data += $"{key}: {e.Data[key]}\n";
                }
                
                Debug.LogError($"Index: {e.Data["Index"]}\n" +
                               $"StackTrace: {e.StackTrace}" +
                               $"Data: \n{data}");
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Null reference exception: " + e);
            }
        }

        private async void TestException2()
        {
            var thirdPartService = new ThirdPartService();
            int index = 3;

            try
            {
                await thirdPartService.LoadScene(index);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load scene: " + e);
                throw;
            }
            finally
            {
               thirdPartService.StopLoading();
            }
        }
        
        private async void TestException3()
        {
            int index = 3;

            using (var thirdPartService = new ThirdPartService())
            {
                try
                {
                    await thirdPartService.LoadScene(index);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load scene: " + e);
                    throw;
                }
            }
        }
    }
}