using System;
using UnityEngine;
using TMPro;

public class TimeManipulation : MonoBehaviour
{
    public float slowMotionFactor = 0.5f;
    public float fastForwardFactor = 2f;
    private float normalTimeScale = 1f;
    public TextMeshProUGUI time;
    public TextMeshProUGUI timeCarpan;
    private float elapsedTime = 0f;

    [SerializeField] PlatformMovement _platformMovement;
    [SerializeField] private DoorController _door;

  

    private void Awake()
    {
        
        if (time == null)
        {
            Debug.LogError("Time TextMeshProUGUI is not assigned in the Inspector");
        }

        if (timeCarpan == null)
        {
            Debug.LogError("TimeCarpan TextMeshProUGUI is not assigned in the Inspector");
        }

        if (timeCarpan != null)
        {
            timeCarpan.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SlowMotion();
            if (timeCarpan != null)
            {
                timeCarpan.enabled = true;
                timeCarpan.text = "x0.5";
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            FastForward();
            if (timeCarpan != null)
            {
                timeCarpan.enabled = true;
                timeCarpan.text = "x2";
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTimeScale();
            if (timeCarpan != null)
            {
                timeCarpan.enabled = false;
            }
        }

        elapsedTime += Time.deltaTime;
        DisplayTime(elapsedTime);
        
    }

    void SlowMotion()
    {
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void FastForward()
    {
        Time.timeScale = fastForwardFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void ResetTimeScale()
    {
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
    public void DisplayTime(float timeToDisplay)
    {
        if (time != null)
        {
            //timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            
            time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformArea"))
        {
            _platformMovement._inArea = true;
        }

        if (other.CompareTag("EndPlatformArea"))
        {
            _door.OpenDoor();
        }
    }
}
