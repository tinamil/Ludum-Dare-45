using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Peasant : MonoBehaviour
{
    public GameObject selectionBox;
    public GroundManager ground;
    public float speed;
    public SpriteRenderer sprite;
    public Animator animator;

    Vector2 targetMove;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        ground = GroundManager.GetInstance();
    }

    public void SetSelected(bool set)
    {
        selectionBox.SetActive(set);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        targetMove = targetPosition;
        sprite.flipX = targetMove.x - transform.position.x < 0;

        Debug.Log("Moving from " + transform.position + " to " + targetMove);

        moving = true;
        animator.SetBool("walking", true);
    }

    private void Update()
    {
        if (moving)
        {
            transform.Translate((targetMove - (Vector2)transform.position).normalized * speed * Time.deltaTime);
        }
        if (moving && Vector2.Distance(targetMove, transform.position) < 0.1f)
        {
            moving = false;
            animator.SetBool("walking", false);
        }
    }
}
