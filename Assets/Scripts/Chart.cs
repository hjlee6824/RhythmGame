using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : MonoBehaviour
{
    // [AudioInfo]
    public string artist { get; set; }
    public string title { get; set; }
    public float audioPreviewTime { get; set; }
    public float bpm { get; set; }
    public float offset { get; set; }
    public string difficulty { get; set; }

    // [HitObjects]
    public List<float> track1;
    public List<float> track2;
    public List<float> track3;
    public List<float> track4;

    public void SetNote(int trackNum, float noteTime)
    {
        if (trackNum.Equals(1))
        {
            track1.Add(noteTime);
        }
        else if (trackNum.Equals(2))
        {
            track2.Add(noteTime);
        }
        else if (trackNum.Equals(3))
        {
            track3.Add(noteTime);
        }
        else
        {
            track4.Add(noteTime);
        }
    }

    public void showChartInfo()
    {
        Debug.Log("Artist: " + artist);
        Debug.Log("Title: " + title);
        Debug.Log("AudioPreviewTime: " + audioPreviewTime);
        Debug.Log("BPM: " + bpm);
        Debug.Log("Offset: " + offset);
        Debug.Log("Difficulty: " + difficulty);
    }
}