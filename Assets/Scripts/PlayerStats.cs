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
        repair = 0;
        lblHealth.GetComponent<Text>().text = "Life: " + health + "%";
    }

    public void Damage(int damage)
    {
        if (isDead) { return; }
        health -= damage;
        if (health<=0)
        {
            Die();
        }
        lblHealth.GetComponent<Text>().text = "Life: "+health+"%";
    }

    [ContextMenu("Die")]
    void Die()
    {
        Vector3 posTemp = transform.position;
        Respawn();
        Instantiate(objectSpawn,posTemp,Quaternion.identity);
    }
    void Respawn()
    {
        health = maxHealth;
        lblHealth.GetComponent<Text>().text = "Life: " + health + "%";
        transform.localPosition = Vector3.zero;
    }
}
