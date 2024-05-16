using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipal : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("hola");

    }
}
