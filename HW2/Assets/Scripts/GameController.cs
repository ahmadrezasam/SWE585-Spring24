using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject player;
    private GameObject keyObject;

    public TMP_Text messageText;
    private bool haveTheKey = false;
    public bool hasWon = false;

    public float pickupRange = 2f;

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

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player2");
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
        messageText.text = "You Win! Press 'R' to restart.";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}