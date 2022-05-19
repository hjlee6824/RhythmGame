using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<Note> poolingObjectQueue = new Queue<Note>();

    private void Awake()
    {
        Instance = this;
        Initialize(200);
    }

    // 오브젝트 풀 안에 오브젝트가 존재하지 않을 때 또는 오브젝트 풀 초기화 시에 미리 만들어 놓을 오브젝트를 생성하는 함수
    private Note CreateNewObject()
    {
        // 오브젝트 풀의 자식 오브젝트로 노트를 미리 만들고 비활성화
        Note note = Instantiate(poolingObjectPrefab, transform).GetComponent<Note>();
        note.gameObject.SetActive(false);
        return note;
    }

    private void Initialize(int count)
    {
        for(int i = 0; i < count; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    public static Note GetObject()
    {
        GameObject notesOnActive = GameObject.Find("NotesOnActive");
        if(Instance.poolingObjectQueue.Count > 0)
        {
            Note note = Instance.poolingObjectQueue.Dequeue();
            note.transform.SetParent(notesOnActive.transform);
            note.gameObject.SetActive(true);
            return note;
        }
        else
        {
            Note note = Instance.CreateNewObject();
            note.transform.SetParent(notesOnActive.transform);
            note.gameObject.SetActive(true);
            return note;
        }
    }

    public static void ReturnObject(Note note)
    {
        note.gameObject.SetActive(false);
        note.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(note);
    }
}