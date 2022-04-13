using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject item;

    private GameObject currentItem;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem == null) return;
        
        if (Vector3.Distance(currentItem.transform.position, transform.position) > 3)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        currentItem = Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
    }
}
