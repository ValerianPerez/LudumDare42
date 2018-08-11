using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Planet : MonoBehaviour {


    Image img;
    Text text;
    public Sprite[] template;
    MyPlanet myPlanet;

	// Use this for initialization
	void Start () {
        img = this.gameObject.GetComponent<Image>();
        text = this.gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generatePlanet()
    {
        //Si la planète n'a pas déjà été générée on la génère
        if (this.myPlanet == null) {
            this.myPlanet = new MyPlanet();
            this.changePlanetSkin(myPlanet.getPlanetType().getType());
        }
        
    }

    // Change l'apparence de la planète
    void changePlanetSkin(int type)
    {
        this.img.sprite = template[type];
        this.text.text = "";
    }

    // Affiche les infos connues de la planète
    public void getPlanetInfo()
    {
         

        if (this.myPlanet != null)
        {
            Debug.Log("Planet name: " + myPlanet.getName());
            Debug.Log("Temperature: " + myPlanet.getTemperature());
            Debug.Log("Radiation: " + myPlanet.getRadiation());
            Debug.Log("Water coeficient: " + myPlanet.getCoeficienEau());
            Debug.Log("Desaster speed: " + myPlanet.getVitesseDesastre());
        }
        else
        {
            Debug.Log("Unknow");
            Debug.Log("Planet name: Unknow");
            Debug.Log("Temperature: Unknow");
            Debug.Log("Radiation: Unknow");
            Debug.Log("Water coeficient: Unknow");
            Debug.Log("Desaster speed: Unknow");
        }
       
    }


    class MyPlanet
    {
        PlanetType planetType;
        string name; // Nom de la planète
        double temperature; // Temperature de la planete
        double radiation; // Niveau de radiation de la planete
        double coeficienEau; // Coeficien de remplissage des ressources en eau
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

        public double getRadiation()
        {
            return this.radiation;
        }

        public double getCoeficienEau()
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
        const int NOMBRE_TYPE = 6;
        
        // Constructeur
        public PlanetType()
        {
            this.type = Random.Range(0, NOMBRE_TYPE - 1);
        }

        public int getType()
        {
            return this.type;
        }

        // Détermine la température de départ d'une planète
        public double getDepartTemperature()
        {
            double temp = 0; 

            if (type == 0)
            {
                temp = Random.Range(35, 40);
            }else if (type == 1)
            {
                temp = Random.Range(2, 40);
            }else if (type == 2 || type == 4)
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
        public double getDepartRadiation()
        {
            double rad = 0;

            if (type == 5)
            {
                rad = Random.Range(1, 5);
            }

            return rad;
        }

        // Détermine le coeficient d'eau d'une planète
        public double getCoefEau()
        {
            double coef = 0;

            if (type == 0 || type == 2)
            {
                coef = Random.Range(0.5f, 0.8f);
            }
            else if (type == 1 || type == 4)
            {
                coef = 0;
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
    }
}
