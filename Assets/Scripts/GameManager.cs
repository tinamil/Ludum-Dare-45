using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Vector2 mouseDown;
    private Vector2 mouseUp;
    bool released = false;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = Input.mousePosition;
            released = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUp = Input.mousePosition;
            released = true;
            var colliders = Physics2D.OverlapAreaAll(Camera.main.ScreenToWorldPoint(mouseDown), Camera.main.ScreenToWorldPoint(mouseUp), LayerMask.GetMask("Selectable"));
            foreach (var c in colliders)
            {
                Debug.Log("Found collider : " + c.name);
            }
        }
    }

    private void FixedUpdate()
    {
        if (released)
        {
            released = false;
        }
    }
}
