using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public enum WeaponName { Ak47, Uzi, Awm};

    public List<Weapon> weapons= new List<Weapon>();

    Camera mainCam;
    float nextShot;
    Weapon currentWeapon;
    int currentAmmo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        mainCam = Camera.main;
        currentWeapon = FPS.Instance.currentWeapon;
        nextShot = currentWeapon.frequency;
        currentAmmo = currentWeapon.ammo;
    }

    #region RAYCAST MODE

    public void FireRaycast()
    {
        if(Time.time > nextShot && currentAmmo > 0)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);

            }
            nextShot += currentWeapon.frequency;
            currentAmmo--;

            if(currentAmmo == 0)
            {
                currentAmmo = currentWeapon.ammo;
                nextShot += currentWeapon.reloadTime;
                Debug.Log("Reloading");
            }
        }


    }

    #endregion

    public void ChangeWeapon(int val)
    {
        currentWeapon = weapons[val];

        Debug.Log("Changed to " + currentWeapon.weaponName.ToString() + " val : " + val);
        nextShot = currentWeapon.frequency;
        currentAmmo = currentWeapon.ammo;

    }

}
