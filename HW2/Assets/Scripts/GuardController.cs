using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float moveSpeed = 5f;
    private int currentPatrolIndex = 0;
    public float detectionRange = 3f;
    public LayerMask obstacleMask;

    public GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (patrolPoints.Count == 0)
        {
            Debug.LogError("No patrol points assigned to the guard.");
            enabled = false;
            return;
        }

        MoveToNextPatrolPoint();
    }

    private void Update()
    {
        if (!GameController.hasLost) 
        {        
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
            {
                MoveToNextPatrolPoint();
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    if (HasLineOfSight(hitCollider.gameObject))
                    {
                        gameController.KillPlayer(hitCollider.gameObject);
                    }
                }
            }
        }

        if (GameController.hasLost && Input.GetKeyDown(KeyCode.R))
        {
            GameController.RestartGame();
        }

    }

    private void MoveToNextPatrolPoint()
    {

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;

        // Check if the guard has reached the last patrol point -> reverse
        if (currentPatrolIndex == 0)
        {
            patrolPoints.Reverse();
        }

        StartCoroutine(MoveToPosition(patrolPoints[currentPatrolIndex].position));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private bool HasLineOfSight(GameObject target)
    {
        RaycastHit hit;
        Vector3 direction = (target.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRange, obstacleMask))
        {
            // If the raycast hits an obstacle before reaching the target, return false
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }

        // If the raycast doesn't hit any obstacles, or hits the player, return true
        return true;
    }
}
