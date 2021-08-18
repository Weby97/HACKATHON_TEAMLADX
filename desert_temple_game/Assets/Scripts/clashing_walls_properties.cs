using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clashing_walls_properties : MonoBehaviour
{
    Vector3 initial_position;
    Vector3 initial_rotation;
    bool stop_moving;
    // Start is called before the first frame update
    void Start()
    {
      stop_moving = false;
      initial_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
      initial_rotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
      if (stop_moving)
      {
        Debug.Log("stop_moving is true!");
        transform.position = initial_position;
        transform.rotation = Quaternion.Euler(initial_rotation);
      }
    }

    void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("clashing_wall"))
      {
        stop_moving = true;
      }
    }
}
