using UnityEngine;

public class Basket : MonoBehaviour
{
    public Berry.FruitType basketFruitType;
    [SerializeField]
    private GameObject addedBerryVFX;
    [SerializeField]
    private Material particleMaterial;
    [SerializeField]
    private GameManager gameManager;

    private const string PROPERTY_TEXTURE = "_MainTex";
    private const float ADDED_BERRY_VFX_DELAY = 5f;

    public void AddBerry()
    {
        addedBerryVFX.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        GameObject _addedBerryVFXInstance = Instantiate(addedBerryVFX, transform.position, Quaternion.identity);
        Destroy(_addedBerryVFXInstance, ADDED_BERRY_VFX_DELAY);
        gameManager.CountBerries();
    }
}
