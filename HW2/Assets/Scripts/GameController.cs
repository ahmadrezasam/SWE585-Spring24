using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject player;
    private GameObject keyObject;

    public static TMP_Text messageText;

    private bool haveTheKey = false;
    public bool hasWon = false;

    public static bool hasLost = false;

    public float pickupRange = 2f;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Door") && haveTheKey)
    //    {
    //        WinGame();
    //    }
    //    else
    //    {
    //        KillPlayer(player);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            keyObject = other.gameObject;
            messageText.text = "Press 'E' to pick up the key";
        }

        if (other.CompareTag("Door") && haveTheKey)
        {
            WinGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == keyObject)
        {
            keyObject = null;
            messageText.text = "";
        }
    }

    void Start()
    {
        GameObject textObject = GameObject.FindGameObjectWithTag("MainText");
        messageText = textObject.GetComponent<TMP_Text>();


        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (keyObject == null)
        {
            keyObject = GameObject.FindGameObjectWithTag("Key");
        }

    }

    private void Update()
    {
        if (!hasWon)
        {
            if (keyObject != null && Input.GetKeyDown(KeyCode.E))
            {
                if (haveTheKey)
                {
                    DropKey();
                }
                else if (keyObject.activeSelf && IsPlayerInRange())
                {
                    PickUpKey();
                }
            }
        }
        if (hasWon && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private bool IsPlayerInRange()
    {
        if (player != null && keyObject != null)
        {
            float distance = Vector3.Distance(player.transform.position, keyObject.transform.position);
            return distance <= pickupRange;
        }
        return false;
    }

    private void PickUpKey()
    {
        haveTheKey = true;
        keyObject.SetActive(false);
        messageText.text = "Key picked up!";
    }

    private void DropKey()
    {
        haveTheKey = false;
        keyObject.SetActive(true);
        keyObject.transform.position = transform.position + transform.forward;
        messageText.text = "Key dropped!";
    }

    private void WinGame()
    {
        hasWon = true;
        hasLost = true;
        messageText.color = Color.green;
        messageText.text = "You Won! Press 'R' to restart.";
    }

    public void KillPlayer(GameObject player)
    {
        messageText.color = Color.red;
        messageText.text = "You Lost! Press 'R' to restart.";
        hasLost = true;
    }

    public static void RestartGame()
    {
        hasLost = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}