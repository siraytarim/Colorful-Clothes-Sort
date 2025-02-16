using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartToGame : MonoBehaviour
{
  // public AudioSource playSound;
    public void StartGame()
    {
       // playSound.Play();
       Debug.Log("ög");
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
