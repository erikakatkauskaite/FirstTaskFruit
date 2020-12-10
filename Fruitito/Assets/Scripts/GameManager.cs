using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField]
    private BerrySpawner berry;
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private RectTransform barFill;
    [SerializeField]
    private UI UI;

    private Vector3 fillPosition;
    private float progressStartValue;
    private int currentBerriesCount;

    private const int MIN_BERRIES               = 0;
    private const int MAX_BERRIES               = 4;
    private const float PROGRESS_BAR_END_VALUE  = 0f;
    private const float DELAY                   = 1f;
    private const string BACKGROUND_SOUND       = "Background";
    private const string WIN_SOUND              = "Win";
    private const string COLLECT_SOUND          = "Collect";

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

    private void Start()
    {
        progressStartValue = -barFill.rect.width;
        currentBerriesCount = MIN_BERRIES;
        IncreaseProgress(currentBerriesCount);
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
            Invoke(nameof(StartButtonAnimation), DELAY);
        }
    }

    private void StartButtonAnimation()
    {
        UI.CheckIfWon();
    }

    private void IncreaseProgress(int count)
    {
        fillPosition.x = progressStartValue + (float)count / MAX_BERRIES * (PROGRESS_BAR_END_VALUE - progressStartValue);
        barFill.localPosition = fillPosition;
    }   
}
