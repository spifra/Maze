using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float movingSpeed;
    public float rotationSpeed;
    private Vector3 movement;
    private Vector3 rotation;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {

            Vector3 newPos = transform.TransformDirection(movement);
            rigidbody.MovePosition(transform.position + newPos * movingSpeed * Time.deltaTime);

            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        }

    }

    void OnMovement(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        movement = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private void OnRotation(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();

        rotation = new Vector3(0, inputValue.x, 0);

    }

}
