using UnityEngine;
using System.Collections.Generic;

public class TimeRewindManager : MonoBehaviour
{
    private struct TimeSnapshot
    {
        public Vector3 position;
        public Quaternion rotation;
        public float elapsedTime;

        public TimeSnapshot(Vector3 pos, Quaternion rot, float time)
        {
            position = pos;
            rotation = rot;
            elapsedTime = time;
        }
    }

    private List<TimeSnapshot> snapshots = new List<TimeSnapshot>();
    private bool isRewinding = false;
    private float rewindSpeed = 0.1f;
    public TimeManipulation _tm;

    // Her güncelleme döngüsünde çağrılır
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRewind();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewind();
        }

        if (isRewinding)
        {
            RewindTime();
        }
        else
        {
            RecordTime();
        }
    }

    // Zamanı geri sarma işlemini başlatır
    void StartRewind()
    {
        isRewinding = true;
    }

    // Zamanı geri sarma işlemini durdurur
    void StopRewind()
    {
        isRewinding = false;
    }

    // Zamanı geri sarma işlevi
    void RewindTime()
    {
        if (snapshots.Count > 0)
        {
            TimeSnapshot snapshot = snapshots[snapshots.Count - 1];
            transform.position = Vector3.Lerp(transform.position, snapshot.position, rewindSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, snapshot.rotation, rewindSpeed);

            // Ekranda görünen zamanı geri sar
            _tm.DisplayTime( -snapshot.elapsedTime);

            snapshots.RemoveAt(snapshots.Count - 1);
        }
        else
        {
            StopRewind();
        }
    }

    // Mevcut durumu kaydeder
    void RecordTime()
    {
        TimeSnapshot snapshot = new TimeSnapshot(transform.position, transform.rotation, Time.time);
        snapshots.Add(snapshot);
    }

    
}
