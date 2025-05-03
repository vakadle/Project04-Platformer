using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    [Header("Patrol Settings")]
    [Tooltip("Transforms between which the enemy will patrol.")]
    public Transform[] waypoints;
    [Tooltip("Movement speed in units per second.")]
    public float speed = 2f;

    private int currentWaypoint = 0;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        // if no waypoints assigned, bail out
        if (waypoints == null || waypoints.Length < 2)
        {
            Debug.LogWarning("EnemyController: Need at least 2 waypoints assigned.", this);
            return;
        }

        // draw line in Scene view for debugging
        Debug.DrawLine(transform.position, waypoints[currentWaypoint].position, Color.red);

        // compute direction toward current target waypoint
        Vector2 currentPos = rb.position;
        Vector2 targetPos = waypoints[currentWaypoint].position;
        Vector2 dir = (targetPos - currentPos).normalized;

        // move the Rigidbody2D
        Vector2 newPos = currentPos + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        // set speed parameter for Animator (to switch between Idle/Run)
        float vel = dir.magnitude * speed;
        anim.SetFloat("Speed", vel);

        // flip sprite based on movement direction
        if (dir.x > 0.1f)      sr.flipX = false;
        else if (dir.x < -0.1f) sr.flipX = true;

        // once close enough to waypoint, advance to the next one
        if (Vector2.Distance(currentPos, targetPos) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}