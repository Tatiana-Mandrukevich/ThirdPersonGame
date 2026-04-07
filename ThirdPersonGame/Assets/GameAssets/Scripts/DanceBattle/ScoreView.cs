using TMPro;
using UnityEngine;

namespace GameAssets.Scripts.DanceBattle
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private GameCore _gameCore;
        [SerializeField] private TMP_Text _score;
        
        private void OnEnable()
        {
            _gameCore.ScoreChanged += OnScoreChanged;
            
            SetCurrentScore();
        }

        private void OnDisable()
        {
            _gameCore.ScoreChanged -= OnScoreChanged;
        }
        
        private void OnScoreChanged(int value)
        {
            _score.text = value.ToString();
        }

        private void SetCurrentScore()
        {
            _score.text = _gameCore.Score.ToString();
        }
    }
}