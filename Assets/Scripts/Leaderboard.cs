using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    int LeaderboardID = 19868;  // from lootlocker console

    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    string LeaderboardKey = "globalHighScore";  // from lootlocker console

    // Start is called before the first frame update
    void Start()
    {
        
    }


    //make public to use it in Player script
    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
                                                                        ///????
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, LeaderboardKey, (response) =>    //LeaderboardID
        {
            if(response.success)
            {
                Debug.Log("Succesfully upload score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }


    public IEnumerator GetLeaderboardRoutine()     //FetchTopHighScoresRoutine
    {
        bool done = false;                       //from top, bottom
        LootLockerSDKManager.GetScoreList(LeaderboardKey, 10, 0, (response) =>    //LeaderboardID
        {
            if(response.success)
            {
                Debug.Log("Succesfully got leaderboard");

                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames; 
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
