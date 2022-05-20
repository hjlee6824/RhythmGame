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

    float accuracy = 0f;

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
    }

    void Update()
    {
        
    }

    public void AddCount(int num)
    {
        if(num.Equals(5)) {
            str = "MARVELOUS";
            judge.color = Color.white;
            SetText();
            maxPerfectCount++;
            comboCount++;
        }
        else if (num.Equals(4)) {
            str = "PERFECT";
            judge.color = Color.yellow;
            SetText();
            perfectCount++;
            comboCount++;
        }
        else if (num.Equals(3))
        {
            str = "GREAT";
            judge.color = Color.green;
            SetText();
            greatCount++;
            comboCount++;
        }
        else if (num.Equals(2))
        {
            str = "GOOD";
            judge.color = Color.blue;
            SetText();
            goodCount++;
            comboCount++;
        }
        else if (num.Equals(1))
        {
            str = "BAD";
            judge.color = Color.magenta;
            SetText();
            badCount++;
            comboCount++;
        }
        else
        {
            str = "MISS";
            judge.color = Color.red;
            SetText();
            missCount++;
            comboCount = 0;
        }

        SetCount();
        SetMaxCombo();
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

    void SetMaxCombo()
    {
        if (comboCount >= maxCombo)
        {
            maxCombo = comboCount;
        }
    }

    void SetText()
    {
        judge.text = str;
        combo.text = comboCount.ToString();
    }
}