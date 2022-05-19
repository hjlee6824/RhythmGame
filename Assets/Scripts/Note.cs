using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Conductor conductor;

    // 노트 생성 위치
    public float spawnPos;

    // 판정선 위치
    public float judgementLinePos;

    // 노트의 시간값을 박자 단위로 변환한 값
    public float beat;

    public void Initialize(Conductor conductor, float xPos, float startYPos, float endYPos, float beat)
    {
        this.conductor = conductor;
        this.spawnPos = startYPos;
        this.judgementLinePos = endYPos;
        this.beat = beat;

        transform.position = new Vector2(xPos, spawnPos);
    }

    void Start()
    {

    }

    void Update()
    {
        // (judgementLinePos - spawnPos)는 노트 생성 지점부터 파괴 지점까지의 높이이며, 이 값이 커질수록 노트가 더 아래로 내려감

        // (beat - conductor.songPosition / conductor.secondsPerBeat) / conductor.beatsShownOnScreen은 
        // 떨어지는 노트의 박자 값과 현재 노래 위치의 박자 값의 차를 화면에 표시할 최대 박자 수(스크롤 속도)로 나눈 것임
        // 예를 들어, beatsShownOnScreen의 값이 4라면, (beat - conductor.songPosition / conductor.secondsPerBeat)의 값이 0~4인 노트만 화면에 보임

        // 1에서 위의 값을 뺀 값인 (1f - (beat - conductor.songPosition / conductor.secondsPerBeat) / conductor.beatsShownOnScreen)을
        // (judgementLinePos - spawnPos)에 곱해야 노트가 내려오는 것 처럼 보이게 됨

        // 요약하면, 떨어지는 노트의 박자 값과 현재 노래 위치의 박자 값의 차가 beatsShownOnScreen일 때부터 노트가 생성되고
        // (노트의 박자 값 - 현재 노래 위치의 박자 값)이 0에 가까워 질수록 노트가 판정선에 가까워지게 됨
        transform.position = new Vector2(transform.position.x,
            spawnPos + (judgementLinePos - spawnPos) * 
            (1f - (beat - conductor.songPosition / conductor.secondsPerBeat) / conductor.beatsShownOnScreen));

        if (transform.position.y <= judgementLinePos)
        {
            conductor.hitSoundPlayer.PlayOneShot(conductor.hitSound);
            ObjectPool.ReturnObject(this);
        }
    }
}