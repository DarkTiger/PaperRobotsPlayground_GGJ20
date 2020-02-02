using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Text lblHealth;
    [SerializeField] Text lblRepair;
    [SerializeField] Text lblAmmo;
    [SerializeField] Image imgCollectorObject;
    [SerializeField] Image imgWeapons;
    [SerializeField] GameObject objectSpawn;
    [SerializeField] GameObject[] shipModelVariants;
    
    public int playerIndex;
    public int health;
    public int maxHealth = 100;
    public int repair;

    PlayerAttack playerAttack;
    int currentShipVariant = 0;


    public bool isDead
    {
        get
        {
            return health <= 0;
        }
    }
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        UpdateAmmoRemain(playerAttack.currentAmmo);
        health = maxHealth;
        repair = 0;
        lblHealth.GetComponent<Text>().text = "Life: " + health + "%";
        lblRepair.GetComponent<Text>().text = "Repair: " + repair + "%";
        UpdateWeaponsHUD();
        imgCollectorObject.GetComponent<Image>().enabled = false;
    }

    public void Damage(int damage, Bullet bullet)
    {
        if (isDead) { return; }
        health -= damage;
        if (health<=0)
        {
            Die(1, bullet);
        }
        lblHealth.GetComponent<Text>().text = "Life: "+health+"%";
    }

    void Die(int i, Bullet bullet)
    {
        Vector3 posTemp = transform.position + transform.up;      
        
        StartCoroutine(Respawn());

        if (!GameManager.objOnScene && GameManager.objOwner != this && bullet.owner == this) { return; }

        if (!GameManager.objOnScene && ((GameManager.objOwner == this) || GameManager.objOwner == null))
        {
            Vector3 planetPos = FindObjectOfType<Planet>().transform.position;
            Transform objectInstantiated = Instantiate(objectSpawn, posTemp, Quaternion.identity).transform;
            objectInstantiated.up = (objectInstantiated.position - planetPos).normalized;
            GameManager.objOnScene = true;
            GameManager.objOwner = null;
        }
    }

    IEnumerator Respawn()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(3f);
        
        health = maxHealth;
        lblHealth.GetComponent<Text>().text = "Life: " + health + "%";
        transform.localPosition = Vector3.zero;
        imgCollectorObject.GetComponent<Image>().enabled = false;
        playerAttack.currentBullet = playerAttack.defaultBullet;
        UpdateAmmoRemain(0);
        UpdateWeaponsHUD();

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    public void activeGear(bool active)
    {
        if (active)
        {
            imgCollectorObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            imgCollectorObject.GetComponent<Image>().enabled = false;
        }

        GameManager.objOnScene = false;
        GameManager.objOwner = this;
    }

    private void OnCollisionEnter(Collision other)
    {
        bool repairObject = imgCollectorObject.GetComponent<Image>().enabled;
        if (other.gameObject.CompareTag("ShipP"+playerIndex)&&repairObject)
        {
            repair += 25;                    

            switch (repair)
            {
                case 50:
                    currentShipVariant = 1;
                    shipModelVariants[0].SetActive(false);
                    shipModelVariants[1].SetActive(true);
                    break;
                case 100:
                    currentShipVariant = 2;
                    shipModelVariants[1].SetActive(false);
                    shipModelVariants[2].SetActive(true);
                    break;
            }

            lblRepair.GetComponent<Text>().text = "Repair: " + repair + "%";
            imgCollectorObject.GetComponent<Image>().enabled = false;
            GameManager.objOnScene = false;
            GameManager.objOwner = null;
        }
    }

    public void UpdateWeaponsHUD()
    {
        imgWeapons.sprite = playerAttack.currentBullet.weaponSprite;
    }

    public void UpdateAmmoRemain(int ammoRemain)
    {
        string ammoRemainString = ammoRemain.ToString();
        if (playerAttack.currentBullet.type == Bullet.Type.Default)
        {
            ammoRemainString = "∞";
            lblAmmo.text = "Ammo: " + ammoRemainString + "/" + ammoRemainString;
        }
        else
        {
            lblAmmo.text = "Ammo: " + ammoRemainString + "/" + playerAttack.currentBullet.startAmmo;
        }
        UpdateWeaponsHUD();
    }
}
