using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;
    public bool isMoving;

    Vector2 min;
    Vector2 max;
    void Awake()
    {
        isMoving = false;

        min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
        max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

        //add the planet sprite half height to max y
        max.y = max.y + GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;

        //subtract the planet sprite half height to min y
        min.y = min.y - GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;

        //get current position of planet
        Vector2 position = transform.position;

        //compute the planet's new position
        position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

        //update the planet's position
        transform.position = position;

        //if the planet gets to the minimum y position. then stop moving the planet
        if (transform.position.y < min.y)
        {
            isMoving = false;
        }

    }

    //function to reset the planet's position
    public void ResetPosition()
    {
        //reset the position of planet to random x, and max y
        transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
    }
}
