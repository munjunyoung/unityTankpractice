using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class TankHealth : NetworkBehaviour
{
    public GameObject ExplosionPrefab;
    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;

    private GameObject cam;
    private GameObject camParent;

    public Slider lifeBar;
    const float m_StartingHealth = 100f;
    [SyncVar(hook = "SetHealthUI")]
    public float m_CurrentHealth = m_StartingHealth;
    //private bool gameEnd = false;

    public void TakeDamage(float amount)
    {
        if (!isServer)
            return;
        m_CurrentHealth -= amount;
        if (m_CurrentHealth <= 0f)
        {
            RpcTankExplosion();
            RpcOnDeath();
            
            return;
        }
    }
 
    private void SetHealthUI(float health)
    {
        lifeBar.value = health;
    }

    [ClientRpc]
    private void RpcTankExplosion()
    {
        m_ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        
        //Destroy(m_ExplosionParticles, 5f);
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        if (isLocalPlayer)
        {
            cam = transform.Find("TankRenderers").transform.Find("TankTurret").transform.Find("MainCam").gameObject;
            camParent = GameObject.Find("CamParent");
            cam.transform.position = transform.position + Vector3.up * 12f;
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);
            cam.transform.parent = camParent.transform;
        }
        SetEndText();
       
        GameObject manager = GameObject.Find("GameManager").gameObject;
        manager.GetComponent<Manager>().EndGame();
        NetworkServer.Destroy(this.gameObject);
    }
   
    void SetEndText()
    {
        Text EndGameText = GameObject.Find("EndGameText").GetComponent<Text>();
        if (isLocalPlayer)
        {
            EndGameText.text = "YOU LOSE";
            EndGameText.color = Color.red;
        }
        else
        {
            EndGameText.text = "YOU WIN";
            EndGameText.color = Color.blue;
        }
    }


}