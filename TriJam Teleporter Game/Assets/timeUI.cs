using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class timeUI : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject Player;
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Seconds passed: " + (int)bossScript.timePassed + "\n" + "Player Health: " + Player.GetComponent<TeleportPlayer>().getHealth() + "\n" + "Enemy Health: " + boss.GetComponent<bossScript>().health;
    }
}
