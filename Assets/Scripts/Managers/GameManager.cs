using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //private int currentLevelIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    currentLevelIndex = scene.buildIndex;
    //}

    //public void LoadNextLevel()
    //{
    //    int nextIndex = currentLevelIndex + 1;

    //    if (nextIndex < SceneManager.sceneCountInBuildSettings)
    //    {
    //        SceneManager.LoadScene(nextIndex);
    //    }
    //    else
    //    {
    //        Debug.Log("Último nivel alcanzado. Volviendo al menú...");
    //        LoadMainMenu();
    //    }
    //}

    //public void ReloadLevel()
    //{
    //    SceneManager.LoadScene(currentLevelIndex);
    //}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("Lose");
    }
}
