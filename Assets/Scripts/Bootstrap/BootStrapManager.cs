using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [Header("Managers Prefabs")]
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject soundManagerPrefab;

    private void Awake()
    {
        if(SoundManager.instance == null) Instantiate(soundManagerPrefab);
        if(GameManager.instance == null) Instantiate(gameManagerPrefab);

        SceneManager.LoadScene(SceneFlow.originalSceneToLoad);
    }

    private void Start()
    {
        SceneManager.LoadScene(SceneFlow.originalSceneToLoad);
    }
}
