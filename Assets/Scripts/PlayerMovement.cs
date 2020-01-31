using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float gravityForce;

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
        Vector3 direction = (planet.transform.position - transform.position).normalized;
        rigidBody.AddForce(direction * gravityForce, ForceMode.Acceleration);
    }

}
