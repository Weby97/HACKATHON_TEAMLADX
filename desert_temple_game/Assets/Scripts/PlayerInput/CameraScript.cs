using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves camera to given parent object
public class CameraScript : MonoBehaviour
{

    [SerializeField] private Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.position;
    }
}
