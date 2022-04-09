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
    [SerializeField]
    int State = 0;
    [SerializeField]
    float timeUntilNextState = 0;
    [SerializeField]
    float timeUntileNextStateLeft = 0;
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
    GameObject projectilePrefab;
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
        Teleporters = GameObject.FindGameObjectsWithTag("Tele");
        timePassed = 0;
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
        Teleporters = GameObject.FindGameObjectsWithTag("Tele");
        timePassed += Time.deltaTime;
        timeUntileNextStateLeft += Time.deltaTime;
        if(timeUntilNextState < timeUntileNextStateLeft)
        {
            State = Random.Range(0, 4);

            timeUntileNextStateLeft = 0;
        }
        if (health < 1)
        {
            SceneManager.LoadScene("Win");
        }
        if (State == 0)
        {
            if (temporaryIndex0 >= 0 && temporaryIndex0 < Teleporters.Length)
            {
                temporarySwitchTimeLeft0 -= Time.deltaTime;
                if (temporarySwitchTimeLeft0 <= 0)
                {
                    Destroy(Teleporters[temporaryIndex0]);
                    temporarySwitchTimeLeft0 = temporarySwitchTime0;
                    temporaryIndex0++;
                    if (temporaryIndex0 >= Teleporters.Length)
                    {
                        temporaryIndex0 = 0;
                    }
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporarySpd0, findAngle(gameObject.transform.position, Teleporters[temporaryIndex0].transform.position), new Vector2(0, 0));
            }
        }
        else if (State == 1)
        {
            if (hasStarted1)
            {
                temporaryRd1 -= Time.deltaTime * (float)0.5;
                temporaryAngle1 += temporarySpd1 * Time.deltaTime;
                transform.position = circleScript.findLocation(temporaryRd1, temporaryAngle1, new Vector2(0, 0));
            }
            else
            {
                hasStarted1 = true;
                temporaryRd1 -= 1;
                temporaryAngle1 = findAngle(new Vector3(0,0), transform.position);
            }
        }
        else if (State == 2)
        {
            temporarySwitchTimeLeft2 -= Time.deltaTime;
            if (temporarySwitchTimeLeft2 <= 0)
            {
                temporarySwitchTimeLeft2 = temporarySwitchTime2;
                GameObject w = Instantiate(projectilePrefab, transform.position, Quaternion.identity.normalized);
                GameObject s = Instantiate(projectilePrefab, transform.position, Quaternion.identity.normalized);
                GameObject a = Instantiate(projectilePrefab, transform.position, Quaternion.identity.normalized);
                GameObject d = Instantiate(projectilePrefab, transform.position, Quaternion.identity.normalized);
                w.GetComponent<Projectile>().angle = 135;
                s.GetComponent<Projectile>().angle = 315;
                a.GetComponent<Projectile>().angle = 225;
                d.GetComponent<Projectile>().angle = 45;
            }
               
            gameObject.GetComponent<Rigidbody2D>().velocity = circleScript.findLocation(temporarySpd2, findAngle(gameObject.transform.position, Player.transform.position), new Vector2(0, 0));
        }
        else if (State == 3)
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
