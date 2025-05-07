using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuCameraAnim camerAnim;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();

    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToGuide()
    {
        camerAnim.GetComponent<MenuCameraAnim>().GoingToGuide();
    }
    public void GoToMenu()
    {
        camerAnim.GetComponent<MenuCameraAnim>().GoingToMenu();
    }
}
