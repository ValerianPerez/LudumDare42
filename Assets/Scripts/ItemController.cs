﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    /// <summary>
    /// Define if the item is grab
    /// </summary>
    public bool IsGrab { get; set; }

    /// <summary>
    /// The animator of object
    /// </summary>
    private Animator Anim;

    protected void Start()
    {
        Grab();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (IsGrab)
        {
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100); // 100 is the plane distance onthe UI canvas
            transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            //transform.position = Input.mousePosition;
        }
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
        IsGrab = true;
    }

    /// <summary>
    /// Relase the object
    /// </summary>
    public void Release()
    {
        IsGrab = false;
    }

    /// <summary>
    /// Release the object at the specified position
    /// </summary>
    /// <param name="position"></param>
    public void ReleaseAt(Vector2 position)
    {
        Release();

        float yOffest = GetComponent<RectTransform>().sizeDelta.x / 2;

        transform.position = position;

        transform.localPosition += new Vector3(0, yOffest, 0);

        Anim.SetTrigger("isGrowing");
    }
}
