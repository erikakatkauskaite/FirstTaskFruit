using UnityEngine;

public class Basket : MonoBehaviour
{
    public BerryControl.FruitType basketFruitType;

    public void AddBerry()
    {
        GameManager.Instance.CountBerries();
    }
}
