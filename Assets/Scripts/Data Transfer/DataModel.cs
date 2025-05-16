using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class DataModel : MonoBehaviour {

    public List<int> level = new List<int>();
    public List<DateTime> dateTime = new List<DateTime>();
    public List<string> playerID = new List<string>();
    public List<string> groupID = new List<string>();
    public List<int> day = new List<int>();
    public List<int> population = new List<int>();
    public List<int> budget = new List<int>();
    public List<int> availToTreat = new List<int>();
    public List<int> aTreat = new List<int>();
    public List<int> aCure = new List<int>();
    public List<int> bTreat = new List<int>();
    public List<int> bCure = new List<int>();
    public List<int> aCost = new List<int>();
    public List<int> bCost = new List<int>();
    public List<int> sickCost = new List<int>();
    public List<string> winLose = new List<string>();
    public List<double> pVal = new List<double>();


    public DataModel()
    {

    }

    public void Start()
    {
        Debug.Log(GetPValue(19, 22, 9, 21));
    }

    public void AddWinLose(string winOrLose)
    {

        foreach (int _ in this.day)
        {
            this.winLose.Add(winOrLose);
        }

    }

    public void AddData(GameBuilder1 world, Initializer init)
    {
        Day cur = world.retDay(1);

        Debug.Log("AddData called: Day " + cur.get("day"));

        this.dateTime.Add(DateTime.Now);
        this.playerID.Add(PlayerData.playerdata.playerID);
        this.groupID.Add(PlayerData.playerdata.groupID);
        this.day.Add(cur.get("day"));
        this.population.Add((int)init.getParam("population"));
        this.budget.Add((int)init.getParam("fund") - cur.get("totalCost"));
        this.availToTreat.Add(cur.get("totalSymps"));
        this.aTreat.Add(cur.get("sickTrtA"));
        this.aCure.Add(cur.get("curedANum"));
        this.bTreat.Add(cur.get("sickTrtB"));
        this.bCure.Add(cur.get("curedBNum"));
        this.aCost.Add((int)init.getParam("costA") * cur.get("sickTrtA"));
        this.bCost.Add((int)init.getParam("costB") * cur.get("sickTrtB"));
        this.sickCost.Add(cur.get("totalSymps") * (int)init.getParam("sickCost"));
        this.pVal.Add(GetPValue(cur.get("curedANum"), cur.get("curedBNum"), cur.get("sickTrtA"), cur.get("sickTrtB")));

        //scoreList.UpdateTable();

    }

    public static double CalculateZStat(double cureA, double cureB, double treatA, double treatB)
    {
        double zstat = 0;
        if (treatA > 0 && treatB > 0)
        {
            double AProportion = cureA / treatA;
            double BProportion = cureB / treatB;
            double totalProportion = (cureA + cureB) / (treatA + treatB);
            double numinverseadded = (1 / treatA) + (1 / treatB);

            double numerator = AProportion - BProportion;
            double beforesqrt = (totalProportion * (1 - totalProportion)) * numinverseadded;
            if(beforesqrt<0)
            {
                beforesqrt = beforesqrt * -1;
            }
            double denominator = Math.Sqrt(beforesqrt); // This is where the problem is. Being fed -0.07954. So returns NaN

            Debug.Log("Aprop, Bprop, totalProp, numinverse, num, b4sqrt, denom " + AProportion + ", " + BProportion + ", " + totalProportion + ", " + numinverseadded + ", " + numerator + ", " + beforesqrt + ", " + denominator);
            zstat = numerator / denominator;
        }
        
        return zstat;
    }

    public static double GetPValue(double cureA, double cureB, double treatA, double treatB)
    {

        double pvalue = 0;
        double zstat = CalculateZStat(cureA, cureB, treatA, treatB);

        if (zstat == 0)
        {
            pvalue = 0;
        }
        else
        {
            double zstatsquare = zstat * zstat;
            // Look up PValue table from pvalues.csv file for corresponding zstat
            pvalue = CSVReader.PValueLookup(zstatsquare);
            Debug.Log("zstat: " + zstat + " zstatsquare: " + zstatsquare);
        }

        return pvalue;
    }

}
