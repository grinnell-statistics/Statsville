using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Linq;
using System;

// SplitCsvGrid function used from Unity Wiki: http://wiki.unity3d.com/index.php/CSVReader#ExampleCSV.txt

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;
    public static string[,] grid;

    public void Start()
    {
        grid = SplitCsvGrid(csvFile.text);
        Debug.Log("size = " + (1 + grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1)));

        //DebugOutputGrid(grid);
    }

    // Outputs the content of a 2D array, useful for checking the importer
    // Comment in last print statement to test function
    static public void DebugOutputGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {
                textOutput = grid[x, y];
                Debug.Log(grid[x,y]+ "\n");
                Debug.Log(grid[x,y]+ "\n");
                textOutput += "|";
            }
            textOutput += "\n";
        }
        //Debug.Log(textOutput);
    }

    public static double PValueLookup(double zstat) {

      
      string line = "";
      double pval = 0;
        double temp;

        
      if(zstat > 0.45 && zstat < 12.12) {

        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
          for (int x = 0; x < grid.GetUpperBound(0); x++)
          {
            line = grid[x, y];
            string[] vals = line.Split(';');
           
           

            if (vals.Length >= 2 ) {
                        temp = double.Parse(vals[0], CultureInfo.InvariantCulture.NumberFormat);
                        if (Math.Round(zstat, 4).Equals(Math.Round(temp,4)) || Math.Round(zstat, 3).Equals(Math.Round(temp, 3)) || Math.Round(zstat, 2).Equals(Math.Round(temp, 2))) {
                            
                    pval = double.Parse(vals[1], CultureInfo.InvariantCulture.NumberFormat);
                    break;
                } else if((Math.Floor(zstat * 100)/100).Equals(Math.Floor(temp *100) /100))
                        {
                            pval = double.Parse(vals[1], CultureInfo.InvariantCulture.NumberFormat);
                            break; 
                        }
                    }
          }
        }
            pval = Math.Round(pval, 3);
      } 
      else if (zstat <= 0.45)
        {
            pval = -999.0;

        }
        else if (zstat >= 12.12)
        {
            pval = -1.0;
        }
        
        return pval;
    }

    // splits a CSV file into a 2D string array
    static public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output.
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    // splits a CSV row
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
}
