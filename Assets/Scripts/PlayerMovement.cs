using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float gravityForce;
    [SerializeField] float jumpForce;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    private int playerIndex;

    private bool isGrouded;


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
        isGrouded = true;
        playerIndex = GetComponent<PlayerStats>().playerIndex;
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("HorizontalP" + playerIndex.ToString());
        float verticalMovement = Input.GetAxis("VerticalP" + playerIndex.ToString());
        Vector3 direction = (planet.transform.position - transform.position).normalized;

        transform.rotation = Quaternion.FromToRotation(transform.up, -direction) * transform.rotation; //mantiene il player orientato verso la superfice

        rigidBody.AddForce(direction * gravityForce, ForceMode.Acceleration);

        rigidBody.AddTorque(transform.up * horizontalMovement * rotationSpeed, ForceMode.VelocityChange);

        rigidBody.AddForce(transform.forward * verticalMovement * movementSpeed, ForceMode.Impulse);

    }

    private void Update()
    {
        if (Input.GetButtonDown("JumpP"+playerIndex)&&isGrouded)
        {
            Debug.Log("Player Jump : " + playerIndex);
            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrouded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Planet>())
        {
            isGrouded = true;
        }
    }

}
