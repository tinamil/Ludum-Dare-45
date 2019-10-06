﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public SpriteRenderer selectionBoxIcon;

    private SpriteRenderer mouseDragBox;
    private Vector2 mouseDown;
    private Vector2 mouseUp;
    private List<Peasant> selectedPeasants;

    void Start()
    {
        mouseDragBox = Instantiate(selectionBoxIcon);
        mouseDragBox.gameObject.SetActive(false);
        selectedPeasants = new List<Peasant>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            foreach (var peasant in selectedPeasants)
            {
                peasant.SetSelected(false);
            }
            selectedPeasants.Clear();

            mouseDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDragBox.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDragBox.gameObject.SetActive(false);
            mouseUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var colliders = Physics2D.OverlapAreaAll(mouseDown, mouseUp);
            foreach (var c in colliders)
            {
                var peasant = c.GetComponentInParent<Peasant>();
                if (peasant != null)
                {
                    peasant.SetSelected(true);
                    selectedPeasants.Add(peasant);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDragBox.transform.position = (currentPosition + mouseDown) / 2.0f;
            mouseDragBox.size = currentPosition - mouseDown;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach(var peasant in selectedPeasants)
            {
                peasant.MoveTo(targetPosition);
            }
        }
    }

}
