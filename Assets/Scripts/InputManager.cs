using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Judgement judgement;

    void Start()
    {
        judgement = FindObjectOfType<Judgement>().GetComponent<Judgement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            judgement.GetDiffTime(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            judgement.GetDiffTime(2);
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            judgement.GetDiffTime(3);
        }

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            judgement.GetDiffTime(4);
        }
    }
}