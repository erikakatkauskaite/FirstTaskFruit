using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;
    private Vector3 initialScale;
    private bool won;
    private float currentScaleValue;
    private float currentScaleValue2;
    private bool restartPressed;

    private const float SCALE_SPEED = 0.007f;
    private const int DEGREES       = 720;

    private void Start()
    {
        won = false;
        restartPressed = false;
        initialScale = restartButton.transform.localScale;
        restartButton.transform.localScale = Vector3.zero;
        restartButton.SetActive(false);
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

    public void CheckIfWon()
    {
        won = true;
    }

    public void CheckIfPressedRestart()
    {
        restartPressed = true;
    }
}
