using System;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public static event Action OnCollected;
    public FruitData fruitData;
    private int randomSpriteIndex;
    private float startPositionX;
    private float startPositionY;
    private bool onHold;
    private Vector3 initialPosition;
    private Collider2D berryCollider;
    private Camera mainCamera;
    private int maxRandomIndex;
    private Vector3 initialScale;
    private bool correctBasket;
    private bool letGo;
    private float currentTime;
    private Vector3 mouseUpPosition;
    private ParticleSystem touchParticlesInstance;
    private ParticleSystem wrongParticlesInstance;

    private const float SCALE_SPEED             = 1.7f;
    private const float MINIMIZE_SCALE_SPEED    = 2.0f;
    private const float RETURN_TIME             = 1.0f;
    private const int BERRY_Z_POSITION          = 1;
    private const int MIN_RANDOM_INDEX          = 0;
    private const string BASKET_LAYER           = "Basket";

    private void Start()
    {
        letGo = false;
        onHold = false;
        correctBasket = false;

        maxRandomIndex = fruitData.sprites.Length;
        randomSpriteIndex = GetRandomIndex();
        GetComponent<SpriteRenderer>().sprite = fruitData.sprites[randomSpriteIndex];   
        berryCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;

        initialPosition = transform.position;
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;

        touchParticlesInstance = fruitData.touchParticles;
        wrongParticlesInstance = fruitData.wrongParticles;

        MaximizeBerry();
    }

    private void Update()
    {            
        if (onHold)
        {
            currentTime = 0;
            if (!letGo)
            {
                touchParticlesInstance.GetComponent<ParticleSystemRenderer>().material = fruitData.berryVFXMaterial;
                Instantiate(touchParticlesInstance, this.transform.position, Quaternion.identity);

                letGo = true;
            }           
            Vector3 _mousePosition;
            _mousePosition = Input.mousePosition;
            _mousePosition = mainCamera.ScreenToWorldPoint(_mousePosition);

            gameObject.transform.localPosition = new Vector3(_mousePosition.x - startPositionX, _mousePosition.y - startPositionY, BERRY_Z_POSITION);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePosition;
            _mousePosition = Input.mousePosition;
            _mousePosition = mainCamera.ScreenToWorldPoint(_mousePosition);

            startPositionX = _mousePosition.x - this.transform.localPosition.x;
            startPositionY = _mousePosition.y - this.transform.localPosition.y;

            onHold = true;
        }
    }

    private void OnMouseUp()
    {
        mouseUpPosition = transform.position;
        onHold = false;
        
        Collider2D collider = Physics2D.OverlapCircle(transform.position, berryCollider.bounds.extents.x, LayerMask.GetMask(BASKET_LAYER));

        if (collider != null)
        {
            Basket _foundBasket = collider.GetComponent<Basket>();

            if (_foundBasket != null)
            {
                if (fruitData.fruitType == _foundBasket.basket.basketFruitType)
                {
                    MinimizeBerry();
                    correctBasket = true;
                    _foundBasket.AddBerry();
                    OnCollected?.Invoke();
                }
                else
                {
                    wrongParticlesInstance.GetComponent<ParticleSystemRenderer>().material = fruitData.berryVFXMaterial;
                    Instantiate(wrongParticlesInstance, this.transform.position, Quaternion.identity);
                    ReturnBerry();
                }
            }
        }
        else
        {
            ReturnBerry();
        }
    }

    private void ReturnBerry()
    {
        LeanTween.move(this.gameObject, initialPosition, RETURN_TIME);
        letGo = false;
    }

    private int GetRandomIndex()
    {
        return UnityEngine.Random.Range(MIN_RANDOM_INDEX, maxRandomIndex);
    }

    private void MaximizeBerry()
    {
        LeanTween.scale(this.gameObject, initialScale, SCALE_SPEED).setEase(fruitData.animationCurve);
    }

    private void MinimizeBerry()
    {
        LeanTween.scale(this.gameObject, Vector2.zero, MINIMIZE_SCALE_SPEED).setOnComplete(DestroyBerry);
    }

    private void DestroyBerry()
    {
        Destroy(this.gameObject);
    }
}
