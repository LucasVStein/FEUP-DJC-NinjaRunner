using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject gameOverText;

    public Text scoreText;
    public float pointsPerSec;
    private float scoreCntr = 0;

    public bool gameOver = false;

    public static GameControl instance;

    public float scrollingVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // point counting
        if(!gameOver) {

            scoreCntr += pointsPerSec * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.Round(scoreCntr);
        }

        if(gameOver && Input.GetKeyDown("space")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayerLost() {
        gameOverText.SetActive(true);
        gameOver = true;
    }
}
