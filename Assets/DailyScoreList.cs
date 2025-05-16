using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SLS.Widgets.Table;

public class DailyScoreList : MonoBehaviour {

    //public GameObject entryPrefab;
    private Table table;
    
    private bool togglePval;
    public WorldModel world;

    //public TextAsset csv;
    DataModel datam;
    //int dayCount;

    int lastDay = 0;

    // Use this for initialization
    void Start () {
        togglePval = GameObject.Find("PlayerData").GetComponent<PlayerData>().togglePval;
        this.table = this.GetComponent<Table>();

        this.table.ResetTable();

        this.table.AddTextColumn();
        this.table.AddTextColumn();
        this.table.AddTextColumn();
        this.table.AddTextColumn();
        this.table.AddTextColumn();
        this.table.AddTextColumn();
        this.table.AddTextColumn();
        if (togglePval)
        {
            this.table.AddTextColumn();
        }


        this.table.Initialize(this.OnTableSelected);

        int uid = -1;

        Datum d = Datum.Body(uid.ToString());

        //Debug.Log("Got inside for loop");
        //DataModel.GetPValue(5, 7, 20, 14, csv);
        //go.transform.Find("").GetComponent<Text>().text = datam.day[index].ToString();
        d.elements.Add("Day");
        d.elements.Add("Budget");
        d.elements.Add("AvailToTreat");
        d.elements.Add("TreatA");
        d.elements.Add("CureA");
        d.elements.Add("TreatB");
        d.elements.Add("CureB");
        if (togglePval)
        {
            d.elements.Add("PValue *");
        }


        this.table.data.Add(d);

        datam = world.data;
        //this.dayCount = datam.day.Count;

        if (datam == null)
        {
            Debug.Log("datam is null");
            return;
        }

        this.table.StartRenderEngine();

    }

	// Update is called once per frame
	void Update () {

        if(datam.day.Count - lastDay == 0)
        {
            // This means there haven't been any new days so do nothing
            ;
        }
        else if (datam.day.Count - lastDay == 1) 
        {
            // This means there has been a single new day added to datam
            UpdateTable(lastDay); // So we update the table with this new day

            // increment last day by one to account for new day addition
            lastDay += 1;

        }
        else if (datam.day.Count - lastDay > 1)
        {
            // This means our code failed to be efficient and did not add the new days
            // So add all new days and set last day to current day
            UpdateTable(lastDay);
            lastDay = datam.day.Count;
        }
    }

    public void UpdateTable(int lastDay) {

        for (int index = lastDay; index < datam.day.Count; index++)
        {
            Datum d = Datum.Body(index.ToString());

            d.elements.Add(datam.day[index]);
            d.elements.Add(datam.budget[index]);
            d.elements.Add(datam.availToTreat[index]);
            d.elements.Add(datam.aTreat[index]);
            d.elements.Add(datam.aCure[index]);
            d.elements.Add(datam.bTreat[index]);
            d.elements.Add(datam.bCure[index]);

            if(togglePval)
            {
                if (datam.pVal[index] == 0.0)
                {
                    d.elements.Add("NA");
                }
                else if (datam.pVal[index] == -999.0)
                {
                    d.elements.Add("> 0.50");
                }
                else if (datam.pVal[index] == -1.0)
                {
                    d.elements.Add("< 0.001");
                }
                else
                {
                    d.elements.Add(datam.pVal[index].ToString());
                }
            }

            this.table.data.Add(d);

        }
    }

    private void OnTableSelected(Datum datum, Column column)
    {
        string cidx = "N/A";
        if (column != null) cidx = column.idx.ToString();
        print("You Clicked: " + datum.uid + " Column: " + cidx);
    }
}
