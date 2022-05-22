using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BGAPlayer : MonoBehaviour
{
    public RawImage screen;
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (screen != null && videoPlayer != null)
        {
            StartCoroutine(PrepareVideo());
        }
    }

    public void PlayVideo()
    {
        if (videoPlayer != null && videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if (videoPlayer != null && videoPlayer.isPrepared)
        {
            videoPlayer.Stop();
        }
    }

    IEnumerator PrepareVideo()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.3f);
        }

        screen.texture = videoPlayer.texture;
    }
}