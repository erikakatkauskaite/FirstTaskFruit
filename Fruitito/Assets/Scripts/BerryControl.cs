using UnityEngine;

public class BerryControl : MonoBehaviour
{
    public enum FruitType
    {
        Blueberry, Peach, Straweberry
    }

    [SerializeField]
    private FruitType fruitType;
    [SerializeField]
    private Sprite[] sprites;
    private int randomSpriteIndex;
    private float startPositionX;
    private float startPositionY;
    private bool onHold = false;
    private Vector3 initialPosition;
    private Collider2D berryCollider;
    private Camera mainCamera;

    private const int BERRY_Z_POSITION  = 1;
    private const int MIN_RANDOM_INDEX  = 0;
    private const int MAX_RANDOM_INDEX  = 3;
    private const string BASKET_LAYER   = "Basket";

    private void Start()
    {
        randomSpriteIndex = GetRandomIndex();
        GetComponent<SpriteRenderer>().sprite = sprites[randomSpriteIndex];
        mainCamera = Camera.main;
        berryCollider = GetComponent<Collider2D>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (onHold)
        {
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            gameObject.transform.localPosition = new Vector3(mousePosition.x - startPositionX, mousePosition.y - startPositionY, BERRY_Z_POSITION);
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
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

        if(collider != null)
        {
            Basket _foundBasket = collider.GetComponent<Basket>();

            if (_foundBasket != null)
            {
                if (fruitType == _foundBasket.basketFruitType)
                {
                    _foundBasket.AddBerry();
                    GameManager.Instance.CheckIfWon();
                    Destroy(this.gameObject);
                }
            }
        }
        ReturnBerry();
    }

    private void ReturnBerry()
    {
        transform.position = new Vector3(initialPosition.x, initialPosition.y, BERRY_Z_POSITION);
    }

    private int GetRandomIndex()
    {
        return Random.Range(MIN_RANDOM_INDEX, MAX_RANDOM_INDEX);
    }
}
