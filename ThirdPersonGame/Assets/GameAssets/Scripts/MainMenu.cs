using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _loadLevelButton;
    [SerializeField] private TMP_InputField _levelInput;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Image _incorrectLevelIndicator;
    
    private int _selectedLevel;

    private void Start()
    {
        _incorrectLevelIndicator.enabled = false;
    }

    private void OnEnable()
    {
        _loadLevelButton.onClick.AddListener(OnLoadLevelClick);
        _quitButton.onClick.AddListener(OnQuitClick);
        _levelInput.onValueChanged.AddListener(OnLevelSelected);
    }

    private void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    }

    private void OnLoadLevelClick()
    {
        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            LoadLevel(_selectedLevel);
        }
        else
        {
            _incorrectLevelIndicator.enabled = true;
            Debug.LogError($"There in no level: {_selectedLevel}");
        }
    }

    private void OnLevelSelected(string value)
    {
        _selectedLevel = int.Parse(value);
        
        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            _incorrectLevelIndicator.enabled = false;
        }
    }
    
    private void LoadLevel(int level)
    {
       LevelManager.Load(level);
    }
}