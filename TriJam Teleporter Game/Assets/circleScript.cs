using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleScript : MonoBehaviour
{
    public float radius;

    public float Speed;

    [SerializeField]
    float startingAngle;

    public Vector2 startingLoc;

    float currentAngle;
    public static Vector2 findLocation(float radiusSet, float angleSet, Vector2 baseLocation)
    {
        Vector2 final;
        final.x = Mathf.Cos(angleSet * Mathf.Deg2Rad) * radiusSet + baseLocation.x;
        final.y = Mathf.Sin(angleSet * Mathf.Deg2Rad) * radiusSet + baseLocation.y;
        return final;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentAngle = startingAngle;
        transform.position = findLocation(radius, startingAngle, startingLoc);
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle += Time.deltaTime * Speed;
        transform.position = findLocation(radius, currentAngle, startingLoc);
    }
}
