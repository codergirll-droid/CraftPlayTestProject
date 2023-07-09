using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public static FPS Instance;

    public Weapon currentWeapon;

    public enum GameMode { RaycastMode, ProjectileMode};
    public GameMode gameMode = GameMode.RaycastMode;
    public bool isDeviceTouchscreen = false;

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

    private void Start()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            isDeviceTouchscreen = true;
        }
    }

    private void Update()
    {
        if (isDeviceTouchscreen)
        {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (gameMode == GameMode.RaycastMode)
                    {
                        WeaponManager.Instance.FireRaycast(touch.position);

                    }
                    else
                    {
                        WeaponManager.Instance.FireProjectile(touch.position);

                    }
                }

            }

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (gameMode == GameMode.RaycastMode)
                {
                    WeaponManager.Instance.FireRaycast(Input.mousePosition);

                }
                else
                {
                    WeaponManager.Instance.FireProjectile(Input.mousePosition);

                }
            }
        }


    }
}
