using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject StarGO;//this is the starGO prefab
    public int MaxStars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

        //loop to create stars
        for (int i = 0; i < MaxStars; i++)
        {
            GameObject star = (GameObject)Instantiate(StarGO);

            //set random position of star
            star.transform.position = new Vector2(Random.Range(min.x , max.x), Random.Range(min.y,max.y));

            //set random speed for star
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            //make the star a child of the StarGeneratorGO
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
