using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject crosshair;
    [Range(0,1)]
    public float crosshairLerpValue = 0.5f;
    public TMP_Text ammoCountTxt;

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

    private void Update()
    {
        if (FPS.Instance.isDeviceTouchscreen)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    crosshair.transform.position = new Vector3(touch.position.x, touch.position.y, crosshair.transform.position.z);

                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    crosshair.transform.position = Vector3.Lerp(crosshair.transform.position, 
                        new Vector3(touch.position.x, touch.position.y, crosshair.transform.position.z), crosshairLerpValue);

                }

            }
        }
        else
        {
            crosshair.transform.position = Vector3.Lerp(crosshair.transform.position, Input.mousePosition, crosshairLerpValue);

        }
    }

    public void ChangeGameMode(int val)
    {
        if(val == 0)
        {
            FPS.Instance.gameMode = FPS.GameMode.RaycastMode;
        }
        else
        {
            FPS.Instance.gameMode = FPS.GameMode.ProjectileMode;

        }
    }
    public void UpdateAmmoCount(string ammoCount)
    {
        ammoCountTxt.text = ammoCount;
    }
}
