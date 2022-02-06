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
    GameObject[] Teleporters;
    [SerializeField]
    public static float timePassed = 0;
    //phase 0
    [SerializeField]
    float temporarySpd0;
    [SerializeField]
    int temporaryIndex0 = 0;
    [SerializeField]
    float temporarySwitchTime0;
    [SerializeField]
    float temporarySwitchTimeLeft0 = 0;
    //phase 1
    [SerializeField]
    float temporaryAngle1 = 0;
    [SerializeField]
    float temporarySpd1;
    [SerializeField]
    float temporaryRd1;
    bool hasStarted1 = false;
    //phase 2
    [SerializeField]
    float temporarySpd2;
    [SerializeField]
    int temporaryIndex2 = 0;
    [SerializeField]
    float temporarySwitchTime2;
    [SerializeField]
    float temporarySwitchTimeLeft2 = 0;
    //phase 3
    [SerializeField]
    float temporaryAcel3;
    [SerializeField]
    float temporaryMaxVel3;
    //phase 4
    [SerializeField]
    float temporaryAngle4 = 0;
    [SerializeField]
    float temporarySpd4;
    [SerializeField]
    float temporaryRd4;
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
        if (timePassed < 8)
        {
            temporarySwitchTimeLeft0 -= Time.deltaTime;
            if (temporarySwitchTimeLeft0 <= 0)
            {
                temporarySwitchTimeLeft0 = temporarySwitchTime2;
                temporaryIndex0++;
                if(temporaryIndex0 >= Teleporters.Length)
                {
                    temporaryIndex0 = 0;
                }
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporarySpd0, findAngle(gameObject.transform.position, Teleporters[temporaryIndex0].transform.position), new Vector2(0, 0));
        }
        else if (timePassed < 16)
        {
            if (hasStarted1)
            {
                temporaryAngle1 += temporarySpd1 * Time.deltaTime;
                transform.position = circleScript.findLocation(temporaryRd1, temporaryAngle1, new Vector2(0, 0));
            }
            else
            {
                hasStarted1 = true;
                temporaryAngle1 = findAngle(new Vector3(0,0), transform.position);
            }
        }
        else if (timePassed < 27)
        {
            temporarySwitchTimeLeft2 -= Time.deltaTime;
            if (temporarySwitchTimeLeft2 <= 0)
            {
                temporarySwitchTimeLeft2 = temporarySwitchTime2;
                temporaryIndex2 = Random.Range(0,Teleporters.Length);
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporarySpd2, findAngle(gameObject.transform.position, Teleporters[temporaryIndex2].transform.position), new Vector2(0, 0));
        }
        else if (timePassed < 38)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(circleScript.findLocation(temporaryAcel3, findAngle(gameObject.transform.position, Player.transform.position), new Vector2(0, 0)), ForceMode2D.Force);
            if (Mathf.Sqrt(gameObject.GetComponent<Rigidbody2D>().velocity.x* gameObject.GetComponent<Rigidbody2D>().velocity.x + gameObject.GetComponent<Rigidbody2D>().velocity.y* gameObject.GetComponent<Rigidbody2D>().velocity.y) > temporaryMaxVel3)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporaryMaxVel3, findAngle(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y), new Vector2(0, 0));
            }
        }
        else
        {
            temporaryAngle4 += temporarySpd4 * Time.deltaTime;
            transform.position = circleScript.findLocation(temporaryRd4, temporaryAngle4, new Vector2(0, 0));
        }
    }
}
