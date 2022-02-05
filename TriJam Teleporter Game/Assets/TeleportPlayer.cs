using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TeleportPlayer : MonoBehaviour
{

    public Transform player;
    public List<Transform> teleporters = new List<Transform>();

    public bool onCooldown;
    public float cooldownTime;

    public LineRenderer lineRenderer;
    public Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            teleporters.Add(GameObject.Find($"Teleporter{i}").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                targetLocation = teleporters[4].position;
                TeleportTo(5);
                StartCoroutine("Cooldown");
            }
        }
    }
    private void TeleportTo(int index)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(player.position, targetLocation);
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
