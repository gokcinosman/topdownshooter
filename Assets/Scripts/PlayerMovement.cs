using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    private Vector2 _movement;
    private Vector2 _mousePos;
    private void Start()
    {
        _rb.gravityScale = 0f; // Disable gravity
        _rb.drag = 0f;        // No drag
        _rb.angularDrag = 0f; // No angular drag
    }
    private void Update()
    {
        _movement.x = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f ? Input.GetAxisRaw("Horizontal") : 0f;
        _movement.y = Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f ? Input.GetAxisRaw("Vertical") : 0f;

        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);

        Vector2 lookDir = _mousePos - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }



}
