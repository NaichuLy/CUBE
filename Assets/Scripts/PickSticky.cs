using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSticky : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float grabRange = 2f;
    [SerializeField] private float dropRange = 2f;

    public bool _isHolding = false;
    private GameObject _currentBox = null;
    private float lastDropTime = -1f;

    private bool _playerInTrigger = false;

    private void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!_isHolding)
            {
                TryGrabBox();
            }
            else
            {
                TryDropBoxInSlot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrigger = true;
            Debug.Log("Player entered trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrigger = false;
            Debug.Log("Player exited trigger.");
        }

    }
        private void TryGrabBox()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, grabRange);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("StickyCube"))
            {
                _currentBox = col.gameObject;
                _currentBox.GetComponent<Rigidbody>().isKinematic = true;
                _currentBox.transform.SetParent(grabPoint);
                _currentBox.transform.localPosition = Vector3.zero;
                _isHolding = true;
            }
        }
    }

    private void TryDropBoxInSlot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, dropRange);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("CubeSlot"))
            {
                StickyCubeHole slot = col.GetComponent<StickyCubeHole>();
                if (slot != null && !slot.IsSlotOccupied())
                {
                    _currentBox.transform.SetParent(null);
                    slot.PlaceCube(_currentBox);

                    _currentBox = null;
                    _isHolding = false;
                    lastDropTime = Time.time;
                    return;
                }
            }
        }
    }

    public GameObject GetHeldStickyCube()
    {
        return _isHolding ? _currentBox : null;
    }

    public void ForceDropStickyCube()
    {
        if (_currentBox != null)
        {
            _currentBox.transform.SetParent(null);
            _currentBox.GetComponent<Rigidbody>().isKinematic = false;
            _currentBox = null;
        }

        _isHolding = false;
        lastDropTime = Time.time;
    }

}
