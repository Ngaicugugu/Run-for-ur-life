using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float limitPos;
    [SerializeField] private float startPos;
    private float x;


    // Update is called once per frame
    void Update()
    {
        if (!ParallaxManager.instance.IsParallaxActive)
        {
            return;
        }

        x = transform.position.x;
        x += speed * Time.deltaTime;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);



        if (x <= limitPos)
        {
            x = startPos;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

    }
}
