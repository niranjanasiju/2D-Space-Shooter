using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
    public GameObject GameManagerGO;//reference to game manager
	public GameObject PlayerBulletGO;//player bullet prefab
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;
    public GameObject ExplosionGO;//explosion prefab

    //reference to the level ui text
    public TMP_Text LivesUIText;
    const int MaxLives = 3;//max player lives
    int lives;//current lives
    public float speed;

    public void Init()
    {
        lives = MaxLives;
        //update lives ui text
        LivesUIText.text = lives.ToString ();

        //reset the player position to the center of te screen
        transform.position = new Vector2 (0,0);

        //set player game object to active
        gameObject.SetActive (true);
    }

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

        //fire bullet's when spacebar is pressed
        if(Input.GetKeyDown("space"))
        {
            //play laser sound when spacebar is pressed
            GetComponent<AudioSource>().Play();
            
            GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
            bullet01.transform.position = BulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
            bullet02.transform.position = BulletPosition02.transform.position;
        }
		float x = Input.GetAxisRaw ("Horizontal");//the value will be -1, 0 or 1 (for left, no input, and right)
		float y = Input.GetAxisRaw ("Vertical");//the value will be -1, 0 or 1 (for down, no input, and up)

		//now based on the input we compute a direction vector, and we normalize it to get a unit vector
		Vector2 direction = new Vector2 (x, y).normalized;

		//noe we call the function that computes and sets the player's position
		Move (direction);
	}

	void Move(Vector2 direction)
	{
		//find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)); //this is the bottom-left point (corner) of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); //this is the top-right point (corner) of the screen

		max.x = max.x - 0.225f; //subtract the player sprite half width
		min.x = min.x + 0.225f; //add the player sprite half width

		max.y = max.y - 0.285f; //subtract the player sprite half height
		min.y = min.y + 0.285f; //add the player sprite half height

		//Get the player's current position
		Vector2 pos = transform.position;

		//Calculate the new position
		pos += direction * speed * Time.deltaTime;

		//Make sure the new position is outside the screen
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//Update the player's position
		transform.position = pos;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision of player with enemy or enemy bullet
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;//loose one life
            LivesUIText.text = lives.ToString();
            if (lives == 0)
            {
                //change game manager state to game over state
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);//hide the player ship
            }
            
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
