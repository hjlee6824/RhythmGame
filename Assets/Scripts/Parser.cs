using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Parser : MonoBehaviour
{
    Chart chart;

    string path;
    public bool isParsed = false;

    void Awake()
    {
        chart = FindObjectOfType<Chart>().GetComponent<Chart>();

        path = "Assets/Resources/Songs/Paradigm Shift/data.txt";
        ReadFile(path);
        chart.showChartInfo();
    }

    public void ReadFile(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        int trackNum = 0;
        float noteTime = 0f;

        double _bpmorigin = 1f;
        double _bpm = 1f;
        double _sv = 1f;

        bool audioInfo = false;
        bool timingPoints = false;
        bool hitObjects = false;

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            string line;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (!audioInfo) audioInfo = line.Contains("[AudioInfo]");
                if (!timingPoints) timingPoints = line.Contains("[TimingPoints]");
                if (!hitObjects) hitObjects = line.Contains("[HitObjects]");

                if (line.Contains("="))
                {
                    string[] arr1 = line.Split('=');

                    if (arr1[0] == "Artist")
                    {
                        chart.artist = arr1[1];
                    }
                    else if (arr1[0] == "Title")
                    {
                        chart.title = arr1[1];
                    }
                    else if (arr1[0] == "AudioPreviewTime")
                    {
                        chart.audioPreviewTime = float.Parse(arr1[1]);
                    }
                    else if (arr1[0] == "Offset")
                    {
                        chart.offset = float.Parse(arr1[1]);
                    }
                    else if (arr1[0] == "Difficulty")
                    {
                        chart.difficulty = arr1[1];
                    }
                }

                if (line.Contains(","))
                {
                    if (timingPoints && !hitObjects)
                    {
                        string[] arr2 = line.Split(',');
                        string strBpm = arr2[1];

                        if (arr2[6] == "1") // Uninherited (BPM)
                        {
                            _sv = 1f;
                            _bpmorigin = double.Parse(strBpm);
                            _bpm = _bpmorigin;
                        }
                        else // Inherited (Slider Velocity)
                        {
                            _sv = -double.Parse(strBpm);
                            _bpm = _bpmorigin * _sv;
                        }

                        if (_bpm < 0)
                        {
                            continue;
                        }

                        chart.AddTimingPoint(float.Parse(arr2[0]), _bpm);
                    }

                    if (hitObjects)
                    {
                        string[] arr3 = line.Split(',');

                        trackNum = int.Parse(arr3[0]);
                        noteTime = float.Parse(arr3[2]);

                        if (trackNum == 64) trackNum = 1;
                        else if (trackNum == 192) trackNum = 2;
                        else if (trackNum == 320) trackNum = 3;
                        else trackNum = 4;

                        chart.AddNoteTime(trackNum, noteTime);
                    }
                }
            }
        }
        else
        {
            Debug.Log("파일을 읽지 못함");
        }

        isParsed = true; // 차트 로드 성공
    }
}