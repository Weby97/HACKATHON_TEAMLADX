using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    [Header("Ammo")]
    [SerializeField] int magazineSize;
    [SerializeField] int reserveSize;

    [Header("Stats")]
    [SerializeField] bool automatic;
    [SerializeField] float rateOfFire;
    //[SerializeField] float recoil;
    [SerializeField] float range;
    [SerializeField] AnimationCurve damageModifier;

    private bool magEmpty;
    private int ammoInMag;
    private int ammoInReserve;

    private GameObject _camera;


    private void Awake()
    {
        if(GameObject.Find("Player").GetComponent<PlayerScript>().weapons.Contains(gameObject))
        {
            transform.parent = GameObject.Find("Gun Transform").transform;
            transform.position = transform.parent.transform.position;
            transform.rotation = transform.parent.transform.rotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera");
        ammoInMag = magazineSize;
        ammoInReserve = reserveSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (ammoInMag <= 0)
        {
            magEmpty = true;
        }
    }

    void OnFire()
    {
        
        if (!automatic && !magEmpty)
        {
            Shoot();

            return;
        }

        StartCoroutine(autofire());


    }

    void StopFire()
    {
        StopCoroutine(autofire());
    }

    IEnumerator autofire()
    {
        while (!magEmpty)
        {
            yield return new WaitForSeconds(rateOfFire);

            Shoot();

        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.parent.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyScript es = hit.collider.gameObject.GetComponent<EnemyScript>();
                es.health -= (int)damageModifier.Evaluate(hit.distance / range);
                Debug.Log("enemy hit");
            }
        }
        //ApplyRecoil();
        ammoInMag--;
        Debug.Log(ammoInMag);
        Debug.Log(ammoInReserve);
    }

    //Couldn't get recoil to work sorry
    /*void ApplyRecoil()
    {
        _camera.transform.Rotate(new Vector3(recoil, 0, 0));
    }*/

    void Reload()
    {
        if (ammoInReserve < magazineSize)
        {
            ammoInMag += ammoInReserve;
            ammoInReserve = 0;
        }
        else
        {
            ammoInReserve -= magazineSize - ammoInMag;
            ammoInMag += magazineSize - ammoInMag;        }

    }

    void Refill(int ammo)
    {
        ammoInReserve += ammo;
        if (ammoInReserve > reserveSize)
            ammoInReserve = reserveSize;
        Reload();
    }
}
