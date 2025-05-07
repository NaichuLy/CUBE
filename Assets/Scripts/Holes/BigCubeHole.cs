using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCubeHole : MonoBehaviour
{
    [SerializeField] private Transform bigCubeDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BigCube"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                other.transform.position = bigCubeDestination.position;
                other.tag = "Untagged";

                Debug.Log("Big Cube placed into hole!");
            }
        }
    }
}
