using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]
    int health;
    [SerializeField]
    float invulTime;
    [SerializeField]
    float invulTimeLeft;

    public Transform player;
    public List<Transform> teleporters = new List<Transform>();

    public bool onCooldown;
    public float cooldownTime;

    public LineRenderer lineRenderer;
    public Vector3 targetLocation;

    public int getHealth()
    {
        return health;
    }
    public void damage()
    {
        if (invulTimeLeft < 0)
        {
            health--;
            invulTimeLeft = invulTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (health < 1 || bossScript.timePassed > 60)
        {
            SceneManager.LoadScene("Lose");
        }
        if (invulTimeLeft >= 0)
        {
            invulTimeLeft -= Time.deltaTime;
        }
    }
    public static void TeleportTo(GameObject teleportToo)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        TeleportPlayer tp = player.GetComponent<TeleportPlayer>();
        if (tp.onCooldown == false)
        {
            tp.targetLocation = teleportToo.transform.position;
            RaycastHit2D hitInfo = Physics2D.Raycast(player.transform.position, teleportToo.transform.position);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.gameObject.GetComponent<bossScript>() != null)
                {
                    hitInfo.collider.gameObject.GetComponent<bossScript>().health--;
                }

            }
            tp.lineRenderer.SetPosition(0, player.transform.position);
            tp.lineRenderer.SetPosition(1, tp.targetLocation);
            player.transform.position = teleportToo.transform.position;
            tp.StartCoroutine("Cooldown");
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
        lineRenderer.enabled = false;
    }

}
