using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DOTween.Kill(collision.gameObject.transform);
            Destroy(collision.gameObject);
        }
    }
}
