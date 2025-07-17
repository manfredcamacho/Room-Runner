using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    [Header("Sounds")]
    public SoundType hoverSound = SoundType.HOVER;
    public SoundType clickSound = SoundType.START;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame()
    {
        SoundManager.PlayMusic(MusicType.BACKGROUND);
        SceneManager.LoadScene("Level_1");
    }    

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        Debug.Log("Opening Options...");
    }
}
