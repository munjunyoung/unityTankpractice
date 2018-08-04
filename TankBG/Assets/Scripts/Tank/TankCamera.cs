using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankCamera : NetworkBehaviour{
   
    private GameObject cam;
    private Camera tankCam;

    public GameObject tankTurretPosition;
    public GameObject aimPosition;

    private bool TPP;
    public Text aim;

    private void Awake()
    {
        cam = GameObject.Find("MainCam");
    }

    private void Start()
    {
        aim.text = "+";
        tankCam = cam.gameObject.GetComponent<Camera>();
        tankCam.fieldOfView = 60f;
        TPP = true;
    }

    void Update () {
        if (!isLocalPlayer)
            return;
        transCameraAngle();
	}

    void transCameraAngle()
    {
        if (Input.GetMouseButtonDown(1) && TPP)
        {
            TPP = false;
            tankCam.fieldOfView = 45;
            aim.text = ".";
        }
        else if (Input.GetMouseButtonDown(1) && !TPP)
        {
            TPP = true;
            tankCam.fieldOfView = 60;
            aim.text = "+";
        }
    }
}
