using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fps, delayedTime;

    void Start()
    {

    }

    void Update()
    {
        float ms = Mathf.Round(Time.smoothDeltaTime * 100000f) / 100f;
        delayedTime.text = string.Format("{0:F2}", ms) + "ms";

        fps.text = $"FPS:{(int)(1f / Time.unscaledDeltaTime)}";
    }
}