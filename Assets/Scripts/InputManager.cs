using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Judgement judgement;

    Transform key1;
    Transform key2;
    Transform key3;
    Transform key4;

    void Start()
    {
        judgement = FindObjectOfType<Judgement>().GetComponent<Judgement>();

        key1 = GameObject.Find("Key1").GetComponent<Transform>();
        key2 = GameObject.Find("Key2").GetComponent<Transform>();
        key3 = GameObject.Find("Key3").GetComponent<Transform>();
        key4 = GameObject.Find("Key4").GetComponent<Transform>();

        key1.gameObject.SetActive(false);
        key2.gameObject.SetActive(false);
        key3.gameObject.SetActive(false);
        key4.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            key1.gameObject.SetActive(true);
            judgement.GetDiffTime(1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            key1.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            key2.gameObject.SetActive(true);
            judgement.GetDiffTime(2);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            key2.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            key3.gameObject.SetActive(true);
            judgement.GetDiffTime(3);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            key3.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            key4.gameObject.SetActive(true);
            judgement.GetDiffTime(4);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            key4.gameObject.SetActive(false);
        }
    }
}
