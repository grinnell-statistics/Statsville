using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    
	public void Begin ()
	{
		string pid = GameObject.Find ("PlayID").GetComponent<Text> ().text;
		string gid = GameObject.Find ("GroupID").GetComponent<Text> ().text;
		GameObject.Find ("PlayerData").GetComponent<PlayerData> ().playerID = pid;
        GameObject.Find("PlayerData").GetComponent<PlayerData>().groupID = gid;
		
	}

    public void OnChange()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().togglePval = !GameObject.Find("PlayerData").GetComponent<PlayerData>().togglePval;
    }

}
