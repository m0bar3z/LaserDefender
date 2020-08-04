using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage = 100f;

    public float GetDamage()
    {
        return Damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
