using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Bullet bullet;   
    int playerIndex;

    private void Start()
    {
        playerIndex = GetComponent<PlayerStats>().playerIndex;
    }

    private void Update()
    {
        if (Input.GetButtonDown("FireP" + playerIndex))
        {
            Rigidbody bulletRB = Instantiate(bullet.gameObject, transform.position + (transform.forward), transform.rotation).GetComponent<Rigidbody>();
            bulletRB.AddForce(transform.forward * bullet.force, ForceMode.VelocityChange);
        }
    }
}
