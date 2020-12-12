using UnityEngine;

[CreateAssetMenu(fileName = "BlueberryData", menuName = "NewBlueberryData")]
public class FruitData : ScriptableObject
{
    public ParticleSystem touchParticles;
    public ParticleSystem wrongParticles;
    public FruitType fruitType;
    public Sprite[] sprites;
    public Material berryVFXMaterial;
    public AnimationCurve animationCurve;

    public enum FruitType
    {
        Blueberry, Peach, Straweberry
    }
}
