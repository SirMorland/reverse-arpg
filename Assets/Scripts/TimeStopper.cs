using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopper : MonoBehaviour
{
    public void StopTime()
    {
        Time.timeScale = 0f;
    }
}
