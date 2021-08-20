using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private static PlayerData playerSettings;

    [HideInInspector] public int health;
    [HideInInspector] public float speed;

    public GameObject defaultWeapon;
    [HideInInspector] public GameObject currentWeapon;

    public List<GameObject> weapons;


    private void Awake()
    {
        playerSettings = Resources.Load<PlayerData>("DefaultPlayerSettings");
        health = playerSettings.health;
        speed = playerSettings.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(defaultWeapon);
        currentWeapon = defaultWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            weapons.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.SetParent(GameObject.Find("Gun Transform").transform);
            collision.gameObject.transform.position = GameObject.Find("Gun Transform").transform.position;
            collision.gameObject.transform.rotation = GameObject.Find("Gun Transform").transform.rotation;
        }
        if (collision.gameObject.CompareTag("Ammo"))
        {
            currentWeapon.SendMessage("Refill", collision.gameObject.GetComponent<AmmoScript>().ammoAmount);
            collision.gameObject.SetActive(false);
        }
    }

    void SwitchWeapon()
    {
        currentWeapon.SetActive(false);
        int index = weapons.IndexOf(currentWeapon);
        if ( index + 1 == weapons.Count)
        {
            currentWeapon = weapons[0];
        }else
        {
            currentWeapon = weapons[index + 1];
        }
        currentWeapon.SetActive(true);
    }
}
