using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //EnemyData is in Resources
    public EnemyData enemyData;

    public bool overrideEnemyData;

    [HideInInspector] public int overrideHealth;
    [HideInInspector] public float overrideSpeed;
    [HideInInspector] public float overrideDamage;

    [HideInInspector] public float health;
    private float speed, damage;


    //TODO: ADD ENEMY MOVEMENT AND AI

    private void Awake()
    {
        if (overrideEnemyData)
        {
            health = overrideHealth;
            speed = overrideSpeed;
            damage = overrideDamage;
        }else
        {
            health = enemyData.health;
            speed = enemyData.speed;
            damage = enemyData.damage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyData.health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController.playerSettings.health -= (int) damage;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
