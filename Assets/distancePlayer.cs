using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class distancePlayer : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public TMP_Text distanceText;
    public TMP_Text healthpoint;
    public float health;

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 enemyPos = new Vector2(enemy.position.x, enemy.position.y);
        float distance = Vector2.Distance(playerPos, enemyPos);
        distanceText.text = "Jarak: " + distance.ToString("F2") + " m";
    }
}
