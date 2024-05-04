using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeController : MonoBehaviour
{
    private Transform[] spikes;

    private Vector3[] startPositions;
    private Vector3[] endPositions;

    public float moveDistance = 1f; // Distance the spikes move up and down
    public float moveSpeed = 1f; // Speed of the spikes' movement
    public float delayBeforeStart = 1f; // Delay before the spikes start moving

    public GameController gameController;

    void Start()
    {
        GameObject parentObject = GameObject.FindGameObjectWithTag("Trap");
        gameController = FindObjectOfType<GameController>();

        if (parentObject != null)
        {
            int childCount = parentObject.transform.childCount;
            spikes = new Transform[childCount];

            for (int i = 0; i < childCount; i++)
            {
                Transform child = parentObject.transform.GetChild(i);
                if (child.name.Contains("Spike"))
                {
                    spikes[i] = child;
                    Collider spikeCollider = child.gameObject.AddComponent<BoxCollider>();
                    spikeCollider.isTrigger = true;
                }
            }
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'TrapParent' found.");
        }

        startPositions = new Vector3[spikes.Length];
        endPositions = new Vector3[spikes.Length];
        for (int i = 0; i < spikes.Length; i++)
        {
            startPositions[i] = spikes[i].position;
            endPositions[i] = startPositions[i] + Vector3.up * moveDistance;
        }

        StartCoroutine(MoveSpikes());
    }

    private void Update()
    {
        if (GameController.hasLost && Input.GetKeyDown(KeyCode.R))
        {
            GameController.RestartGame();
        }
    }

    IEnumerator MoveSpikes()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while (!GameController.hasLost)
        {
            yield return MoveSpikesToPositions(endPositions);

            yield return MoveSpikesToPositions(startPositions);
            yield return new WaitForSeconds(6);
        }
    }

    IEnumerator MoveSpikesToPositions(Vector3[] targetPositions)
    {
        float[] distances = new float[spikes.Length];
        for (int i = 0; i < spikes.Length; i++)
        {
            distances[i] = Vector3.Distance(spikes[i].position, targetPositions[i]);
        }

        while (!ArrayAreCloseEnough(distances, 0.01f))
        {
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].position = Vector3.MoveTowards(spikes[i].position, targetPositions[i], moveSpeed * Time.deltaTime);
                distances[i] = Vector3.Distance(spikes[i].position, targetPositions[i]);
            }
            yield return null;
        }
    }

    bool ArrayAreCloseEnough(float[] distances, float threshold)
    {
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] > threshold)
                return false;
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.KillPlayer(other.gameObject);
        }
    }
}
