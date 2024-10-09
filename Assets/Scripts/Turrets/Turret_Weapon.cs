using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Weapon : MonoBehaviour
{
    // bullet to spawn
    public GameObject projectile;

    // Instantiating the projectile at this point
    public Transform shootPoint;

    // force by which to shoot projectile
    public float force = 100f;

    // Firing rate
    public float shootingDelay = 1f;

    [Space(5)]
    [Header("Additional Options")]
    // For Shooting Multiple Bullets
    public GameObject secondProjectile;
    public Transform secondShootPoint;

    [HideInInspector] public bool canShoot = false;

    IEnumerator Start()
    {
        while (true)
        {
            // Delay before each fire
            yield return new WaitForSeconds(shootingDelay);

            if (canShoot)
            {
                // Instantiate bullet
                GameObject bullet = Instantiate(projectile, shootPoint.position, shootPoint.rotation);

                // Add force to the Instantiated bullet
                bullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * force);

                // Double projectile mode (useful for double weapon's turret)
                if (secondProjectile)
                {
                    GameObject bullet2 = Instantiate(secondProjectile, secondShootPoint.position, secondShootPoint.rotation);
                    bullet2.GetComponent<Rigidbody>().AddForce(secondShootPoint.forward * force);
                }
            }
        }
    }
}
