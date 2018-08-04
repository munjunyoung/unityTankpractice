using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {
    public GameObject tankTurret;
    public GameObject canvas;
    private GameObject cam;
    
    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            //GetComponent<TankCamera>().enabled = true;
            cam = GameObject.Find("MainCam");
            cam.transform.parent = tankTurret.transform;
            cam.transform.localPosition = new Vector3(0, 2f, -5f);
            cam.transform.localRotation = Quaternion.Euler(3.5f, 0, 0);

            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            
            for(int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = Color.red;
            }   
        }

        if(!isLocalPlayer)
        {
            canvas.SetActive(false);
        }
	}
}
