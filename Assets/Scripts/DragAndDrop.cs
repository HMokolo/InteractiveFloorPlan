﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord;
    private bool MouseActive = false;
    public float rotationSpeed = 50f;
    private Color myColor;

    private float currentScale = 1.0f;

    private bool selector = false;

    private Slider scaleSlide;

    [SerializeField]
    private KeyCode deleteObjectHotkey = KeyCode.Backspace;

    void Start()
    {
        scaleSlide = GameObject.Find("/AgentController/HUD/Slider").GetComponent<Slider>();
        myColor = GetComponent<Renderer>().material.GetColor("_Color");
    }

    void Update()
    {
        if (MouseActive)
        {
            if (Input.GetKey(KeyCode.X))
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Z))
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);

            if (Input.GetKeyDown(deleteObjectHotkey))
            {
                Destroy(this.gameObject);
            }

        }

        if(selector)
        {
            currentScale = scaleSlide.value;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (Input.GetKeyDown(deleteObjectHotkey))
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnMouseDown()
    {
        MouseActive = true;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        selector = !selector;
        HighlightObject(selector);

    }

    void HighlightObject(bool selector)
    {
        if (selector)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            scaleSlide.value = currentScale;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", myColor);
        }

    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    void OnMouseUp()
    {
        MouseActive = false;
    }

}