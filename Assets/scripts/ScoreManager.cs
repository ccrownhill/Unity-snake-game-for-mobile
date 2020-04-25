using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreBoard;
    public Text highscoreBoard;

    void Start()
    {
        int score = PlayerPrefs.GetInt("prevscore", 0);
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreBoard.text = string.Format("{0:000}", score);
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highscoreBoard.text = string.Format("New Highscore: {0:000}", score);
        }
        else
        {
            highscoreBoard.text = string.Format("Highscore: {0:000}", highscore);
        }
    }
}
