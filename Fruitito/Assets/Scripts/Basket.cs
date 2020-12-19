using UnityEngine;
using System;

public class Basket : MonoBehaviour
{
    public static event Action OnBerryCollected;
    public BasketData basket;

    private const string PROPERTY_TEXTURE = "_MainTex";
    private const float ADDED_BERRY_VFX_DELAY = 5f;

    public void AddBerry()
    {
        basket.addedBerryVFX.GetComponent<ParticleSystemRenderer>().material = basket.particleMaterial;
        GameObject _addedBerryVFXInstance = Instantiate(basket.addedBerryVFX, transform.position, Quaternion.identity);
        Destroy(_addedBerryVFXInstance, ADDED_BERRY_VFX_DELAY);
        OnBerryCollected?.Invoke();
    }
}
