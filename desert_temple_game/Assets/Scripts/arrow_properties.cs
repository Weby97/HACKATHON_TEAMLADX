using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_properties : MonoBehaviour
{
    Vector3 velocity = new Vector3(0,2f,0);
    float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
      Destroy(gameObject, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
      Destroy(gameObject);
    }
}
