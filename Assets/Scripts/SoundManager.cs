using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton = null;

    public static SoundManager Instance;

    public AudioSource music;
    public bool isMuted = false;
    public Image musicOn, musicOff;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        music.Play();
        musicOn.gameObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
    }

    public void PlayStop()
    {
        if (isMuted == true)
        {
            music.Play();
            isMuted = false;
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
        }
        else if (isMuted == false)
        {
            music.Stop();
            isMuted = true;
            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
