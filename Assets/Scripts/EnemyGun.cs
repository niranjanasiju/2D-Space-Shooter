using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletGO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke ("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find ("PlayerGO");

        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            //bullet.GetComponent<EnemyBulletGO>().SetDirection(direction);

            // Set the direction on the EnemyBullet script
            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
            else
            {
                Debug.LogError("EnemyBulletGO does not have an EnemyBullet script attached.");
            }

        }
    }
}
