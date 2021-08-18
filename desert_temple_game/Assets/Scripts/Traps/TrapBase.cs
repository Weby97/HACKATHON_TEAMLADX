using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;


public class TrapBase : MonoBehaviour
{

    MethodInfo[] methodInfos;

    public int effectSelection;

    public enum TrapActivationMode
    {
        COLLISION, RADIUS, LINE
    }

    public TrapActivationMode trapActivationMode;

    public bool continuous;
    public float effectRate;

    private bool effectEnabled;

    
    public float sphereRadius;
    public float range;
    public Vector3 rayDirection;

    // Start is called before the first frame update
    void Start()
    {
        methodInfos = typeof(TrapEffects).GetMethods(BindingFlags.Public | BindingFlags.Static);

        Array.Sort(methodInfos,
            delegate (MethodInfo methodInfo1, MethodInfo methodInfo2)
            { return methodInfo1.Name.CompareTo(methodInfo2.Name); });

        if (trapActivationMode == TrapActivationMode.RADIUS)
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = sphereRadius;
        }

        if (trapActivationMode == TrapActivationMode.RADIUS)
            CheckPlayerInRange();

    }

    // Update is called once per frame
    void Update()
    {
    }

    //COLLISION
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && trapActivationMode == TrapActivationMode.COLLISION)
        {
            methodInfos[effectSelection].Invoke(typeof(TrapEffects), new object[] { gameObject.transform, collision.transform });
            effectEnabled = true;
            if (continuous)
            {
                StartCoroutine(continuousEffect(methodInfos[effectSelection], collision.collider));
            }
            return;
        }
        effectEnabled = false;
    }

    //RADIUS
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && trapActivationMode == TrapActivationMode.RADIUS)
        {
            methodInfos[effectSelection].Invoke(typeof(TrapEffects), new object[] { gameObject.transform, other.transform });
            effectEnabled = true;
            if (continuous)
            {
                StartCoroutine(continuousEffect(methodInfos[effectSelection], other));
            }
            return;
        }
        effectEnabled = false;
    }

    //LINE
    private void CheckPlayerInRange()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirection, out hit, range))
        {
            Debug.Log("Range");
            if (hit.transform.gameObject.tag == "Player")
            {
                methodInfos[effectSelection].Invoke(typeof(TrapEffects), new object[] { gameObject.transform, hit.transform });
                effectEnabled = true;
                if (continuous)
                {
                    StartCoroutine(continuousEffect(methodInfos[effectSelection], hit.collider));
                }
                return;
            }
        }
        effectEnabled = false;
    }

    IEnumerator continuousEffect(MethodInfo effect, Collider other)
    {
        while (effectEnabled)
        {
            yield return new WaitForSeconds(effectRate);
            effect.Invoke(typeof(TrapEffects), new object[] { gameObject.transform, other.transform });
        }

        StopCoroutine(continuousEffect(effect, other));
    }
}
