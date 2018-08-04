using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankTurretRotation : NetworkBehaviour  {

    public GameObject tankTurretChild;

    private float rotateSens;
    private float rotationX;
    private float rotationY;
    private float rotationMinY;
    private float rotationMaxY;

    
    private void Start()
    {
        if (isLocalPlayer)
            rotationX = this.transform.rotation.eulerAngles.y; 
        //처음시작할때 스타트포지션의 로테이션을 가져올수 있도록함
        
        rotateSens = 20.0f;
        rotationY = 0.0f;
        rotationMinY = -20.0f;
        rotationMaxY = 45.0f;

    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        TurretTurn();
    }
    //상단 터렛 로테이션 함수
    void TurretTurn()
    {
        rotationX += Input.GetAxis("Mouse X") * rotateSens * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * rotateSens * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, rotationMinY, rotationMaxY);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        tankTurretChild.gameObject.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}
