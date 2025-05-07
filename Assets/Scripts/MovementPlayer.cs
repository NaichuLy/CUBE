using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";
    private Animator _animator;

    [Header("<color=green>Physics</color>")]
    [SerializeField] Transform _camera;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _runningSpeed = 7f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _grounRayDistance = 0.25f;
    [SerializeField] private LayerMask _groundRayMask;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space; 
    private float _normalSpeed = 3.5f;
    private Rigidbody _rb;
    private Ray _groundRay;

    private Vector3 _dir = Vector3.zero, _posOffset = new();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _normalSpeed = _moveSpeed;
        _animator = GetComponentInChildren<Animator>(); 
    }
    private void Update()
    {

        _dir.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_xAxisName, _dir.x);
        _dir.z = Input.GetAxis("Vertical");
        _animator.SetFloat(_zAxisName, _dir.z);

        _isGrounded = IsGrounded();


        if (Input.GetKeyDown(_jumpKey) && _isGrounded)
        {
            _animator.SetBool("OnJump", _isGrounded);
            _animator.SetBool("isOnAir", _isGrounded);
            Jump();
            Invoke("EndJump", 1f);
        }
        
        
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _moveSpeed = _runningSpeed;
        }
        else
        {
            _moveSpeed = _normalSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (_dir.sqrMagnitude != 0.0f)
        {
            Movement(_dir);
        }
        
    }

    private bool IsGrounded()
    {
        _posOffset = new Vector3(transform.position.x, transform.position.y + 0.1f , transform.position.z);
        _groundRay = new Ray(_posOffset, -transform.up);
        return Physics.Raycast(_groundRay, _grounRayDistance, _groundRayMask);
    }

    private void EndJump() 
    {
        _animator.SetBool("isOnAir", _isGrounded);
    }
    
    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse );
    }
    private void Movement(Vector3 dir)
    {
        _rb.MovePosition(transform.position + (_camera.transform.right * dir.x + transform.forward * dir.z).normalized * _moveSpeed * Time.fixedDeltaTime);
    }

}

