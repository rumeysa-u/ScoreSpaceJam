using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Leaderboard leaderboard;

    int score = 130;


/*
    void SetScoreOnce()
    {
        StartCoroutine(EndGameRoutine());
    }

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        SetScoreOnce();
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        // Limit the framerate to 60
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndGameRoutine()
    {
        //It will freeze for one second and then restart the level
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);

        // coroutine for update leaderboar
        yield return leaderboard.SubmitScoreRoutine(score);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
