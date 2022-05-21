using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI miss, bad, good, great, perfect, maxPerfect, judge, combo, max_combo, acc;

    int missCount;
    int badCount;
    int goodCount;
    int greatCount;
    int perfectCount;
    int maxPerfectCount;
    int comboCount;
    int maxCombo;

    float accuracy;

    string str;

    void Start()
    {
        missCount = 0;
        badCount = 0;
        goodCount = 0;
        greatCount = 0;
        perfectCount = 0;
        maxPerfectCount = 0;
        comboCount = 0;
        maxCombo = 0;
        accuracy = 0f;
        str = "";

        miss.text = str;
        bad.text = str;
        good.text = str;
        great.text = str;
        perfect.text = str;
        maxPerfect.text = str;
        judge.text = str;
        combo.text = str;
        max_combo.text = str;
        acc.text = str;
    }

    void Update()
    {
        
    }

    public void AddCount(int num)
    {
        if(num.Equals(5)) {
            str = "MARVELOUS";
            judge.color = Color.white;
            SetJudge();
            maxPerfectCount++;
            comboCount++;
        }
        else if (num.Equals(4)) {
            str = "PERFECT";
            judge.color = Color.yellow;
            SetJudge();
            perfectCount++;
            comboCount++;
        }
        else if (num.Equals(3))
        {
            str = "GREAT";
            judge.color = Color.green;
            SetJudge();
            greatCount++;
            comboCount++;
        }
        else if (num.Equals(2))
        {
            str = "GOOD";
            judge.color = Color.blue;
            SetJudge();
            goodCount++;
            comboCount++;
        }
        else if (num.Equals(1))
        {
            str = "BAD";
            judge.color = Color.magenta;
            SetJudge();
            badCount++;
            comboCount++;
        }
        else
        {
            str = "MISS";
            judge.color = Color.red;
            SetJudge();
            missCount++;
            comboCount = 0;
        }

        SetCount();
        SetAccuracy();
        SetCount();
        SetCombo();
        SetMaxCombo();
    }

    void SetAccuracy()
    {
        int a = 50 * badCount + 100 * goodCount + 200 * greatCount + 300 * (perfectCount + maxPerfectCount);
        int b = 300 * (missCount + badCount + goodCount + greatCount + perfectCount + maxPerfectCount);
        accuracy = (float)a / (float)b * 100f;
        acc.text = accuracy.ToString("F2") + "%";
    }

    void SetCount()
    {
        maxPerfect.text = "Marvelous: " + maxPerfectCount.ToString();
        perfect.text = "Perfect: " + perfectCount.ToString();
        great.text = "Great: " + greatCount.ToString();
        good.text = "Good: " + goodCount.ToString();
        bad.text = "Bad: " + badCount.ToString();
        miss.text = "Miss: " + missCount.ToString();
        max_combo.text = "Max Combo: " + maxCombo.ToString();
    }

    void SetJudge()
    {
        judge.text = str;
    }

    void SetCombo()
    {
        combo.text = comboCount.ToString();
    }

    void SetMaxCombo()
    {
        if (comboCount >= maxCombo)
        {
            maxCombo = comboCount;
        }
    }
}