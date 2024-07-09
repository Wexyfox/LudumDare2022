using UnityEngine;

public class ProjectileHoming : MonoBehaviour
{
    private Transform target;
    private Enemy enemyScript;
    private int bountyValue;

    [Header("Attributes")]
    public float speed = 5f;
    public int damage = 3;

    public void TargetAquired(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        enemyScript = target.GetComponent<Enemy>();
        if (enemyScript.DestroyTest(damage))
        {
            bountyValue = enemyScript.DestroyBountyReturn();
            EconomySystem.Instance.EarnMoney(bountyValue);
            Destroy(target.gameObject);
        }
        Destroy(gameObject);
    }
}
