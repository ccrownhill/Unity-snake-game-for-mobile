using UnityEngine;
using UnityEngine.UI;

public class CandyManager : MonoBehaviour
{
    public int score = 0;
    public int highscore = 23;

    public GameObject candyPrefab;
    public Grid grid;
    public GameObject head;
    private PlayerController playerControllerScript;

    public Text scoreBoard;
    public Text highscoreBoard;

    private AudioSource audio;
    public AudioClip scoreSound;

    void Start()
    {
        audio = GameObject.Find("snake").GetComponent<AudioSource>();
        highscore = PlayerPrefs.GetInt("highscore", 0);
        playerControllerScript = GameObject.Find("snake").GetComponent<PlayerController>();
        spawnNewInstance();
        scoreBoard.text = string.Format("{0:000}", score);
        highscoreBoard.text = string.Format("Highscore: {0:000}", highscore);
    }

    void FixedUpdate()
    {
        checkForCollision();
    }

    private void checkForCollision()
    {
        Transform candy = GetComponentsInChildren<Transform>()[1];
        Vector3Int gridPos = grid.LocalToCell(candy.position);
        Vector3Int headGridPos = grid.LocalToCell(head.transform.position);
        if (gridPos == headGridPos)
        {
            audio.PlayOneShot(scoreSound);
            playerControllerScript.len++;
            score++;
            scoreBoard.text = string.Format("{0:000}", score);
            Destroy(candy.gameObject);
            spawnNewInstance();
        }
    }

    private void spawnNewInstance()
    {
        Vector3Int randomGridPos = new Vector3Int(Random.Range(-21, 4), Random.Range(-12, 12), 0);
        while (randomGridPos == grid.LocalToCell(head.transform.position))
            randomGridPos = new Vector3Int(Random.Range(-21, 4), Random.Range(-12, 12), 0);
        Instantiate(candyPrefab, grid.GetCellCenterLocal(randomGridPos), Quaternion.identity, transform);
    }
}
