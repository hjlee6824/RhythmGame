using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioSource songPlayer;
    public AudioSource hitSoundPlayer;
    public AudioClip hitSound;

    Chart chart;
    Parser parser;
    Judgement judgement;
    BGAPlayer bgaPlayer;

    public float startYPos;
    public float endYPos;

    // 노래의 BPM
    public float songBpm;

    // 현재 노래의 재생 위치
    public float songPosition;

    // 한 박자에 소요되는 시간으로 (60f / BPM)과 동일
    // public float secondsPerBeat;

    // 입력 오프셋
    public float globalOffset;

    // 노래 재생이 시작된 시점을 저장하여 songPosition을 계산할 때 빼주어야 함
    public float dspTimeSong;

    // 노트 생성 지점부터 노트 파괴 지점까지 표시될 수 있는 최대 박자 수 (스크롤 속도)
    public float beatsShownOnScreen;

    bool isSongStarted = false;
    bool isBGAStarted = false;

    float videoStartTime;
    public float bgaOffset;

    void Start()
    {
        chart = FindObjectOfType<Chart>().GetComponent<Chart>();
        parser = FindObjectOfType<Parser>().GetComponent<Parser>();
        judgement = FindObjectOfType<Judgement>().GetComponent<Judgement>();
        bgaPlayer = FindObjectOfType<BGAPlayer>().GetComponent<BGAPlayer>();
        beatsShownOnScreen = 1.6f;
        //globalOffset = -0.15f;
        globalOffset = 0f;
        bgaOffset = 1.6f;
        hitSound = hitSoundPlayer.clip;
    }

    void Update()
    {
        // 차트 데이터 파싱이 다 될때까지 대기
        if (parser.isParsed)
        {
            songBpm = (float)chart.timingList[0].bpm * songPlayer.pitch;
            //songBpm = Mathf.Round((1f / (float)chart.timingList[0].bpm  * 1000f * 60f) * 100f) / 100f * songPlayer.pitch;
            //secondsPerBeat = 60f / songBpm;
        }
        else
        {
            return;
        }

        // 스페이스바를 누르면 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSongStarted)
            {
                isSongStarted = true;
                // 3초 뒤에 시작
                dspTimeSong = (float)AudioSettings.dspTime + 3f;
                songPlayer.PlayDelayed(3f);
                videoStartTime = Time.time;
                return;
            }
        }

        if (!isSongStarted) return;

        if (!isBGAStarted)
        {
            if (Time.time - videoStartTime >= 3f + bgaOffset)
            {
                isBGAStarted = true;
                bgaPlayer.PlayVideo();
            }
        }

        // 특정 노래에는 시작 부분에 약간의 공백이 있기 때문에 노래 위치를 계산할 때 그 공백만큼 빼주어야 함
        songPosition = (float)(AudioSettings.dspTime - dspTimeSong + chart.offset + globalOffset) * songPlayer.pitch;

        float noteToSpawn = songPosition * 1000 / songBpm + beatsShownOnScreen;

        if (chart.track1_TimingData.Count > 0)
        {
            float nextTimeInTrack1 = chart.track1_TimingData.Peek() / songBpm;

            if (nextTimeInTrack1 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                note.Initialize(this, -2.25f, startYPos, endYPos, chart.track1_TimingData.Dequeue(), nextTimeInTrack1);
                judgement.EnqueueNote(1, note);
            }
        }

        if (chart.track2_TimingData.Count > 0)
        {
            float nextTimeInTrack2 = chart.track2_TimingData.Peek() / songBpm;

            if (nextTimeInTrack2 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                note.Initialize(this, -0.75f, startYPos, endYPos, chart.track2_TimingData.Dequeue(), nextTimeInTrack2);
                judgement.EnqueueNote(2, note);
            }
        }

        if (chart.track3_TimingData.Count > 0)
        {
            float nextTimeInTrack3 = chart.track3_TimingData.Peek() / songBpm;

            if (nextTimeInTrack3 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                note.Initialize(this, 0.75f, startYPos, endYPos, chart.track3_TimingData.Dequeue(), nextTimeInTrack3);
                judgement.EnqueueNote(3, note);
            }
        }

        if (chart.track4_TimingData.Count > 0)
        {
            float nextTimeInTrack4 = chart.track4_TimingData.Peek() / songBpm;

            if (nextTimeInTrack4 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                note.Initialize(this, 2.25f, startYPos, endYPos, chart.track4_TimingData.Dequeue(), nextTimeInTrack4);
                judgement.EnqueueNote(4, note);
            }
        }
    }
}