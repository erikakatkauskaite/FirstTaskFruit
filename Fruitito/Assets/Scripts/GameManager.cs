using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnWin;
    public static int currentBerriesCount;
    [SerializeField]
    private GameSettingsData gameSettingsData;
    [SerializeField]
    private BerrySpawner berrySpawner;

    private void Start()
    {        
        currentBerriesCount = 0;
        Basket.OnBerryCollected += CountBerriesAndCheckIfWon;
    }

    public void CountBerriesAndCheckIfWon()
    {
        CountBerries();
        CheckIfWon();
    }

    private void OnDestroy()
    {
        Basket.OnBerryCollected -= CountBerriesAndCheckIfWon;
    }

    public void CountBerries()
    {
        currentBerriesCount++;
    }

    public void CheckIfWon()
    {
        if(currentBerriesCount == gameSettingsData.maxBerries)
        {
            OnWin?.Invoke();
            berrySpawner.StopSpawning();
        }
    }
}
