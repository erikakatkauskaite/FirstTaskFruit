using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMaster : MonoBehaviour
{
    public static SceneMaster Instance { get; private set; }

    [SerializeField]
    private UI UI;

    private const int SCENE_INDEX   = 0;
    private const float DELAY       = 0.6f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartGame()
    {
        UI.CheckIfPressedRestart();
        Invoke(nameof(LoadScene), DELAY);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SCENE_INDEX);
    }
}
