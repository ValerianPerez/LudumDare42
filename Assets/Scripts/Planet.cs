﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Planet : MonoBehaviour
{

    Canvas canvas;
    Image img;
    Text text;
    Text infoPlanete;
    Text infoTravel;
    public Sprite[] template;
    public planetManager sm;
    private int planetIndex;
    private Transform textTr_planet;
    private Transform textTr_travel;

    //<summary>
    // The current planet
    //</summary>
    [SerializeField]
    private MyPlanet myPlanet;

    // Use this for initialization
    void Start()
    {
                
        this.canvas = this.gameObject.GetComponentInParent<Canvas>();

        this.textTr_planet = this.transform.parent.Find("Text_info_planete");
        this.infoPlanete = textTr_planet.GetComponent<Text>();

        this.textTr_travel = this.transform.parent.Find("Text_info_travel");
        this.infoTravel = textTr_travel.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void generatePlanet()
    {
        
        if (((sm.listeVisited[this.planetIndex - 1] == true && sm.listeVisited[this.planetIndex] == false) || (this.planetIndex == 1 && sm.listeVisited[this.planetIndex] == false)) && this.planetIndex != 9)
        {
            //Si la planète n'a pas déjà été générée on la génère
            if (this.myPlanet == null)
            {
                this.myPlanet = new MyPlanet();
                this.changePlanetSkin(myPlanet.getPlanetType().getType());
                this.getPlanetInfo();
                this.infoTravel.text = "You are here\nClick on the planet to land";
                
            }
            else
            {
                //================================================================
                // Put here code to land on the planet
                GameObject.Find("SceneManager").GetComponent<CustomSceneManager>().TravelTo(this);
                //================================================================
                
                Debug.Log("land");

                // La planète a été visité 
                sm.listeVisited[this.planetIndex] = true;
            }           
            
        }else if (sm.listeVisited[this.planetIndex - 1] == true && this.planetIndex == 9)
        {
            //================================================================
            // Put win or losing condition here when palyer arrive to homeland
            GameObject.Find("SceneManager").GetComponent<CustomSceneManager>().WinTheGame();
            //================================================================            
            Debug.Log("game win");
        }

        

    }

    // Change l'apparence de la planète
    void changePlanetSkin(int type)
    {
        this.img = this.gameObject.GetComponent<Image>();
        this.text = this.gameObject.GetComponentInChildren<Text>();
        this.img.sprite = template[type];
        this.text.text = "";
    }

    // Affiche les infos connues de la planète
    public void getPlanetInfo()
    {
        this.canvas = this.gameObject.GetComponentInParent<Canvas>();
        this.textTr_planet = this.transform.parent.Find("Text_info_planete");
        this.infoPlanete = textTr_planet.GetComponent<Text>();

        this.textTr_travel = this.transform.parent.Find("Text_info_travel");
        this.infoTravel = textTr_travel.GetComponent<Text>();

        if (this.myPlanet != null)
        {
            this.infoPlanete.text = "Planet name: " + myPlanet.getName() + "\n" +
                 "Planet type: " + myPlanet.getPlanetType().getTypeName() + "\n" +
                "Temperature: " + myPlanet.getTemperature() + "\n" +
                "Radiation: " + myPlanet.getPlanetType().getRadInfo() + "\n" +
                "Water: " + myPlanet.getPlanetType().getWaterInfo() + "\n";
        }
        else
        {

            this.infoPlanete.text = "Planet name: Unknow\n" +
            "Planet type: Unknow\n" +
           "Temperature: Unknow\n" +
           "Radiation: Unknow\n" +
           "Water: Unknow\n";
            
        }

        if (sm.listeVisited[this.planetIndex - 1] == true && sm.listeVisited[this.planetIndex] == false && this.myPlanet == null)
        {
            this.infoTravel.text = "You Can travel to this planet";

        }else if (sm.listeVisited[this.planetIndex - 1] == true && sm.listeVisited[this.planetIndex] == false && this.myPlanet != null)
        {
            this.infoTravel.text = "You are here\nClick on the planet to land";
        }
        else if (sm.listeVisited[this.planetIndex] == true)
        {
            this.infoTravel.text = "You can't return to this planet";
        }
        else
        {
            this.infoTravel.text = "You Can't travel to this planet yet";
        }
    }

    // Affiche les infos de la dernière planète
    public void getFinalPlanetInfo()
    {
        this.infoPlanete.text = "Planet name: Homeland\n" +
               "Planet type: Habitable\n" +
              "Temperature: 26\n" +
              "Radiation: Low\n" +
              "Water: High\n";

        this.infoTravel.text = "Final goal";        
        
    }

    // Reinitialise le cadre des infos de planètes
    public void resetPlanetInfo()
    {
        this.infoPlanete.text = "";
    }


    public string GetName() { return myPlanet.getName(); }

    public double GetTemperature() { return myPlanet.getTemperature(); }
    public float GetRadiation() { return myPlanet.getRadiation(); }
    public float GetWaterMultiplier() { return myPlanet.getCoeficienEau(); }
    public double GetDisasterFrequency() { return myPlanet.getVitesseDesastre(); }   

    public int getPlanetIndex()
    {
        return this.planetIndex;
    }

    public void setPlanetIndex(int index)
    {
        this.planetIndex = index;
    }

    class MyPlanet
    {
        PlanetType planetType;
        string name; // Nom de la planète
        double temperature; // Temperature de la planete
        float radiation; // Niveau de radiation de la planete
        float coeficienEau; // Coeficien de remplissage des ressources en eau
        double vitesseDesastre; // Influe sur la vitesse d'apparition des desastres

        // Constructeur
        public MyPlanet()
        {
            this.planetType = new PlanetType();
            this.name = "default";
            this.temperature = this.planetType.getDepartTemperature();
            this.radiation = this.planetType.getDepartRadiation();
            this.coeficienEau = this.planetType.getCoefEau();
            this.vitesseDesastre = this.planetType.getVitesseDesastre();
        }

        public PlanetType getPlanetType()
        {
            return this.planetType;
        }

        public string getName()
        {
            return this.name;
        }

        public double getTemperature()
        {
            return this.temperature;
        }

        public float getRadiation()
        {
            return this.radiation;
        }

        public float getCoeficienEau()
        {
            return this.coeficienEau;
        }

        public double getVitesseDesastre()
        {
            return this.vitesseDesastre;
        }

    }

    class PlanetType
    {
        /*
         * type 0 = planète brûlante
         * La planète est victime de plus en plus de vague a hausse de température qui brûle de plus
         * en plus de ressources (et fait bouillir l'eau ?)
         * 
         * type 1 = planète gélée
         * La terre et les ressources gèles de plus en plus vite à mesure que la temperature baisse
         * 
         * type 2 = planète désertique
         * Les cases deviennent non cultivable lorsqu'elles sont recouvertent par le desert
         * 
         * type 3 = planète à croissance rapide
         * Les cultures grandisent vite mais la végetation recouvre de plus en plus vite le sol
         * 
         * type 4 = planète peu d'eau
         * L'eau ne re renouvelle pas
         * 
         * type 5 = planète radioactive 
         * L'eau devient de plus en plus radioactive
         */

        int type;
        const int NOMBRE_TYPE = 6, BURNING_PLANET = 0, FROZEN_PLANET = 1,
            DESERT_PLANET = 2, FAST_GROW_PLANET = 3, LOW_WATER_PLANET = 4,
            RADIOACTIVE_PLANET = 5;

        // Constructeur
        public PlanetType()
        {
            this.type = Random.Range(0, NOMBRE_TYPE);

        }

        public int getType()
        {
            return this.type;
        }

        // Détermine la température de départ d'une planète
        public double getDepartTemperature()
        {
            double temp = 0;

            if (type == BURNING_PLANET)
            {
                temp = Random.Range(35, 40);
            }
            else if (type == FROZEN_PLANET)
            {
                temp = Random.Range(2, 5);
            }
            else if (type == DESERT_PLANET || type == LOW_WATER_PLANET)
            {
                temp = Random.Range(40, 45);
            }
            else
            {
                temp = Random.Range(20, 30);
            }

            return temp;
        }

        // Détermine la radiation de départ d'une planète
        public float getDepartRadiation()
        {
            float rad = 0;

            if (type == RADIOACTIVE_PLANET)
            {
                rad = Random.Range(3, 5);
            } else
            {
                rad = Random.Range(1, 3);
            }

            return rad;
        }

        // Détermine le coeficient d'eau d'une planète
        public float getCoefEau()
        {
            float coef = 0;

            if (type == BURNING_PLANET || type == DESERT_PLANET)
            {
                coef = Random.Range(0.2f, 0.5f);
            }
            else if (type == FROZEN_PLANET || type == LOW_WATER_PLANET)
            {
                coef = Random.Range(0.4f, 0.6f);
            }
            else
            {
                coef = Random.Range(1.5f, 2.5f);
            }

            return coef;
        }

        // Détermine le coeficient de vitesse de départ des désastres

        public double getVitesseDesastre()
        {

            //TODO Récuperer la difficulté du gameManager et changer les valeurs random en fonction

            double vitesse = Random.Range(0.5f, 1.5f);

            return vitesse;
        }

        public string getTypeName()
        {
            string name = "Unknow";

            switch (this.type)
            {
                case BURNING_PLANET:
                    name = "Regulary burning";
                    break;
                case FROZEN_PLANET:
                    name = "Frozen";
                    break;
                case DESERT_PLANET:
                    name = "Desert";
                    break;
                case FAST_GROW_PLANET:
                    name = "Fast growing";
                    break;
                case LOW_WATER_PLANET:
                    name = "Low water";
                    break;
                case RADIOACTIVE_PLANET:
                    name = "Lot of radiation";
                    break;
                default:
                    name = "Unknow";
                    break;
            }

            return name;
        }

        // Renvoi les infos sur la radioavtivité
        public string getRadInfo()
        {
            string rad = "Low";

            if (this.type == RADIOACTIVE_PLANET)
            {
                rad = "High";
            }

            return rad;
        }

        // Renvoi les infos sur l'eau
        public string getWaterInfo()
        {
            string water = "Low";

            if (this.type == BURNING_PLANET || this.type == DESERT_PLANET)
            {
                water = "Low";
            }
            else if (this.type == LOW_WATER_PLANET)
            {

                water = "Very Low";
            }
            else if (this.type == RADIOACTIVE_PLANET || this.type == FAST_GROW_PLANET)
            {
                water = "High";
            }
            else
            {
                water = "Very high";
            }

            return water;
        }
    }
}
