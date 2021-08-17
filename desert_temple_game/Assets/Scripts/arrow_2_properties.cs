using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_2_properties : MonoBehaviour
{
    Vector3 velocity = new Vector3(0,-2f,0);
    // Start is called before the first frame update
    void Start()
    {
      Destroy(gameObject, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity);
    }

    void OnCollisionEnter(Collision other)
    {
      Destroy(gameObject);
    }
}
