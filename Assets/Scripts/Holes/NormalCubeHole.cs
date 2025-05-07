using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCubeHole : MonoBehaviour
{
    [SerializeField] private Transform cubeDestination;
    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isOccupied) return; 
        if (!other.CompareTag("Grabbable")) return; 

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

    
        other.transform.SetParent(null);

       
        rb.isKinematic = true;

       
        other.transform.position = cubeDestination.position;
        other.transform.rotation = cubeDestination.rotation;

        
        other.tag = "Untagged"; 
        isOccupied = true;
    }
}
