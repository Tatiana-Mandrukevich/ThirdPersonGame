using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _loadLevelButton;
    [SerializeField] private Button _stopLoadingLevelButton;
    [SerializeField] private TMP_InputField _levelInput;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _incorrectLevelIndicator;
    
    private int _selectedLevel;
    private Coroutine _loadLevelCoroutine;

    private void Start()
    {
        _incorrectLevelIndicator.SetActive(false);
    }

    private void OnEnable()
    {
        _loadLevelButton.onClick.AddListener(OnLoadLevelClick);
        _stopLoadingLevelButton.onClick.AddListener(OnStopLoadingLevelClick);
        _quitButton.onClick.AddListener(OnQuitClick);
        _levelInput.onValueChanged.AddListener(OnLevelSelected);
    }
    
    private void OnDisable()
    {
        _loadLevelButton.onClick.RemoveListener(OnLoadLevelClick);
        _stopLoadingLevelButton.onClick.RemoveListener(OnStopLoadingLevelClick);
        _quitButton.onClick.RemoveListener(OnQuitClick);
        _levelInput.onValueChanged.RemoveListener(OnLevelSelected);
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
            _incorrectLevelIndicator.SetActive(true);
            Debug.LogError($"There in no level: {_selectedLevel}");
        }
    }
    
    private void OnStopLoadingLevelClick()
    {
        //StopCoroutine(_loadLevelCoroutine);
        LevelManager.StopLoading();
        Debug.Log($"Stop loading level {_selectedLevel}");
    }

    private void OnLevelSelected(string value)
    {
        _selectedLevel = int.Parse(value);
        
        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            _incorrectLevelIndicator.SetActive(false);
        }
    }
    
    private async void LoadLevel(int level)
    {
        bool isLoaded = await LevelManager.LoadAsync(level); 
        Debug.Log(isLoaded);
    }
    
    /*private void LoadLevel(int level)
    {
        _loadLevelCoroutine = StartCoroutine(LevelManager.LoadByCoroutine(level));
        Debug.Log("LoadLevel: after call LevelManager.Load");
        StopCoroutine(_loadLevelCoroutine);
    }*/
}