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
        if(Time.realtimeSinceStartup > nextShot && currentAmmo > 0)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

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
        }


    }

    #endregion

    #region PROJECTILE MODE

    public void FireProjectile()
    {
        if (Time.realtimeSinceStartup > nextShot && currentAmmo > 0)
        {
            Vector3 aimedPos = mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward);
            GameObject bullett = CreateBullet(currentWeapon, mainCam.transform.position);
            bullett.GetComponent<Rigidbody>().AddForce((aimedPos - mainCam.transform.position) * currentWeapon.bulletSpeed);

            nextShot = currentWeapon.frequency + Time.realtimeSinceStartup;
            currentAmmo--;
            Debug.Log("Aimed pos is " + aimedPos);

            if (currentAmmo == 0)
            {
                currentAmmo = currentWeapon.ammo;
                nextShot += currentWeapon.reloadTime;
                Debug.Log("Reloading");
            }
        }
    }

    GameObject CreateBullet(Weapon weapon, Vector3 bulletPosition)
    {
        GameObject bullet = ObjectPooling.Instance.GetPooledObject(weapon);
        bullet.transform.position = bulletPosition;
        bullet.SetActive(true);

        IEnumerator c = DisableBullet(bullet);
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
