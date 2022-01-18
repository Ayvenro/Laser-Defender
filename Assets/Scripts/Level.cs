using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }
    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
