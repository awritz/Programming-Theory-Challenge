using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private const float Speed = 5.0f;
    private Rigidbody _playerRigidbody;

    [SerializeField]
    private GameObject _itemHolderLocation;

    public GameObject HeldItem;

    public GameObject NearbyItem;

    private DeliveryZone _deliveryZone;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _deliveryZone = GameObject.Find("DeliveryZone").GetComponent<DeliveryZone>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

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

        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput);
        
        _playerRigidbody.velocity = inputDir * Speed;

        if (HeldItem == null) return;

        HeldItem.transform.position = _itemHolderLocation.transform.position;

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
