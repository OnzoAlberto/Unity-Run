using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static bool pause = false;
    float time;
    public GameObject Score, EndGameCanvas,FinalScore;
    private static GameObject finalScore;

    void Start()
    {
        
        Time.timeScale = 0;
        finalScore = FinalScore;
    }

    void Update()
    {
        if(pause)
        Score.GetComponent<Text>().text = "Score: " + ((int)(Time.timeSinceLevelLoad*100)).ToString();

        if (Input.GetKey("p"))
            StopGame();

        if (Input.GetKey("r"))
            ResumeGame();
    }

    public void startgame()
    {
        Time.timeScale = 1;
        pause = true;
    }

    public void StopGame()
    {
        time = Time.timeScale;
        Time.timeScale = 0;
        pause = false;
    }

    public void ResumeGame()
    {
        Time.timeScale =1+ time ;
        pause = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void EndGame()
    {
        Destroy(GameObject.Find("model"));
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
       pause = false;
       finalScore.GetComponent<Text>().text = "Your score is: \n " + ((int)(Time.timeSinceLevelLoad * 100)).ToString();

    }
}
