using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Planet : MonoBehaviour {

    Canvas canvas;
    Image img;
    Text text;
    Text infoPlanete;
    public Sprite[] template;

    //<summary>
    // The current planet
    //</summary>
    [SerializeField]
    private MyPlanet myPlanet;

	// Use this for initialization
	void Start () {
        this.img = this.gameObject.GetComponent<Image>();
        this.text = this.gameObject.GetComponentInChildren<Text>();
        this.canvas = this.gameObject.GetComponentInParent<Canvas>();

        Transform textTr = this.canvas.transform.Find("Text_info_planete");
        this.infoPlanete = textTr.GetComponent<Text>();

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
            this.getPlanetInfo();
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
                "Water:: Unknow\n";
        } 
       
    }


    public void resetPlanetInfo()
    {
        this.infoPlanete.text = "";
    }


    public string GetName() {  return myPlanet.getName(); }

    public double GetTemperature() { return myPlanet.getTemperature(); }
    public double GetRadiation() { return myPlanet.getRadiation(); }
    public double GetWaterMultiplier() { return myPlanet.getCoeficienEau(); }
    public double GetDisasterFrequency() { return myPlanet.getVitesseDesastre(); }


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
            }else if (type == FROZEN_PLANET)
            {
                temp = Random.Range(2, 5);
            }else if (type == DESERT_PLANET || type == LOW_WATER_PLANET)
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

            if (type == RADIOACTIVE_PLANET)
            {
                rad = Random.Range(1, 5);
            }

            return rad;
        }

        // Détermine le coeficient d'eau d'une planète
        public double getCoefEau()
        {
            double coef = 0;

            if (type == BURNING_PLANET || type == DESERT_PLANET)
            {
                coef = Random.Range(0.5f, 0.8f);
            }
            else if (type == FROZEN_PLANET || type == LOW_WATER_PLANET)
            {
                coef = 0;
            }           
            else
            {
                coef = Random.Range(1.5f, 2.5f);
            }           

            return System.Math.Round(coef);
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
                case BURNING_PLANET: name = "Regulary burning";
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

            if(this.type == RADIOACTIVE_PLANET)
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
            }else if(this.type == LOW_WATER_PLANET){

                water = "Very Low";
            }else if (this.type == RADIOACTIVE_PLANET || this.type == FAST_GROW_PLANET)
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
