using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private BoxCollider _spawnArea;
    [SerializeField] private GameObject _coinPrefab;

    private IEnumerator Start()
    {
        Debug.Log("Spawning coins");
        
        var delay = new WaitForSecondsRealtime(0.05f);

        for (int i = 0; i < 100; i++)
        {
            Instantiate(_coinPrefab, new Vector3(
                    Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x),
                    Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y),
                    Random.Range(_spawnArea.bounds.min.z, _spawnArea.bounds.max.z)),
                Quaternion.identity, _spawnArea.transform);
            yield return delay;
        }
        
        Debug.Log("All coins were spawned");
    }

    /*private async void OnEnable()
    {
        //Debug.Log($"Spawn Area from  {_spawnArea.bounds.min} to  {_spawnArea.bounds.max}");
        Debug.Log("Spawning coins");

        for (int i = 0; i < 100; i++)
        {
            await InstantiateAsync(_coinPrefab, _spawnArea.transform, new Vector3(
                Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x),
                Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y),
                Random.Range(_spawnArea.bounds.min.z, _spawnArea.bounds.max.z)),
                Quaternion.identity);
            await Task.Delay(50);
        }
        
        Debug.Log("All coins were spawned");
    }*/
}