using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public TMP_Text messageText;
    private bool isMuted = false;

    private void Start()
    {
        messageText = GameObject.Find("Music").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            ToggleMute();
        }
    }
    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            musicSource.Pause();
            messageText.text = "Press (M) Mute";
        }
        else
        {
            musicSource.UnPause();
            messageText.text = "Press (M) Unmute";
        }
    }
}
