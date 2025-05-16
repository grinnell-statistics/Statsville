using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

//this script sends game data to the server
public class SubmitData : MonoBehaviour {

    public WorldModel world;

	//This Method respond to the click on the submit button
	public void SubmitUpload(){
        DataModel data = world.data;
		StartCoroutine (Upload (data));
	}

    IEnumerator Upload(DataModel data) {

        int gameNum = -1;

        WWW getGameNum = new WWW("https://stat2games.sites.grinnell.edu/php/getgamenum.php");
        yield return getGameNum;

        try {
            gameNum = int.Parse(getGameNum.text);
        }
        catch (Exception e) {
            Debug.Log("Fetching game number failed.  Error message: " + e.ToString());
        }

        for (int index = 0; index < data.day.Count; index++) {

            WWWForm form = new WWWForm();
            form.AddField("Game", gameNum);
            form.AddField("Level", LevelScreen.level);
            form.AddField("PlayerID", data.playerID[index]);
            form.AddField("GroupID", data.groupID[index]);
            form.AddField("Day", data.day[index]);
            form.AddField("Population", data.population[index]);
            form.AddField("Budget", data.budget[index]);
            form.AddField("AvailToTreat", data.availToTreat[index]);
            form.AddField("TreatA", data.aTreat[index]);
            form.AddField("CureA", data.aCure[index]);
            form.AddField("TreatB", data.bTreat[index]);
            form.AddField("CureB", data.bCure[index]);
            form.AddField("CostA", data.aCost[index]);
            form.AddField("CostB", data.bCost[index]);
            form.AddField("SickCost", data.sickCost[index]);
            form.AddField("WinLose", data.winLose[index]);


            WWW www = new WWW("https://stat2games.sites.grinnell.edu/php/sendgameinfo.php", form);
            yield return www;

            if (www.text == "0")
            {
                Debug.Log("Player data created successfully.");
            }
            else
            {
                Debug.Log("Player data creation failed. Error # " + www.text);
            }

        }

        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}



