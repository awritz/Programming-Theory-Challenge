using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            // Debug.Log("Entered item trigger zone " + gameObject.name);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.NearbyItem = gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            // Debug.Log("Exited item trigger zone " + gameObject.name);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            
            if (playerController.NearbyItem != null && playerController.NearbyItem.Equals(gameObject))
            {
                playerController.NearbyItem = null;
            }
        }
    }

    public abstract string GetItemId();
}
