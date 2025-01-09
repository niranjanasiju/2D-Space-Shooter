using UnityEngine;
using System.Collections;

public class PlayerBulletControl : MonoBehaviour
{
    float speed;
    
   // Use this for initialization
   void Start()
    {
        speed = 8f;
    }



    // Update is called once per frame
    void Update()
    {
        //Get the bullet's current position
        Vector2 position = transform.position;

        //Compute the bullets new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the bullet's position
        transform.position = position;

        //this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if the bullet went out of the screen , destroy it
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision of player bullet with enemy ship 
        if (col.tag == "EnemyShipTag") 
        {
            Destroy(gameObject);//Destroy the player bullet
        }
    }
}
