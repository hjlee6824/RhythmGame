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

    public float startYPos;
    public float endYPos;

    // 노래의 BPM
    public float songBpm;

    // 현재 노래의 재생 위치
    public float songPosition;

    // 한 박자에 소요되는 시간으로 (60f / BPM)과 동일
    public float secondsPerBeat;

    // 특정 노래에는 시작 부분에 약간의 공백이 있기 때문에 노래 위치를 계산할 때 그 공백만큼 빼주어야 함
    public float songOffset;

    // 노래 재생이 시작된 시점을 저장하여 songPosition을 계산할 때 빼주어야 함
    public float dspTimeSong;

    // 노트 생성 지점부터 노트 파괴 지점까지 표시될 수 있는 최대 박자 수 (스크롤 속도)
    public float beatsShownOnScreen;

    // 각 트랙 별로 다음에 생성해야 할 노트의 배열 위치를 기억
    int trackIndex1 = 0;
    int trackIndex2 = 0;
    int trackIndex3 = 0;
    int trackIndex4 = 0;

    bool isSongStarted = false;

    // 오브젝트 풀링을 이용해 최적화할 예정
    //private Queue<Note> objectPool;

    void Start()
    {
        chart = FindObjectOfType<Chart>().GetComponent<Chart>();
        parser = FindObjectOfType<Parser>().GetComponent<Parser>();
        beatsShownOnScreen = 2f;
        hitSound = hitSoundPlayer.clip;
    }

    void Update()
    {
        // 차트 데이터 파싱이 다 될때까지 대기
        if (parser.isParsed)
        {
            songBpm = chart.bpm * songPlayer.pitch;
            secondsPerBeat = 60f / songBpm;
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
                dspTimeSong = (float)AudioSettings.dspTime;
                songPlayer.Play();
                return;
            }
        }

        if (!isSongStarted) return;

        songPosition = (float)(AudioSettings.dspTime - dspTimeSong) * songPlayer.pitch - songOffset;

        float noteToSpawn = songPosition / secondsPerBeat + beatsShownOnScreen;

        if (trackIndex1 < chart.track1.Count)
        {
            float nextTimeInTrack1 = chart.track1[trackIndex1] / 1000 / secondsPerBeat;

            if (nextTimeInTrack1 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                // Note note = ((GameObject)Instantiate(notePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Note>();
                note.Initialize(this, -1.5f, startYPos, endYPos, nextTimeInTrack1);
                trackIndex1++;
            }
        }

        if (trackIndex2 < chart.track2.Count)
        {
            float nextTimeInTrack2 = chart.track2[trackIndex2] / 1000 / secondsPerBeat;

            if (nextTimeInTrack2 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                // Note note = ((GameObject)Instantiate(notePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Note>();
                note.Initialize(this, -0.5f, startYPos, endYPos, nextTimeInTrack2);
                trackIndex2++;
            }
        }

        if (trackIndex3 < chart.track3.Count)
        {
            float nextTimeInTrack3 = chart.track3[trackIndex3] / 1000 / secondsPerBeat;

            if (nextTimeInTrack3 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                // Note note = ((GameObject)Instantiate(notePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Note>();
                note.Initialize(this, 0.5f, startYPos, endYPos, nextTimeInTrack3);
                trackIndex3++;
            }
        }

        if (trackIndex4 < chart.track4.Count)
        {
            float nextTimeInTrack4 = chart.track4[trackIndex4] / 1000 / secondsPerBeat;

            if (nextTimeInTrack4 < noteToSpawn)
            {
                Note note = ObjectPool.GetObject();
                // Note note = ((GameObject)Instantiate(notePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Note>();
                note.Initialize(this, 1.5f, startYPos, endYPos, nextTimeInTrack4);
                trackIndex4++;
            }
        }
    }
}