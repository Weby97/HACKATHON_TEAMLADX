using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clashing_walls_trap : MonoBehaviour
{
    public GameObject wall_1;
    public GameObject wall_2;
    public bool trigger_detection;
    float speed = 2;
    public bool move;
    Vector3 initial_position;
    Vector3 initial_rotation;
    // Start is called before the first frame update
    void Start()
    {
      trigger_detection = false;
      initial_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
      initial_rotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger_detection)
        {
          clash_walls();
          move = true;
        }
        else{
          transform.position = initial_position;
          transform.rotation = Quaternion.Euler(initial_rotation);
        }
    }

    void clash_walls()
    {
      if (move)
      {
        Vector3 clash_1 = new Vector3(-0.1f,0,0);
        wall_1.transform.Translate(clash_1 * speed * Time.deltaTime);

        Vector3 clash_2 = new Vector3(0.1f,0,0);
        wall_2.transform.Translate(clash_2 * speed * Time.deltaTime);
      }
      // else{
      //   Vector3 stationary = new Vector3(0,0,0);
      //
      // }
    }

    void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        Debug.Log("Collision detected!");
        move = false;
        trigger_detection = true;
      }
    }
}
