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
    FPS fpsInstance;
    Touch touch;
    Vector3 newCrosshairPos;

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
        fpsInstance = FPS.Instance;
        newCrosshairPos = new Vector3();
    }

    private void Update()
    {
        if (fpsInstance.isDeviceTouchscreen)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                newCrosshairPos.x = touch.position.x;
                newCrosshairPos.y = touch.position.y;
                newCrosshairPos.z = crosshair.transform.position.z;

                if (touch.phase == TouchPhase.Began)
                {

                    crosshair.transform.position = newCrosshairPos;

                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    crosshair.transform.position = Vector3.Lerp(crosshair.transform.position, 
                        newCrosshairPos, crosshairLerpValue);

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
            fpsInstance.gameMode = FPS.GameMode.RaycastMode;
        }
        else
        {
            fpsInstance.gameMode = FPS.GameMode.ProjectileMode;

        }
    }
    public void UpdateAmmoCount(string ammoCount)
    {
        ammoCountTxt.text = ammoCount;
    }
}
