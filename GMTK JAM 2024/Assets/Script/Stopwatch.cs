using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using system;
public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = false;
    float currentTime;
    public Text currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true) {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopwatch() {
        timerActive = true;
    }

    public void StopStopwatch() {
        timerActive = false;
    }

}
