using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Random;

public class arrow_trap : MonoBehaviour
{
    public bool check_for_arrow_trap;
    public GameObject arrow;
    public GameObject arrow_2;
    // Start is called before the first frame update
    void Start()
    {
        check_for_arrow_trap = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (check_for_arrow_trap)
        {
          fire_arrows();
        }
    }

    void fire_arrows()
    {
      Debug.Log("firing");
      float which_wall = Random.Range(0,1);
      // if (which_wall < 0.5f)
      // {
        Vector3 new_position = new Vector3(-16f, Random.Range(1.5f,3), Random.Range(9,17));
        Vector3 arrow_rotation = new Vector3(0,270,3);
        Instantiate(arrow, new_position, Quaternion.Euler(arrow_rotation));
      // }
      // else
      // {

        Vector3 new_position_2 = new Vector3(-22f, Random.Range(1.5f,3), Random.Range(9,17));
        Vector3 arrow_rotation_2 = new Vector3(-3,90,0);
        Instantiate(arrow_2, new_position_2, Quaternion.Euler(arrow_rotation_2));
      // }
    }

    void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        check_for_arrow_trap = true;
      }
    }
}
