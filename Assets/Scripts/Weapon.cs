using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public WeaponManager.WeaponName weaponName;
    public int ammo;
    public float damage;
    public float frequency;
    public float reloadTime;
    public float bulletSpeed;
    public float bulletDestroyTime = 3;
    public GameObject bulletObject;
}
