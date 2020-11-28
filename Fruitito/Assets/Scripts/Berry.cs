using UnityEngine;

public class Berry : MonoBehaviour
{
    public enum FruitType
    {
        Blueberry, Peach, Straweberry
    }
    [SerializeField]
    GameObject touchParticles;
    [SerializeField]
    GameObject wrongParticles;
    [SerializeField]
    private FruitType fruitType;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Material berryVFXMaterial;
    private int randomSpriteIndex;
    private float startPositionX;
    private float startPositionY;
    private bool onHold;
    private Vector3 initialPosition;
    private Collider2D berryCollider;
    private Camera mainCamera;
    private int maxRandomIndex;
    private float currentSpeedValue;
    private Vector3 initialScale;
    private float currentScaleValue;
    private bool correctBasket;
    private bool berryMaximized;
    private bool letGo;

    private const float SCALE_SPEED         = 0.001f;
    private const float ACCELERATION_VALUE  = 1f;
    private const float BERRY_VFX_DELAY     = 1f;
    private const int BERRY_Z_POSITION      = 1;
    private const int MIN_RANDOM_INDEX      = 0;
    private const string BASKET_LAYER       = "Basket";
    private const string PROPERTY_TEXTURE   = "_MainTex";

    private Texture particleTexture;

    private void Start()
    {
        particleTexture = GetComponent<SpriteRenderer>().sprite.texture;

        letGo = false;
        onHold = false;
        berryMaximized = false;
        correctBasket = false;

        maxRandomIndex = sprites.Length;
        randomSpriteIndex = GetRandomIndex();
        GetComponent<SpriteRenderer>().sprite = sprites[randomSpriteIndex];   
        berryCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;

        initialPosition = transform.position;
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;    
    }

    private void Update()
    {
        if (!berryMaximized)
        {
            MaximizeBerry();
        }        

        if (onHold)
        {
            if (!letGo)
            {
                berryVFXMaterial.SetTexture(PROPERTY_TEXTURE, particleTexture);
                GameObject _touchParticlesInstance = (GameObject)Instantiate(touchParticles, transform.position, Quaternion.identity);
                Destroy(_touchParticlesInstance, BERRY_VFX_DELAY);

                letGo = true;
            }
            
            Vector3 _mousePosition;
            _mousePosition = Input.mousePosition;
            _mousePosition = mainCamera.ScreenToWorldPoint(_mousePosition);

            gameObject.transform.localPosition = new Vector3(_mousePosition.x - startPositionX, _mousePosition.y - startPositionY, BERRY_Z_POSITION);
        }
        else
        {
            letGo = false;
            if (correctBasket)
            {
                MinimizeBerry();
            }
            else
            {
                ReturnBerry();
            }
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
        onHold = false;
        
        Collider2D collider = Physics2D.OverlapCircle(transform.position, berryCollider.bounds.extents.x, LayerMask.GetMask(BASKET_LAYER));

        if (collider != null)
        {
            Basket _foundBasket = collider.GetComponent<Basket>();

            if (_foundBasket != null)
            {
                if (fruitType == _foundBasket.basketFruitType)
                {
                    correctBasket = true;
                    _foundBasket.AddBerry();
                    GameManager.Instance.CheckIfWon();
                }
                else
                {
                    berryVFXMaterial.SetTexture(PROPERTY_TEXTURE, particleTexture);
                    GameObject _wrongParticlesInstance = (GameObject)Instantiate(wrongParticles, transform.position, transform.rotation);
                    Destroy(_wrongParticlesInstance, BERRY_VFX_DELAY);
                }
            }
        }
    }

    private void ReturnBerry()
    {
        currentSpeedValue += ACCELERATION_VALUE * Time.deltaTime; 

        Vector3 _currentPosition = transform.position;
        _currentPosition = Vector3.MoveTowards(_currentPosition, initialPosition, currentSpeedValue * Time.deltaTime);
        transform.position = _currentPosition;
    }

    private int GetRandomIndex()
    {
        return Random.Range(MIN_RANDOM_INDEX, maxRandomIndex);
    }

    private void MaximizeBerry()
    {
        if (transform.localScale.x < initialScale.x)
        {
            currentScaleValue += SCALE_SPEED;
            Vector3 _currentScale = new Vector3(currentScaleValue, currentScaleValue, currentScaleValue);
            transform.localScale = _currentScale;
        }
        else
        {
            berryMaximized = true;
            currentScaleValue = 0;
        }
    }

    private void MinimizeBerry()
    {
       if(transform.localScale.x < 0)
       {
           Destroy(gameObject);
       }
       else
       {
           currentScaleValue += SCALE_SPEED;
           Vector3 _currentScale = new Vector3(initialScale.x - currentScaleValue, initialScale.y - currentScaleValue, initialScale.z - currentScaleValue);
           transform.localScale = _currentScale;
       }     
    }
}
