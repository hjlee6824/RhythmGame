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
    public Queue<float> track1_TimingData = new Queue<float>();
    public Queue<float> track2_TimingData = new Queue<float>();
    public Queue<float> track3_TimingData = new Queue<float>();
    public Queue<float> track4_TimingData = new Queue<float>();

    public void SetNote(int trackNum, float noteTime)
    {
        if (trackNum.Equals(1))
        {
            track1_TimingData.Enqueue(noteTime);
        }
        else if (trackNum.Equals(2))
        {
            track2_TimingData.Enqueue(noteTime);
        }
        else if (trackNum.Equals(3))
        {
            track3_TimingData.Enqueue(noteTime);
        }
        else
        {
            track4_TimingData.Enqueue(noteTime);
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