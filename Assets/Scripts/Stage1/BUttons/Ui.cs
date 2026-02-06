using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("play quited");
    }
}
