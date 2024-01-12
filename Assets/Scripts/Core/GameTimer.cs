using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{

    public static bool stop;
    public bool reset = false;
    public bool reverse;
    public string result;

    public enum CounterMode { hourMinSec = 0, minSec = 1 };
    public CounterMode counterMode = CounterMode.hourMinSec;
    public int startHour, startMin, startSec;
    public bool startAwake = true;

    private int hour, min, sec;
    private string h, m, s;

    void Awake()
    {
        if (startAwake) stop = false; else stop = true;

        if (startHour > 0 && startHour <= 23) hour = startHour; else startHour = 0;
        if (startMin > 0 && startMin <= 59) min = startMin; else startMin = 0;
        if (startSec > 0 && startSec <= 59) sec = startSec; else startSec = 0;

        ResetGameTimer();
    }

    void Start()
    {
        StartCoroutine(RepeatingFunction());
    }

    IEnumerator RepeatingFunction()
    {
        while (true)
        {
            if (reset)
            {
                ResetGameTimer();
                reset = false;
            }
            else
            {
                if (!stop) TimeCount();
            }

            yield return new WaitForSeconds(1);
        }
    }

    void TimeCount()
    {
        if (reverse)
        {
            if (sec < 0)
            {
                sec = 59;
                min--;
            }
            if (min < 0)
            {
                min = 59;
                hour--;
            }
            if (hour < 0)
            {
                hour = 23;
            }

            CurrentTime();

            sec--;
        }
        else
        {
            if (sec > 59)
            {
                sec = 0;
                min++;
            }
            if (min > 59)
            {
                min = 0;
                hour++;
            }
            if (hour > 23)
            {
                hour = 0;
            }

            CurrentTime();

            sec++;
        }
    }

    void CurrentTime()
    {
        if (sec < 10) s = "0" + sec; else s = sec.ToString();
        if (min < 10) m = "0" + min; else m = min.ToString();
        if (hour < 10) h = "0" + hour; else h = hour.ToString();
    }

    private void ResetGameTimer()
    {
        sec = 0;
        min = 0;
        hour = 0;

        CurrentTime();
    }

    void OnGUI()
    {
        switch (counterMode)
        {
            case CounterMode.hourMinSec:
                result = h + ":" + m + ":" + s;
                break;

            case CounterMode.minSec:
                result = m + ":" + s;
                break;
        }
    }
}