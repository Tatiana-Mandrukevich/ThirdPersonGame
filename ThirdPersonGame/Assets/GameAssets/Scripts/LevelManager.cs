using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const int MAX_LEVEL = 1;

    private static CancellationTokenSource _cts = new();

    public static bool IsLevelCorrect(int level)
    {
        return level > 0 && level <= MAX_LEVEL;
    }

    public static void StopLoading()
    {
        _cts.Cancel();
    }

    public static async Task<bool> LoadAsync(int level)
    {
        //SceneManager.LoadScene(level);
        Debug.Log($"Start loading level {level}");
        await Task.Delay(3000);

        if (_cts.IsCancellationRequested)
        {
            _cts = new CancellationTokenSource();
            return false;
        }

        await SceneManager.LoadSceneAsync(level);
        Debug.Log($"Level {level} loaded");
        return true;
    }

    public static IEnumerator LoadByCoroutine(int level)
    {
        Debug.Log($"Start loading level {level}");
        yield return new WaitForSeconds(5);
        yield return SceneManager.LoadSceneAsync(level);
        Debug.Log($"Level {level} loaded"); // Не будет вызвано, если сцена загрузится, так как текущая сцена будет выгружена и объект с этим скриптом будет уничтожен
    }
}