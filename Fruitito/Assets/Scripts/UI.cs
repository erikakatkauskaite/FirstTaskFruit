﻿using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameSettingsData gameSettingsData;
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private RectTransform barFill;
    private Vector3 initialScale;
    private bool won;
    private float currentScaleValue;
    private float currentScaleValue2;
    private bool restartPressed;
    private Vector3 fillPosition;
    private static float progressStartValue;

    private const float SCALE_SPEED             = 0.007f;
    private const int DEGREES                   = 720;
    private const float PROGRESS_BAR_END_VALUE  = 0f;

    private void Start()
    {
        progressStartValue = -barFill.rect.width;
        won = false;
        restartPressed = false;
        initialScale = restartButton.transform.localScale;
        restartButton.transform.localScale = Vector3.zero;
        restartButton.SetActive(false);
        IncreaseProgress(GameManager.currentBerriesCount);
        Berry.OnCollected += HandleBerryCollected;
        GameManager.OnWin += SetWon;
    }

    private void OnDestroy()
    {
        Berry.OnCollected -= HandleBerryCollected;
        GameManager.OnWin -= SetWon;
    }

    private void Update()
    {
        if(won)
        {
            MaximizeRestartButton();
        }
        
        if(restartPressed)
        {
            MinimizeRestartButton();
        }
    }

    public void HandleBerryCollected()
    {
        if (GameManager.currentBerriesCount <= gameSettingsData.maxBerries)
        {
            IncreaseProgress(GameManager.currentBerriesCount);
        }
    }

    private void MinimizeRestartButton()
    {
        if (restartButton.transform.localScale.x > 0)
        {
            currentScaleValue2 += SCALE_SPEED;
            Vector3 _currentScale = new Vector3(initialScale.x - currentScaleValue2, initialScale.y - currentScaleValue2, initialScale.z - currentScaleValue2);
            restartButton.transform.localScale = _currentScale;
            restartButton.transform.Rotate(Vector3.forward * -DEGREES * Time.deltaTime);
        }
    }

    private void MaximizeRestartButton()
    {
        restartButton.SetActive(true);
        if (currentScaleValue < initialScale.x)
        {
            currentScaleValue += SCALE_SPEED;
            Vector3 _currentScale = new Vector3(currentScaleValue, currentScaleValue, currentScaleValue);
            restartButton.transform.localScale = _currentScale;
            restartButton.transform.Rotate(Vector3.forward * DEGREES * Time.deltaTime);
        }
    }

    public void SetWon()
    {
        won = true;
    }

    public void CheckIfPressedRestart()
    {
        restartPressed = true;
    }

    private void IncreaseProgress(int count)
    {
        fillPosition.x = progressStartValue + (float)count / gameSettingsData.maxBerries * (PROGRESS_BAR_END_VALUE - progressStartValue);
        barFill.localPosition = fillPosition;
    }
}
