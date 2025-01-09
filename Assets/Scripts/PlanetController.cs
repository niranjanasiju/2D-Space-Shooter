using UnityEngine;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets;//an array of PlanetGO prefabs

    Queue<GameObject> availablePlanets = new Queue<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availablePlanets.Enqueue (Planets [0]);
        availablePlanets.Enqueue (Planets [1]);
        availablePlanets.Enqueue (Planets [2]);
        
        //call the MovePlanetDown function every 20 seconds
        InvokeRepeating ("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to dequeue a planet and set its isMoving flag to true
    void MovePlanetDown()
    {
        EnqueuePlanets ();
        //if the queue is empty, then return
        if (availablePlanets.Count == 0)
            return;
        //get a planet from the queue
        GameObject aplanet = availablePlanets.Dequeue();

        //set the planet isMoving flag to true
        aplanet.GetComponent<Planet>().isMoving = true;
    }

    //function to enqueu planets that are below the screen and are not mving
    void EnqueuePlanets()
    {
        foreach(GameObject aPlanet in Planets)
        {
            if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                //reset the planet position
                aPlanet.GetComponent<Planet>().ResetPosition();

                //Enqueue the planet
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
