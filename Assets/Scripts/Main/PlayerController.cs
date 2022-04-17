using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private const float Speed = 5.0f;
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;

    [SerializeField]
    private GameObject _itemHolderLocation;

    public GameObject HeldItem;

    public GameObject NearbyItem;

    private DeliveryZone _deliveryZone;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _deliveryZone = GameObject.Find("DeliveryZone").GetComponent<DeliveryZone>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (HeldItem != null)
            {
                // Drop item
                DropItem();
            }
            else if (NearbyItem != null)
            {
                // Pickup nearby item
                PickUpItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _deliveryZone.TurnInOrder();
        }

    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalInput) > 0 || Mathf.Abs(horizontalInput) > 0)
        {
            _playerAnimator.SetFloat("Speed_f", 0.3f);
        }
        else
        {
            _playerAnimator.SetFloat("Speed_f", 0f);
        }
        
        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput);
        
        _playerRigidbody.velocity = inputDir * Speed;

        if (HeldItem == null) return;

        HeldItem.transform.position = _itemHolderLocation.transform.position;

    }

    void HandleRotation()
    {
        // Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        
        // Get the Screen position of the mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        // Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void PickUpItem()
    {
        HeldItem = NearbyItem;
        HeldItem.GetComponent<Rigidbody>().detectCollisions = false;
        NearbyItem = null;
    }
    
    void DropItem()
    {
        HeldItem.GetComponent<Rigidbody>().detectCollisions = true;
        HeldItem = null;
    }
}
