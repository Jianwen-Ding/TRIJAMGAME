using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class bossScript : MonoBehaviour
{
    public int health;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    public static float timePassed = 0;
    //phase 1
    [SerializeField]
    float temporaryAngle0 = 0;
    [SerializeField]
    float temporarySpd0;
    [SerializeField]
    float temporaryRd0;
    //phase 2
    [SerializeField]
    float temporarySpd1;
    //phase 3
    [SerializeField]
    float temporaryAcel2;
    [SerializeField]
    float temporaryMaxVel2;
    //phase 4
    [SerializeField]
    float temporaryAngle3 = 0;
    [SerializeField]
    float temporarySpd3;
    [SerializeField]
    float temporaryRd3;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TeleportPlayer>() != null)
        {
            collision.gameObject.GetComponent<TeleportPlayer>().damage();
        }
    }
    public static float findAngle(Vector3 point1, Vector3 point2){
        return Mathf.Atan2(point2.y - point1.y, point2.x - point1.x) * Mathf.Rad2Deg;
    }
    public static float findAngle(float x, float y)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (health < 1)
        {
            SceneManager.LoadScene("Win");
        }
        if(timePassed < 16)
        {
            temporaryAngle0 += temporarySpd0 * Time.deltaTime;
            transform.position = circleScript.findLocation(temporaryRd0,temporaryAngle0, new Vector2(0,0));
        }
        else if (timePassed < 27)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporarySpd1, findAngle(gameObject.transform.position, Player.transform.position), new Vector2(0, 0));
        }
        else if (timePassed < 50)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(circleScript.findLocation(temporaryAcel2, findAngle(gameObject.transform.position, Player.transform.position), new Vector2(0, 0)), ForceMode2D.Force);
            if (Mathf.Sqrt(gameObject.GetComponent<Rigidbody2D>().velocity.x* gameObject.GetComponent<Rigidbody2D>().velocity.x + gameObject.GetComponent<Rigidbody2D>().velocity.y* gameObject.GetComponent<Rigidbody2D>().velocity.y) > temporaryMaxVel2)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporaryMaxVel2, findAngle(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y), new Vector2(0, 0));
            }
        }
        else
        {
            temporaryAngle3 += temporarySpd3 * Time.deltaTime;
            transform.position = circleScript.findLocation(temporaryRd3, temporaryAngle3, new Vector2(0, 0));
        }
    }
}
