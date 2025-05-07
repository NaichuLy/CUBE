using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCubeHole : MonoBehaviour
{
    private GameObject _currentBox = null;

    public bool IsSlotOccupied()
    {
        return _currentBox != null;
    }

    public void PlaceCube(GameObject cube)
    {
        if (_currentBox == null)
        {
            _currentBox = cube;
            cube.transform.SetParent(transform);
            cube.transform.position = transform.position;
            cube.transform.rotation = transform.rotation;
            cube.GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Cube placed in the slot.");
        }
    }

    public void RemoveCube()
    {
        if (_currentBox != null)
        {
            _currentBox.GetComponent<Rigidbody>().isKinematic = false;
            _currentBox = null;
            Debug.Log("Cube removed from the slot.");
        }
    }
}
