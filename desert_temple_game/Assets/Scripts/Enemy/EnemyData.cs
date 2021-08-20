using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Enemy Data", menuName="EnemyData")]
public class EnemyData : ScriptableObject
{
    public int health;
    public float speed;
    public float damage;
}
