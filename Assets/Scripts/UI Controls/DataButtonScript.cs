using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;



public class DataButtonScript : MonoBehaviour {
    public GameObject dataPanel;
    public GameObject dataPanelCloseButton;
    public GameObject pvalwarning;
    public bool togglePval;

    //public static ScoreManager scorelist = new ScoreManager();
    public void Start()
    {
        togglePval = GameObject.Find("PlayerData").GetComponent<PlayerData>().togglePval;
    }

    public void OpenPanelAndButton(){
        if(dataPanel != null){
            bool isActive = dataPanel.activeSelf;
            dataPanel.SetActive(!isActive);
        }
        if (dataPanelCloseButton != null)
        {
            bool isActive = dataPanelCloseButton.activeSelf;
            dataPanelCloseButton.SetActive(!isActive);
        }
        if (pvalwarning != null && togglePval)
        {
            bool isActive = pvalwarning.activeSelf;
            pvalwarning.SetActive(!isActive);
        }
    }

    public void ClosePanelAndButton()
    {
        if (dataPanel != null)
        {
            dataPanel.SetActive(false);
        }
        if (dataPanelCloseButton != null)
        {
            dataPanelCloseButton.SetActive(false);
        }
        if (pvalwarning != null && togglePval)
        {
            pvalwarning.SetActive(false);
        }
    }

}
