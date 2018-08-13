using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    /// <summary>
    /// The different state of a plant
    /// </summary>
    public enum PlantState
    {
        SEED,
        GROWING,
        MATURE,
        DYING,
        DEAD,
        SICK,
        BURNING,
        FROZEN
    }

    /// <summary>
    /// Define if the item is grab
    /// </summary>
    public bool IsPlanted { get; set; }

    /// <summary>
    /// The animator of object
    /// </summary>
    private Animator Anim;

    /// <summary>
    /// The current state of the plant
    /// </summary>
    private PlantState CurrentState;

    /// <summary>
    /// The duration on mature stare
    /// </summary>
    [SerializeField]
    private float MatureDuration;

    /// <summary>
    /// The current chrono
    /// </summary>
    private float Chrono;

    /// <summary>
    /// The amount of ressources gets when harvesting, multiply by a growth factor
    /// </summary>
    public float HarvestAmount;

    public float WaterDrain;

    protected void Start()
    {
        Anim = GetComponent<Animator>();
        CurrentState = PlantState.SEED;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!IsPlanted)
        {
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100); // 100 is the plane distance onthe UI canvas
            transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            //transform.position = Input.mousePosition;
        }

        if (CurrentState.Equals(PlantState.MATURE) && Chrono < MatureDuration)
        {
            Chrono += Time.deltaTime;

            if (Chrono > MatureDuration)
            {
                Anim.SetTrigger("isDying");
            }
        }
    }

    public void SoDryIWantToDie()
    {
        Anim.SetTrigger("isDying");
        Chrono = MatureDuration + 1;
        HarvestAmount = 0;
    }

    /// <summary>
    /// Activ the object 
    /// </summary>
	public void Active()
    {

    }

    /// <summary>
    /// Grab the object
    /// </summary>
    public void Grab()
    {
        IsPlanted = false;
    }

    /// <summary>
    /// Relase the object
    /// </summary>
    public void Release()
    {
        IsPlanted = true;
    }

    /// <summary>
    /// Release the object at the specified position
    /// </summary>
    /// <param name="position"></param>
    public void ReleaseAt(GameObject gameObject)
    {
        Release();

        float yOffest = GetComponent<RectTransform>().sizeDelta.x / 2;

        transform.position = gameObject.transform.position;

        transform.localPosition += new Vector3(0, yOffest, 0);

        Anim.SetTrigger("isGrowing");

        transform.SetParent(gameObject.transform);
    }

    /// <summary>
    /// Trigger when the growing animation begin
    /// </summary>
    public void TriggerGrowing()
    {
        CurrentState = PlantState.GROWING;
    }

    /// <summary>
    /// Trigger when the growing animation end
    /// </summary>
    public void TriggerMature()
    {
        CurrentState = PlantState.MATURE;
    }

    /// <summary>
    /// Trigger when the dying animation begin
    /// </summary>
    public void TriggerDying()
    {
        CurrentState = PlantState.DYING;
    }

    /// <summary>
    /// Trigger when the dying animation end
    /// </summary>
    public void TriggerDead()
    {
        CurrentState = PlantState.DEAD;
    }

    public float Harvest()
    {

        switch (CurrentState)
        {
            case PlantState.SEED:
                HarvestAmount = 0;
                break;
            case PlantState.GROWING:
                HarvestAmount *= .1f;
                break;
            case PlantState.MATURE:
                HarvestAmount *= 2;
                break;
            case PlantState.DYING:
                HarvestAmount = 0;
                break;
            case PlantState.DEAD:
                HarvestAmount = 0;
                break;
            case PlantState.SICK:
                HarvestAmount = 0;
                break;
            case PlantState.BURNING:
                HarvestAmount = 0;
                break;
            case PlantState.FROZEN:
                HarvestAmount = 0;
                break;
            default:
                break;
        }

        Destroy(gameObject, .05f);

        return HarvestAmount;
    }


    public float GetWaterDrain()
    {
        if (!IsPlanted)
        {
            return 0f;
        }

        float actual_drain = 0f;
        switch (CurrentState)
        {
            case PlantState.SEED:
                actual_drain = WaterDrain;
                break;
            case PlantState.GROWING:
                actual_drain = WaterDrain;
                break;
            case PlantState.MATURE:
                actual_drain = WaterDrain;
                break;
            case PlantState.DYING:
                actual_drain = WaterDrain / 3;
                break;
            case PlantState.DEAD:
                actual_drain = 0;
                break;
            case PlantState.SICK:
                actual_drain = WaterDrain;
                break;
            case PlantState.BURNING:
                actual_drain = 0;
                break;
            case PlantState.FROZEN:
                actual_drain = 0;
                break;
            default:
                break;
        }
        return actual_drain;
    }
}
