using UnityEngine;

[CreateAssetMenu(fileName = "basketData", menuName = "NewBasketData")]
public class BasketData : ScriptableObject
{
    public FruitData.FruitType basketFruitType;
    public GameObject addedBerryVFX;
    public Material particleMaterial;
}
