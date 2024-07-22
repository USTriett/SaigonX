using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private static int score = 0;
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = score.ToString();
        ZombieController.AddDeathEventListener(IncreaseScore);
    }

    private void OnDisable()
    {
        ZombieController.RemoveDeathEventListener(IncreaseScore);
    }

    private void IncreaseScore()
    {
        score = int.Parse(_text.text) + 1;
        _text.text = score.ToString();
    }
}
