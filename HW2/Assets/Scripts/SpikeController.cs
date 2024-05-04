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

    public TMP_Text messageText;
    public PlayerController playerControllerScript;

    void Start()
    {
        GameObject parentObject = GameObject.FindGameObjectWithTag("Trap");
        messageText = FindObjectOfType<TMP_Text>();
        playerControllerScript = FindObjectOfType<PlayerController>();

        // Get all the spikes
        if (parentObject != null)
        {
            // Get all children of the parent GameObject
            int childCount = parentObject.transform.childCount;
            spikes = new Transform[childCount];

            // Iterate through each child of the parent GameObject
            for (int i = 0; i < childCount; i++)
            {
                Transform child = parentObject.transform.GetChild(i);
                // Optionally, check for specific name or component of the child GameObject
                if (child.name.Contains("Spike"))
                {
                    // Store the transform component of the spike GameObject
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

        // Store start and end positions for each spike
        startPositions = new Vector3[spikes.Length];
        endPositions = new Vector3[spikes.Length];
        for (int i = 0; i < spikes.Length; i++)
        {
            startPositions[i] = spikes[i].position;
            endPositions[i] = startPositions[i] + Vector3.up * moveDistance;
        }

        // Start the spikes movement coroutine
        StartCoroutine(MoveSpikes());
    }

    IEnumerator MoveSpikes()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while (true)
        {
            // Move the spikes up
            yield return MoveSpikesToPositions(endPositions);

            // Move the spikes down
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
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            KillPlayer(other.gameObject);
        }
    }

    void KillPlayer(GameObject player)
    {
        messageText.color = Color.red;
        messageText.text = "Player killed!";

        if (playerControllerScript != null)
        {
            playerControllerScript.DisableMovement();
        }
        StartCoroutine(RestartGameAfterDelay(3f));
    }
    IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartGame();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
