using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class ScoreManager : MonoBehaviour {



    public void AddDataDisplay(){


    }

    /*public string[] scoreList;
    WorldModel worldmodel;

    int numToCreate;

    public void PopulateArray() {

        GameBuilder1 world = worldmodel.worldRet();
        numToCreate = world.totalDays();

        String curstring;

        for (int i = 0; i < numToCreate; i++) {
            Day cur = world.retDay(i);
            Debug.Log("Tried to access data in populate function");
            // newObj = (GameObject)Instantiate(textprefab, transform);

            curstring = cur.get("day").ToString() + cur.get("budget").ToString() +
                           cur.get("totalSymps").ToString() + cur.get("sickTrtA").ToString() 
                           + cur.get("curedANum").ToString() + cur.get("sickTrtB").ToString() 
                           + cur.get("curedBNum").ToString();

            scoreList[i] = curstring;
            // newObj.GetComponent<Text>().text = curstring;
        }
    }

    public void PrintArray(){
        GameBuilder1 world = worldmodel.worldRet();



        for (int i = 0; i < numToCreate; i++)
        {
            Day cur = world.retDay(i);
            Debug.Log("Tried to access data in print function");
             
            Debug.Log("Printing " + i + "th element of string score list: " + scoreList[i]);
        }
    }

    public string[] retArray(){
        return scoreList;
    }*/

}
