using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Bullet bullet;   
    int playerIndex;
    int currentAmmo = 0;
    float lastFire = 0;


    private void Start()
    {
        playerIndex = GetComponent<PlayerStats>().playerIndex;
        currentAmmo = bullet.startAmmo;
    }

    private void Update()
    {
        if (GameManager.isPaused) { return; }
        if (Time.time > lastFire + bullet.fireDelay)
        {
            if (bullet.type == Bullet.Type.SMG)
            {
                if (currentAmmo > 0 && Input.GetButton("FireP" + playerIndex))
                {
                    Instantiate(bullet.gameObject, transform.position + (transform.forward), transform.rotation);
                    lastFire = Time.time;
                    currentAmmo--;
                }
            }
            else
            {
                if ((currentAmmo > 0 || bullet.type == Bullet.Type.Default) && Input.GetButtonDown("FireP" + playerIndex))
                {
                    if (bullet.type == Bullet.Type.Default || bullet.type == Bullet.Type.Sniper)
                    {
                        Instantiate(bullet.gameObject, transform.position + (transform.forward), transform.rotation);
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                            Instantiate(bullet.gameObject, transform.position + (transform.forward) + randomOffset, transform.rotation);
                        }
                    }
                    
                    lastFire = Time.time;
                    currentAmmo--;
                }
            }
        }
    }
}
