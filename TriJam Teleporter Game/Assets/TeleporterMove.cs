using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterMove : MonoBehaviour
{
    public float accel;
    public float speed;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    // Update is called once per frame
    void Update()
    {
        speed += accel * Time.deltaTime;
        float x = gameObject.transform.position.x;
        if(x < maxX)
        {
            gameObject.transform.position = new Vector2(x + Time.deltaTime * speed, gameObject.transform.position.y);
        }
        else
        {
            gameObject.transform.position = new Vector2(minX, Random.Range(minY, maxY));
        }
    }
}
