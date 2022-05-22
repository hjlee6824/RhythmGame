using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Parser : MonoBehaviour
{
    TextAsset textAsset;
    StringReader strReader;
    Chart chart;

    string sheetText = "";
    string[] textSplit;

    public bool isParsed = false;

    void Awake()
    {
        chart = FindObjectOfType<Chart>().GetComponent<Chart>();
        textAsset = Resources.Load("Songs/Speedcore 300/data") as TextAsset;
        strReader = new StringReader(textAsset.text);

        ParsingData();
        chart.showChartInfo();
    }

    public void ParsingData()
    {
        int trackNum = 0;
        float noteTime = 0;

        while (sheetText != null)
        {
            sheetText = strReader.ReadLine();
            textSplit = sheetText.Split('=');

            if (textSplit[0] == "Artist") chart.artist = textSplit[1];
            else if (textSplit[0] == "Title") chart.title = textSplit[1];
            else if (textSplit[0] == "AudioPreviewTime") chart.audioPreviewTime = float.Parse(textSplit[1]);
            else if (textSplit[0] == "BPM") chart.bpm = float.Parse(textSplit[1]);
            else if (textSplit[0] == "Offset") chart.offset = float.Parse(textSplit[1]);
            else if (textSplit[0] == "Difficulty") chart.difficulty = textSplit[1];
            else if (sheetText == "[HitObjects]")
            {
                while ((sheetText = strReader.ReadLine()) != null)
                {
                    textSplit = sheetText.Split(',');
                    trackNum = int.Parse(textSplit[0]);
                    noteTime = float.Parse(textSplit[2]);

                    if (trackNum == 64) trackNum = 1;
                    else if (trackNum == 192) trackNum = 2;
                    else if (trackNum == 320) trackNum = 3;
                    else trackNum = 4;

                    chart.SetNote(trackNum, noteTime);
                }
            }
        }

        isParsed = true;
    }
}