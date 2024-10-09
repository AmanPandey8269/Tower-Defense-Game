using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int iD;
    public int cost;

    public Transform gunHead;
    public float dampingSpeed = 10f;
    public string targetTag = "Enemy";
    public float shootingDistance = 30f;

    public float seekSpeed = 50f;
    public float rotateAngle = 70f;
    // Internal variables
    Vector3 originalRotation;
    bool isActive;
    Transform target;

    IEnumerator Start()
    {
        // Saving the original rotation of the gun head
        originalRotation = gunHead.localRotation.eulerAngles;

        while (true)
        {
            // Finding the closest enemy
            target = FindClosestEnemy();

            if (target)
            {
                // Checking that the distance from the enemy is in the shooting distance range
                if (Vector3.Distance(transform.position, target.position) <= shootingDistance)
                {
                    // Start attack
                    GetComponent<Turret_Weapon>().canShoot = true;
                    isActive = true;
                }
                else
                {
                    // Stop attach
                    GetComponent<Turret_Weapon>().canShoot = false;
                    isActive = false;
                }
            }
            else
            {
                // The enemy is out of the shooting range
                GetComponent<Turret_Weapon>().canShoot = false;
                isActive = false;
            }
            // Use delay to have better performance (instead of update function)
            yield return new WaitForSeconds(0.3f);
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (target)
            {
                // Setting the Gun Rotation to Towards Target
                Vector3 lookPos = target.position - gunHead.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                gunHead.rotation = Quaternion.Slerp(gunHead.rotation, rotation, Time.deltaTime * dampingSpeed);
            }
        }
        else
        {
            // Weapon head's seek animation (when the enemy is not available or it's out of the shooting distance)
            gunHead.localRotation = Quaternion.Euler(originalRotation.x, Mathf.PingPong(Time.time * seekSpeed, rotateAngle * 2) - rotateAngle, 1f);
        }
    }

    // Finding the closest target with targetTag
    GameObject closest;
    Transform FindClosestEnemy()
    {
        // First find all game object to determine which one is near than others
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(targetTag);
        if (gos.Length == 0)
            return null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if (closest.GetComponent<EnemyAI>().GetCurrentHealth <= 0)
            return null;
        // Return the closest target's transform
        return closest.transform;
    }
}
