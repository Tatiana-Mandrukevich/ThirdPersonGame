using System;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu;
    
    private void Start()
    {
        Debug.Log("Start initialization");
        _menu.alpha = 0;
        _menu.blocksRaycasts = false;
        
        Debug.Log("Game initialized");
        _menu.alpha = 1;
        _menu.blocksRaycasts = true;
    }
}