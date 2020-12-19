using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnWin;
    public static int currentBerriesCount;
    //public static float progressStartValue;
    [SerializeField]
    private BerrySpawner berrySpawner;

    private const int MIN_BERRIES = 0;
    private const int MAX_BERRIES = 4;

    private void Start()
    {        
        currentBerriesCount = MIN_BERRIES;

        Basket.OnBerryCollected += CountBerriesAndCheckIfWon;
    }

    public void CountBerriesAndCheckIfWon()
    {
        CountBerries();
        CheckIfWon();
    }

    public void CountBerries()
    {
        currentBerriesCount++;
    }

    public void CheckIfWon()
    {
        if(currentBerriesCount == MAX_BERRIES)
        {
            OnWin?.Invoke();
            berrySpawner.StopSpawning();
        }
    }
}
