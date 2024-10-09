using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Target's tag to apply damage
    public string targetTag = "Enemy";

    // Value of Damage to Apply on the Target
    public float damageValue;

    // Instantiate the damage particle after collision
    public GameObject damageParticle;

    // Destroy the projectile after 5 seconds with out collision
    public float lifeTime = 5f;

    IEnumerator Start()
    {
        // Destroing the projectile after lifeTime value if Enemy not collided
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        // Applying damage to the target's health component
        if (col.transform.tag == targetTag)
        {
            if(col.transform.TryGetComponent(out IHealth component))
                component.GetDamage(damageValue);

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().mass = 0;
            GetComponent<SphereCollider>().enabled = false;
            Vector3 pos = col.transform.GetComponent<EnemyAI>().headPos.position;
            // Instantiating the collision particle
            if (damageParticle)
            {
                GameObject hitParticle = Instantiate(damageParticle, pos, col.transform.rotation, col.transform.GetComponent<EnemyAI>().headPos);
                //Destroying the Hit Particle
                StartCoroutine(DestroyParticle(hitParticle));
            }
            // Destroying the projectile
            StartCoroutine(DestroyParticle(gameObject));
        }
    }

    //For Destroying all Objects with a Delay
    IEnumerator DestroyParticle(GameObject Particle)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(Particle);
    }
}