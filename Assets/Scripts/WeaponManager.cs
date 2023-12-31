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
    public Weapon currentWeapon;
    int currentAmmo;
    RaycastHit hit;
    Ray ray;
    Vector3 aimedPos;
    GameObject createdBullet;
    ObjectPooling objectPooling;
    IEnumerator c;
    GameObject bullet;

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
        UIManager.Instance.UpdateAmmoCount(currentAmmo.ToString());
        objectPooling = ObjectPooling.Instance;
    }

    #region RAYCAST MODE

    public void FireRaycast(Vector3 input)
    {
        if(Time.realtimeSinceStartup > nextShot && currentAmmo > 0)
        {
            ray = mainCam.ScreenPointToRay(input);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
                //check if you hit the right tag, eg.enemy
                    //DamageEnemy(currentWeapon.damage);

            }
            nextShot = currentWeapon.frequency + Time.realtimeSinceStartup;
            currentAmmo--;

            if(currentAmmo == 0)
            {
                currentAmmo = currentWeapon.ammo;
                nextShot += currentWeapon.reloadTime;
                Debug.Log("Reloading");
            }
            UIManager.Instance.UpdateAmmoCount(currentAmmo.ToString());

        }


    }

    #endregion

    #region PROJECTILE MODE

    public void FireProjectile(Vector3 input)
    {
        if (Time.realtimeSinceStartup > nextShot && currentAmmo > 0)
        {
            aimedPos = mainCam.ScreenToWorldPoint(input + Vector3.forward);
            createdBullet = CreateBullet(currentWeapon, mainCam.transform.position);
            createdBullet.GetComponent<Rigidbody>().AddForce((aimedPos - mainCam.transform.position) * currentWeapon.bulletSpeed);

            nextShot = currentWeapon.frequency + Time.realtimeSinceStartup;
            currentAmmo--;

            if (currentAmmo == 0)
            {
                currentAmmo = currentWeapon.ammo;
                nextShot += currentWeapon.reloadTime;
                Debug.Log("Reloading");
            }
            UIManager.Instance.UpdateAmmoCount(currentAmmo.ToString());

        }
    }

    GameObject CreateBullet(Weapon weapon, Vector3 bulletPosition)
    {
        bullet = objectPooling.GetPooledObject(weapon);
        bullet.transform.position = bulletPosition;
        bullet.SetActive(true);

        c = DisableBullet(bullet);
        StartCoroutine(c);
        return bullet;
    }

    IEnumerator DisableBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(currentWeapon.bulletDestroyTime);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.SetActive(false);
    }

    #endregion

    public void DamageEnemy(float dmg)
    {
        //check if you hit the desired target, decrease health by dmg, can be called from the Bullet script
    }

    public void ChangeWeapon(int val)
    {
        currentWeapon = weapons[val];

        Debug.Log("Changed to " + currentWeapon.weaponName.ToString());
        nextShot = Time.realtimeSinceStartup;
        currentAmmo = currentWeapon.ammo;

    }

}
