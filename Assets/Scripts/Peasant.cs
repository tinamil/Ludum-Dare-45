using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Peasant : MonoBehaviour
{
    enum State
    {
        Idle, Walking, Building
    }

    private struct NextAction
    {
        Vector2 targetPosition;

    }
    public float walk_speed;
    public float building_speed;

    public GameObject selectionBox;
    public GroundManager ground;
    public SpriteRenderer sprite;
    public Animator animator;
    public Sprite icon;

    Vector2 targetMove;
    Building targetBuilding;

    State currentState = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        ground = GroundManager.GetInstance();
    }

    public void SetSelected(bool set)
    {
        selectionBox.SetActive(set);
    }

    void ChangeState(State newState)
    {
        currentState = newState;

        animator.SetBool("walking", currentState == State.Walking);
        animator.SetBool("constructing", currentState == State.Building);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeState(State.Idle);
        var dir = collision.ClosestPoint(transform.position);
        transform.Translate(dir.normalized * .01f);
    }


    public void InteractAt(Vector2 targetPosition)
    {
        var allColliders = Physics2D.OverlapPointAll(targetPosition);
        if (allColliders.Length == 0)
        {
            MoveTo(targetPosition);
        }
        else
        {
            foreach (var collider in allColliders)
            {
                var targetable = collider.transform.root.GetComponentInChildren<Building>();
                if (targetable != null)
                {
                    var distance = Vector2.Distance(transform.position, targetable.transform.position);
                    if (distance > 2)
                    {
                        MoveTo(targetPosition);
                    }
                    else
                    {
                        BeginConstruction(targetable);
                    }
                }
            }
        }
    }

    void BeginConstruction(Building b)
    {
        ChangeState(State.Building);
        b.BeginConstruction();
        targetBuilding = b;
    }

    private void MoveTo(Vector2 targetPosition)
    {
        targetMove = targetPosition;
        sprite.flipX = targetMove.x - transform.position.x < 0;

        ChangeState(State.Walking);
    }

    private void Update()
    {
        if (currentState == State.Walking)
        {
            transform.Translate((targetMove - (Vector2)transform.position).normalized * walk_speed * Time.deltaTime);

            if (Vector2.Distance(targetMove, transform.position) < 0.1f)
            {
                ChangeState(State.Idle);
            }
        }
        if (currentState == State.Building)
        {
            bool done = targetBuilding.AddConstructionProgress(building_speed * Time.deltaTime);
            if (done)
            {
                ChangeState(State.Idle);
            }
        }
    }
}
