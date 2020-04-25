using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int start = 0;
    private int game = 1;
    private int gameover = 2;

    private CandyManager candyManagerScript;

    private AudioSource audio;
    public AudioClip clickSound;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == game)
        {
            candyManagerScript = GameObject.Find("candy").GetComponent<CandyManager>();
        }
    }

    public void gameOver()
    {
        PlayerPrefs.SetInt("prevscore", candyManagerScript.score);
        Thread.Sleep(500);
        SceneManager.LoadScene(gameover);
    }

    public void restart()
    {
        SceneManager.LoadScene(start);
    }

    public void startGame()
    {
        audio.PlayOneShot(clickSound);
        Thread.Sleep(500);
        SceneManager.LoadScene(game);
    }

    public void exit()
    {
        Application.Quit();
    }
}
