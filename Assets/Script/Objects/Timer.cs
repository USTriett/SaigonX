using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameObject result;
    private TextMeshProUGUI _timeDisplay; //in second

    [SerializeField]
    private float _startTime = 60;
    private float _origin;

    void OnEnable()
    {
        _timeDisplay = GetComponent<TextMeshProUGUI>();
        _timeDisplay.text = _startTime.ToString();
        _origin = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        _startTime -= Time.deltaTime;
        if (_startTime <= 0)
        {
            _startTime = 0;
            NotifyEndTimer();
        }
        _timeDisplay.text = ((int)_startTime).ToString();
    }

    private void NotifyEndTimer()
    {
        _startTime = _origin;

        result.SetActive(true);
        Time.timeScale = 0;
    }
}
