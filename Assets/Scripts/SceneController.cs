using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Change the scene to the string passed in
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    //Reloads the current scene we are in
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Loads title scene, scene is named "Title" exactly
    public void ToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    //Gets name of active scene
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //Closes game executable entirely
    public void QuitGame()
    {
        Application.Quit();
    }
}
