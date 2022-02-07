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
        if (onCooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //Change the button if needed
            {
                targetLocation = teleporters[0].position;
                TeleportTo(1);
                StartCoroutine("Cooldown");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                targetLocation = teleporters[1].position;
                TeleportTo(2);
                StartCoroutine("Cooldown");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                targetLocation = teleporters[2].position;
                TeleportTo(3);
                StartCoroutine("Cooldown");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                targetLocation = teleporters[3].position;
                TeleportTo(4);
                StartCoroutine("Cooldown");
            }
        }
    }
    private void TeleportTo(int index)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(player.position, targetLocation);
        if(hitInfo.collider != null )
        {
            if (hitInfo.collider.gameObject.GetComponent<bossScript>() != null)
            {
                hitInfo.collider.gameObject.GetComponent<bossScript>().health--;
            }
            
        }
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, targetLocation);
        player.position = teleporters[index - 1].position;
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
