using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit " + other.collider.gameObject.name);
        //check if you hit the enemy
            //WeaponManager.Instance.DamageEnemy(WeaponManager.Instance.currentWeapon.damage);

    }
}
