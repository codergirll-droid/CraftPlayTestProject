using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject crosshair;
    [Range(0,1)]
    public float crosshairLerpValue = 0.5f;

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
        crosshair.transform.position = Vector3.Lerp(crosshair.transform.position, Input.mousePosition, crosshairLerpValue);
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
}
