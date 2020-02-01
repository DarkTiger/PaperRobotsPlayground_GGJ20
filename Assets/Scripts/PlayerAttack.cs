using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int damage = 1;
    int playerIndex;

    private void Start()
    {
        playerIndex = GetComponent<PlayerStats>().playerIndex;
    }

    private void Update()
    {
        if (Input.GetButtonDown("FireP" + playerIndex))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, LayerMask.GetMask("Player")))
            {
                hit.collider.GetComponent<PlayerStats>().Damage(damage);
            }
        }
    }
}
