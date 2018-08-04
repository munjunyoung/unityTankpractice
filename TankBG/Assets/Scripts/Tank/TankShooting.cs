using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class TankShooting : NetworkBehaviour
{
    public Rigidbody shellPrefab;
    public Transform fireTransform;
    public AudioSource shootingAudio;

    private float fireForce = 35f;
    private float coolTime;
    
    private void Start()
    {
        coolTime = 1f;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        coolTime += Time.deltaTime;
        if (coolTime > 0.5f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdFire();
                coolTime = 0;
            }
        }
    }

    [Command]
    void CmdFire()
    {
        Rigidbody shellInstance = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);//, fireTransform.position, fireTransform.rotation);

        shellInstance.velocity = fireForce * fireTransform.forward;
        shootingAudio.Play();
        shellInstance.transform.position = fireTransform.position;
        shellInstance.transform.rotation = fireTransform.rotation;
        NetworkServer.Spawn(shellInstance.gameObject);
        //NetworkServer.SpawnWithClientAuthority(shellInstance.gameObject, connectionToClient);
    }

    /*
   [Command]
    IEnumerator CmdFire()
    {
        m_Fired = true;
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);

        shellInstance.velocity = fireForce * m_FireTransform.forward;
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        NetworkServer.Spawn(shellInstance.gameObject);
        yield return new WaitForSeconds(0.5f);
        m_Fired = false;
    }
 */
}