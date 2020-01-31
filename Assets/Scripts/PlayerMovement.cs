using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int playerIndex;
    [SerializeField] float gravityForce;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;


    Vector3 direction = new Vector3(0, 0, 0);

    Planet planet = null;

    Rigidbody rigidBody;
    

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        planet = FindObjectOfType<Planet>();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("HorizontalP1");// + playerIndex.ToString());
        float verticalMovement = Input.GetAxis("VerticalP1"); //+ playerIndex.ToString());
        Vector3 direction = (planet.transform.position - transform.position).normalized;
        rigidBody.AddForce(direction * gravityForce, ForceMode.Acceleration);

        rigidBody.AddTorque(transform.up * horizontalMovement * rotationSpeed, ForceMode.VelocityChange);

        rigidBody.AddForce(transform.forward * verticalMovement * movementSpeed, ForceMode.Impulse);

        Debug.Log("vertical: " + verticalMovement);
    }

}
