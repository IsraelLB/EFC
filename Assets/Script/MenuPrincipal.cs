using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuPrincipal : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ClickBoton;
    public Button startButton; 
    public Button exitButton; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartButtonClicked()
    {
        startButton.interactable = false;
        audioSource.PlayOneShot(ClickBoton);
        StartCoroutine(LoadNextSceneAfterDelay("SampleScene", 0.9f));
    }

    public void OnExitButtonClicked()
    {
        exitButton.interactable = false;
        audioSource.PlayOneShot(ClickBoton);
        StartCoroutine(QuitGameAfterDelay(1.0f));
        Debug.Log("hola");

    }

    IEnumerator LoadNextSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator QuitGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
        startButton.interactable = true;
    }
}
