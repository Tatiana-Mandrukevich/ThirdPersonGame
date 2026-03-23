using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    
    private void Start()
    {
        StartCoroutine(DoRotate());
    }

    private IEnumerator DoRotate()
    {
        while (true)
        {
            transform.rotation *= Quaternion.Euler(0, _rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    public void DoSmall(Action completeAction)
    {
        transform.DOScale(transform.localScale * 0.5f,  0.2f).SetEase(Ease.InOutSine)
            .OnComplete(completeAction.Invoke);
    }
}