using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameWinMenu;

    private void OnEnable()
    {
        Player.OnPlayerWin += EnableGameWInMenu;            
    }
    private void OnDisable()
    {
        Player.OnPlayerWin -= EnableGameWInMenu;
    }
    public void EnableGameWInMenu()
    {
        gameWinMenu.SetActive(true);
    }

    public void RestardGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
