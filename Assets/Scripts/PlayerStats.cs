using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    Text lblHealth;
    [SerializeField]
    Text lblRepair;
    [SerializeField]
    Image imgCollectorObject;
    [SerializeField]
    GameObject objectSpawn;
    public int playerIndex;
    public int health;
    public int maxHealth = 100;
    public int repair;
    

    bool isDead
    {
        get
        {
            return health <= 0;
        }
    }
    void Start()
    {
        health = maxHealth;
        repair = 100;
        lblHealth.GetComponent<Text>().text = "Life:" + health + "%";
        lblRepair.GetComponent<Text>().text = "Repair:" + repair + "%";
        imgCollectorObject.GetComponent<Image>().enabled = false;
    }

    public void Damage(int damage)
    {
        if (isDead) { return; }
        health -= damage;
        if (health<=0)
        {
            Die();
        }
        lblHealth.GetComponent<Text>().text = "Life:"+health+"%";
    }

    [ContextMenu("Die")]
    void Die()
    {
        Vector3 posTemp = transform.position;
        Respawn();
        if (GameManager.objOnScene)
        {
            return;
        }
        else
        {
            Instantiate(objectSpawn, posTemp, Quaternion.identity);
            GameManager.objOnScene = true;
        }
    }
    void Respawn()
    {
        health = maxHealth;
        lblHealth.GetComponent<Text>().text = "Life:" + health + "%";
        transform.localPosition = Vector3.zero;
        imgCollectorObject.GetComponent<Image>().enabled = false;
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
    }

    private void OnCollisionEnter(Collision other)
    {
        bool repairObject = imgCollectorObject.GetComponent<Image>().enabled;
        if (other.gameObject.CompareTag("ShipP"+playerIndex)&&repairObject)
        {
            repair += 25;
            lblRepair.GetComponent<Text>().text = "Repair:" + repair;
            imgCollectorObject.GetComponent<Image>().enabled = false;
            GameManager.objOnScene = false;
        }
    }
}
