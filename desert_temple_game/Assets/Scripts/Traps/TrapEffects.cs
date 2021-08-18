using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TrapEffects
{
    //All members must be static

    //private static GameObject prefab;

    //Runs on start
    [RuntimeInitializeOnLoadMethod]
    static void Start()
    {
        //References go here
        //Reference must be from a static field or an asset in the Assets/Resources folder using Resources.Load method
        //Path name starts from resources so Assets/Resources/Prefabs/Test Object.prefab becomes Prefabs/Test Object.prefab
        //Do not include the file extention so Prefabs/Test Object.prefab becomes Prefabs/Test Object which is the final path

        //prefab = Resources.Load("Prefabs/Test Object") as GameObject;
    }

    //Effects must be public
    //If instantiating GameObjects use Object.Instantiate(), Instantiate() does not exist
    public static void Slow(Transform trap, Transform other)
    {
        Debug.Log("Slowed!!");
    }

    public static void Damage(Transform trap, Transform other)
    {
        Debug.Log("Damaged!!");
    }

    public static void Poison(Transform trap, Transform other)
    {
        Debug.Log("Poisoned!!");
    }

    public static void Fire(Transform trap, Transform other)
    {
        Debug.Log("Fire!!");
    }
}
