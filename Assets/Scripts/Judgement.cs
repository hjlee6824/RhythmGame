using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    Conductor conductor;
    ScoreManager scoreManager;
    
    Queue<Note> judgeTrack1 = new Queue<Note>();
    Queue<Note> judgeTrack2 = new Queue<Note>();
    Queue<Note> judgeTrack3 = new Queue<Note>();
    Queue<Note> judgeTrack4 = new Queue<Note>();

    // osu!mania의 Overall Difficulty = 8 값을 기준으로 한 판정 범위
    // MAX: +-16.5ms
    // 300: +-40.5ms
    // 200: +-73.5ms
    // 100: +-103.5ms
    // 50: +-127.5ms
    // 그 외에 노트를 누르지 못하거나 Miss 범위에서 누르면 Miss 처리
    float maxTiming = 16.5f;
    float perfectTiming = 40.5f;
    float greatTiming = 73.5f;
    float goodTiming = 103.5f;
    float badTiming = 127.5f;

    bool isAutoPlay = false;

    void Start()
    {
        conductor = FindObjectOfType<Conductor>().GetComponent<Conductor>();
        scoreManager = FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
    }

    // 키를 입력하지 않고 노트가 판정선 아래로 내려갔을 때 미스 처리
    void Update()
    {
        if (isAutoPlay)
        {
            float diffTime;
            
            if (judgeTrack1.Count > 0)
            {
                diffTime = judgeTrack1.Peek().timing - conductor.songPosition * 1000;

                if (diffTime < 0)
                {
                    JudgeTiming(1, diffTime);
                }
            }

            
            if (judgeTrack2.Count > 0)
            {
                diffTime = judgeTrack2.Peek().timing - conductor.songPosition * 1000;

                if (diffTime < 0)
                {
                    JudgeTiming(2, diffTime);
                }
            }
            
            if (judgeTrack3.Count > 0)
            {
                diffTime = judgeTrack3.Peek().timing - conductor.songPosition * 1000;

                if (diffTime < 0)
                {
                    JudgeTiming(3, diffTime);
                }
            }
            
            if (judgeTrack4.Count > 0)
            {
                diffTime = judgeTrack4.Peek().timing - conductor.songPosition * 1000;

                if (diffTime < 0)
                {
                    JudgeTiming(4, diffTime);
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A))
        {
            isAutoPlay = !isAutoPlay;
        }

        if (judgeTrack1.Count > 0)
        {
            if (judgeTrack1.Peek().transform.position.y < -6f)
            {
                scoreManager.AddCount(0);
                ObjectPool.ReturnObject(judgeTrack1.Dequeue());
            }
        }

        if (judgeTrack2.Count > 0)
        {
            if (judgeTrack2.Peek().transform.position.y < -6f)
            {
                scoreManager.AddCount(0);
                ObjectPool.ReturnObject(judgeTrack2.Dequeue());
            }
        }

        if (judgeTrack3.Count > 0)
        {
            if (judgeTrack3.Peek().transform.position.y < -6f)
            {
                scoreManager.AddCount(0);
                ObjectPool.ReturnObject(judgeTrack3.Dequeue());
            }
        }

        if (judgeTrack4.Count > 0)
        {
            if (judgeTrack4.Peek().transform.position.y < -6f)
            {
                scoreManager.AddCount(0);
                ObjectPool.ReturnObject(judgeTrack4.Dequeue());
            }
        }
    }

    // 키를 입력했을 때 특정 트랙에서 떨어지는 노트들 중 가장 가까운 노트와의 시간차
    public void GetDiffTime(int trackNum)
    {
        float diffTime;

        if (trackNum.Equals(1) && judgeTrack1.Count > 0)
        {
            diffTime = Mathf.Abs(judgeTrack1.Peek().timing - conductor.songPosition * 1000);
            JudgeTiming(1, diffTime);
        }
        else if (trackNum.Equals(2) && judgeTrack2.Count > 0)
        {
            diffTime = Mathf.Abs(judgeTrack2.Peek().timing - conductor.songPosition * 1000);
            JudgeTiming(2, diffTime);
        }
        else if (trackNum.Equals(3) && judgeTrack3.Count > 0)
        {
            diffTime = Mathf.Abs(judgeTrack3.Peek().timing - conductor.songPosition * 1000);
            JudgeTiming(3, diffTime);
        }
        else if (trackNum.Equals(4) && judgeTrack4.Count > 0)
        {
            diffTime = Mathf.Abs(judgeTrack4.Peek().timing - conductor.songPosition * 1000);
            JudgeTiming(4, diffTime);
        }
    }

    public void EnqueueNote(int trackNum, Note note)
    {
        if (trackNum.Equals(1))
        {
            judgeTrack1.Enqueue(note);
        }
        else if (trackNum.Equals(2))
        {
            judgeTrack2.Enqueue(note);
        }
        else if (trackNum.Equals(3))
        {
            judgeTrack3.Enqueue(note);
        }
        else
        {
            judgeTrack4.Enqueue(note);
        }
    }

    public void DequeueNote(int trackNum)
    {
        if (trackNum.Equals(1))
        {
            ObjectPool.ReturnObject(judgeTrack1.Dequeue());
        }
        else if (trackNum.Equals(2))
        {
            ObjectPool.ReturnObject(judgeTrack2.Dequeue());
        }
        else if (trackNum.Equals(3))
        {
            ObjectPool.ReturnObject(judgeTrack3.Dequeue());
        }
        else
        {
            ObjectPool.ReturnObject(judgeTrack4.Dequeue());
        }
    }

    // 너무 빨리 누르거나 늦게 눌러서 생기는 Miss는 히트사운드 X
    void JudgeTiming(int trackNum, float diffTime)
    {
        if (diffTime < maxTiming)
        {
            scoreManager.AddCount(5);
            DequeueNote(trackNum);
            conductor.hitSoundPlayer.PlayOneShot(conductor.hitSound);
        }
        else if (diffTime < perfectTiming)
        {
            scoreManager.AddCount(4);
            DequeueNote(trackNum);
            conductor.hitSoundPlayer.PlayOneShot(conductor.hitSound);
        }
        else if (diffTime < greatTiming)
        {
            scoreManager.AddCount(3);
            DequeueNote(trackNum);
            conductor.hitSoundPlayer.PlayOneShot(conductor.hitSound);
        }
        else if (diffTime < goodTiming)
        {
            scoreManager.AddCount(2);
            DequeueNote(trackNum);
            conductor.hitSoundPlayer.PlayOneShot(conductor.hitSound);
        }
        else if (diffTime < badTiming)
        {
            scoreManager.AddCount(1);
            DequeueNote(trackNum);
            conductor.hitSoundPlayer.PlayOneShot(conductor.hitSound);
        }
        else if (diffTime < 150f)
        {
            scoreManager.AddCount(0);
            DequeueNote(trackNum);
        }
    }
}