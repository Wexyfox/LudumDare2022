using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range;
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Unity required fields")]
    public string enemyTag = "Enemy";
    public GameObject projectilePrefab;
    public Transform projectileSpawnTransform;
    public Transform rotationalPart;

    void Start()
    {
        InvokeRepeating("TargetSearch", 0f, 0.3f);
    }

    private void TargetSearch()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float temporaryY = enemy.transform.position.y - transform.position.y;
            temporaryY = temporaryY * 2;
            Vector3 isometricScaledPosition = new Vector3(enemy.transform.position.x, transform.position.y + temporaryY);
            float distanceToEnemy = Vector3.Distance(transform.position, isometricScaledPosition);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 pos = rotationalPart.position;
        Vector3 dir = target.position - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotationalPart.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    private void Shoot()
    {
        GameObject projectileSpawned = (GameObject)Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        ProjectileHoming projectile = projectileSpawned.GetComponent<ProjectileHoming>();

        if (projectile != null)
        {
            projectile.TargetAquired(target);
        }
    }
}
