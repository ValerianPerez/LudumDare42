﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    
    public Planet currentPlanet;
    
    
    public GameObject SceneManagerGO;

    CustomSceneManager sm;
    ResourceManager rm;
    
    public float StartingHumanResource;
    public float StartingFoodResource;



    // Use this for initialization
    void Start () {
        sm = SceneManagerGO.GetComponent<CustomSceneManager>();
        rm = SceneManagerGO.GetComponent<ResourceManager>();

        rm.FoodResource = StartingFoodResource;
        rm.HumanResource = StartingHumanResource;


	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Jump() {
    }
}
