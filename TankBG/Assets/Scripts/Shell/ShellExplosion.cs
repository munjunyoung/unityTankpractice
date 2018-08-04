using UnityEngine;
using UnityScript;
using UnityEngine.Networking;

public class ShellExplosion : NetworkBehaviour
{

    public LayerMask tankMask;
    private GameObject sonObject;
    private ParticleSystem explosionParticles;
    AudioSource explosionAudio;
    private float maxDamage = 55f;
    private float explosionForce = 200f;
    float explosionRadius = 5f;     //폭발 반경         
    NetworkIdentity isPlayer;
    private void Awake()
    {
        sonObject = transform.Find("ShellExplosion").gameObject;
        explosionParticles = sonObject.GetComponent<ParticleSystem>();
        explosionAudio = sonObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);//폭발반경, 다른탱크
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody) //타겟을 만나지 않았을경우 아래 생략
                continue;

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            var targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (!targetHealth)
                continue;
            float damage = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damage);

            //지정한 position을 중심으로 설정한 radius반경내에 있는 collider 객체 추출)
        }

        explosionParticles.transform.parent = null;
        explosionParticles.Play();
        explosionAudio.Play();

        Destroy(explosionParticles.gameObject, 3f);
        Destroy(this.gameObject, 2f);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTargetPos = targetPosition - transform.position;

        float explosionDistance = explosionToTargetPos.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max(0f, damage);
        return damage;
    }
}