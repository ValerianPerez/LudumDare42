using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetManager : MonoBehaviour
{

    public GameObject planete1;
    public GameObject planete2;
    public GameObject planete3;
    public GameObject planete4;
    public GameObject planete5;
    public GameObject planete6;
    public GameObject planete7;
    public GameObject planete8;
    public GameObject planete9;
    public List<bool> listeVisited;


    // Use this for initialization
    void Start()
    {
        for(int i = 0; i <= 9; i++)
        {
            listeVisited.Add(false);
        }

        listeVisited[0] = true;
        //listeVisited[1] = true;

        planete1.GetComponent<Planet>().setPlanetIndex(1);
        planete2.GetComponent<Planet>().setPlanetIndex(2);
        planete3.GetComponent<Planet>().setPlanetIndex(3);
        planete4.GetComponent<Planet>().setPlanetIndex(4);
        planete5.GetComponent<Planet>().setPlanetIndex(5);
        planete6.GetComponent<Planet>().setPlanetIndex(6);
        planete7.GetComponent<Planet>().setPlanetIndex(7);
        planete8.GetComponent<Planet>().setPlanetIndex(8);
        planete9.GetComponent<Planet>().setPlanetIndex(9);
        
        planete1.GetComponent<Planet>().generatePlanet();



    }
    // Update is called once per frame
    void Update()
    {

    }
}
