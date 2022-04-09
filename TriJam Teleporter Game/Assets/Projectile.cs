using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public float angle;

    [SerializeField]
    float Speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TeleportPlayer>() != null)
        {
            collision.gameObject.GetComponent<TeleportPlayer>().damage();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(Speed, angle, new Vector2(0, 0));
    }
}
