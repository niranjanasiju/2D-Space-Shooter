using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO;//reference to score ui
    public GameObject ExplosionGO;//explosion prefab
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float speed;

    

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        //get score text ui
        scoreUITextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision of enemy with player or player bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            //add 100 points to score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);//Destroy the enemy ship
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
