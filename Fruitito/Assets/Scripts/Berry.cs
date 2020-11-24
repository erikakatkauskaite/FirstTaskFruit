using System.Collections;
using UnityEngine;

public class Berry : MonoBehaviour
{
    [SerializeField]
    private GameObject[] berryPrefab;
    private Vector3 screenBoundaries;
    private bool collectedAll;
    private int randomBerryIndex;
    private Camera mainCamera;

    private const float RESPAWN_TIME = 0.5f;
    private const float BOUNDARIES_OFFSET_X = 0.5f;
    private const float BOUNDARIES_OFFSET_Y = 1.0f;
    private const int MIN_RANDOM_INDEX = 0;
    private const int MAX_RANDOM_INDEX = 3;
    private const int BERRY_Z_POSITION = 1;

    private void Awake()
    {
        mainCamera = Camera.main;
        collectedAll = false;

        GetScreenBoundaries();

        StartCoroutine(nameof(SpawnBerryObject));
    }

    private void InstantiateBerryObject(int index)
    {
        GameObject _newBerry = Instantiate(berryPrefab[index]) as GameObject;

        _newBerry.transform.position = new Vector3(Random.Range(-screenBoundaries.x + BOUNDARIES_OFFSET_X, screenBoundaries.x - BOUNDARIES_OFFSET_X), Random.Range(BOUNDARIES_OFFSET_X, screenBoundaries.y - BOUNDARIES_OFFSET_Y), BERRY_Z_POSITION);
    }

    private void Update()
    {
        GetScreenBoundaries();
    }

    public void StopSpawning()
    {
        collectedAll = true;
        StopCoroutine(nameof(SpawnBerryObject));
    }

    IEnumerator SpawnBerryObject()
    {
        while (!collectedAll)
        {
            yield return new WaitForSeconds(RESPAWN_TIME);
            GetRandomIndex();
            InstantiateBerryObject(randomBerryIndex);
        }
    }

    private void GetRandomIndex()
    {
        randomBerryIndex = Random.Range(MIN_RANDOM_INDEX, MAX_RANDOM_INDEX);
    }

    private void GetScreenBoundaries()
    {
        screenBoundaries = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }
}
