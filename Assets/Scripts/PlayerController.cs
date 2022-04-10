using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private const float Speed = 5.0f;
    private Rigidbody _playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput);
        
        _playerRigidbody.velocity = inputDir * Speed;
    }
}
