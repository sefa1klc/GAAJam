using System;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private Vector3 maxPosition;
    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 targetPosition;
    private Vector3 currentPosition;
    public float _changetime { get; set; }

    private TimeManipulation _tm;
    public bool _inArea { get; set; }
    public bool isEnd { get; set; }
    
    private void Awake()
    {
        isEnd = true;
        _inArea = false;
        _changetime = 0;
    }

    void Update()
    {
        currentPosition = transform.position;

        if (_inArea)
        {
            // Fare tekerleği yukarı hareket ettiğinde
            if (Input.GetKey(KeyCode.Alpha1))
            {
                MoveUp();
            }
            // Fare tekerleği aşağı hareket ettiğinde
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                MoveDown();
            }
            
            ChangePos();
        }

    }
    
    void MoveUp()
    {
        // y yaptık çünkü sadece yüksekliğini karşıaştıracağız
        if (currentPosition.y <= maxPosition.y)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    void MoveDown()
    {
        if (currentPosition.y >= minPosition.y)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    public void ChangePos()
    {
        if (isEnd)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isEnd = false;
        }
    }
    
}