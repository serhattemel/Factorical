using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingsButton()
    {
        
    }
}
