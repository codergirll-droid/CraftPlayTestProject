using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    WeaponManager.WeaponName weaponName;
    public int ammo;
    public float damage;
    public float frequency;
    public float reloadTime;
    public GameObject bulletObject;
}
