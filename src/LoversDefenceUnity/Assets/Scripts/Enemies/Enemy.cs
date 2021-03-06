using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Required Attributes")]
    public float speed;
    public int bountyValue = 3;
    public int health = 7;
    public int damage = 1;

    private LivesController lifeScript;
    private Transform target;
    private int waypointIndex = 0;

    private void Start()
    {
        target = Waypoints.waypointTransforms[0];
        lifeScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<LivesController>();
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        waypointIndex++;
        if (waypointIndex < Waypoints.waypointTransforms.Length)
        {
            target = Waypoints.waypointTransforms[waypointIndex];
        }
        else
        {
            lifeScript.LoseLives(damage);
            Destroy(gameObject);
        }
    }

    public int DestroyBountyReturn()
    {
        return bountyValue;
    }

    public bool DestroyTest(int damageTaken)
    {
        health = health - damageTaken;

        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
