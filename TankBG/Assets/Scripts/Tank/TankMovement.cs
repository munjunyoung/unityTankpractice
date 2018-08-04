using UnityEngine;
using UnityEngine.Networking;

public class TankMovement : NetworkBehaviour
{
    //Move
    private float PlayerSpeed;
    private float PlayerMaxSpeed;
    private Vector3 playerMovement;

    //Audio
    private AudioSource TankAudio;
    public AudioClip EngineIdle;
    public AudioClip EngineDriving;
    
    private void Awake()
    {
        PlayerSpeed = 3.0f;
        PlayerMaxSpeed = 5.0f;

        TankAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Move();
        EngineAudio();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        playerMovement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(playerMovement * PlayerMaxSpeed * Time.deltaTime);
        else
            transform.Translate(playerMovement * PlayerSpeed * Time.deltaTime);
    }
    

    void EngineAudio()
    {
        if(Mathf.Abs(playerMovement.x)<0.1f&&Mathf.Abs(playerMovement.z)<0.1f)
        {
            if (TankAudio.clip == EngineDriving)
            {
                TankAudio.clip = EngineIdle;
                TankAudio.Play();
            }
        }
        else
        {
            if (TankAudio.clip == EngineIdle)
            {
                TankAudio.clip = EngineDriving;
                TankAudio.Play();
            }
        }
    }
}