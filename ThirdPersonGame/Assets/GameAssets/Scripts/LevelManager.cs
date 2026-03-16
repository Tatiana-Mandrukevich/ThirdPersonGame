using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const int MAX_LEVEL = 1;

    public static bool IsLevelCorrect(int level)
    {
        return level > 0 && level <= MAX_LEVEL;
    }

    public static void Load(int level)
    {
        SceneManager.LoadScene(level);
    }
}