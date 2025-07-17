using UnityEngine;
using UnityEngine.SceneManagement;

public static class BootstrapLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadBootstrap()
    {


        if (SceneManager.GetActiveScene().name != "Bootstrap")
        {
            SceneFlow.originalSceneToLoad = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Bootstrap");
        }
            

        
    }
}

public static class SceneFlow
{
    public static string originalSceneToLoad = "Menu";
}
