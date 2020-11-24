using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField]
    private Berry berry;
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private RectTransform barFill;
    private Vector3 fillPosition;
    private float progressStartValue;
    private int currentBerriesCount;

    private const int MIN_BERRIES               = 0;
    private const int MAX_BERRIES               = 10;
    private const float PROGRESS_BAR_END_VALUE  = 0f;
    private const string BACKGROUND_SOUND       = "Background";
    private const string WIN_SOUND              = "Win";
    private const string COLLECT_SOUND          = "Collect";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        progressStartValue = - barFill.rect.width;
        currentBerriesCount = MIN_BERRIES;
        IncreaseProgress(currentBerriesCount);
    }

    private void Start()
    {
        soundManager.Play(BACKGROUND_SOUND);
    }

    public void CountBerries()
    {
        currentBerriesCount++;
        soundManager.Play(COLLECT_SOUND);

        if(currentBerriesCount <= MAX_BERRIES)
        {
            IncreaseProgress(currentBerriesCount);
        }   
    }

    public void CheckIfWon()
    {
        if(currentBerriesCount == MAX_BERRIES)
        {
            soundManager.Stop(BACKGROUND_SOUND);
            soundManager.Play(WIN_SOUND);
            berry.StopSpawning();
        }
    }

    private void IncreaseProgress(int count)
    {
        fillPosition.x = progressStartValue + (float)count / MAX_BERRIES * (PROGRESS_BAR_END_VALUE - progressStartValue);
        barFill.localPosition = fillPosition;
    }
}
