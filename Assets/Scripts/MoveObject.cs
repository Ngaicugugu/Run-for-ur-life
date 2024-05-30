using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float delay;
    [SerializeField] private float duration; 

    void Start()
    {
        // Gọi phương thức MoveObject sau một khoảng thời gian delay
        DOTween.Init();
        DOVirtual.DelayedCall(delay, MovingObject);
    }

    
    void MovingObject()
    {
        if (targetPosition != null)
        {
            
            transform.DOMove(targetPosition, duration);
        }
    }
}
