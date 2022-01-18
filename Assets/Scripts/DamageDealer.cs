using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;

    public int GetDamage() => damage;

    public void Hit()
    {
        Destroy(gameObject);
    }
}
