using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Bullet defaultBullet;
    [SerializeField] Bullet[] bullets;
    public Bullet currentBullet;
    public int currentAmmo { get; private set;} = 0;
    int playerIndex;
    float lastFire = 0;


    private void Start()
    {
        playerIndex = GetComponent<PlayerStats>().playerIndex;
        currentAmmo = currentBullet.startAmmo;
    }

    private void Update()
    {
        if (GameManager.isPaused) { return; }
        if (Time.time > lastFire + currentBullet.fireDelay)
        {
            if (currentBullet.type == Bullet.Type.SMG)
            {
                if (currentAmmo > 0 && Input.GetButton("FireP" + playerIndex))
                {
                    Bullet bullet = Instantiate(currentBullet.gameObject, transform.position + (transform.forward), transform.rotation).GetComponent<Bullet>();
                    bullet.owner = this;

                    lastFire = Time.time;
                    currentAmmo--;
                    

                    if (currentAmmo == 0)
                    {
                        currentBullet = defaultBullet;
                    }
                    GetComponent<PlayerStats>().UpdateAmmoRemain(currentAmmo);
                }
            }
            else
            {
                if ((currentAmmo > 0 || currentBullet.type == Bullet.Type.Default) && Input.GetButtonDown("FireP" + playerIndex))
                {
                    if (currentBullet.type == Bullet.Type.Default || currentBullet.type == Bullet.Type.Sniper)
                    {
                        Bullet bullet = Instantiate(currentBullet.gameObject, transform.position + (transform.forward*1.5f), transform.rotation).GetComponent<Bullet>();
                        bullet.owner = this;
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Bullet bullet = Instantiate(currentBullet.gameObject, transform.position + (transform.forward), transform.rotation).GetComponent<Bullet>();
                            bullet.owner = this;
                        }
                    }
                    
                    lastFire = Time.time;
                    currentAmmo--;
                    
                    if (currentAmmo == 0)
                    {
                        currentBullet = defaultBullet;
                    }
                    GetComponent<PlayerStats>().UpdateAmmoRemain(currentAmmo);

                }
            }
        }
    }
}
