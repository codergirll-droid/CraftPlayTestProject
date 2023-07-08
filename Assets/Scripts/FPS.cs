using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public static FPS Instance;

    public Weapon currentWeapon;

    public enum GameMode { RaycastMode, ProjectileMode};
    public GameMode gameMode = GameMode.RaycastMode;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(gameMode == GameMode.RaycastMode)
            {
                WeaponManager.Instance.FireRaycast();

            }
            else
            {
                WeaponManager.Instance.FireProjectile();

            }
        }
    }
}
