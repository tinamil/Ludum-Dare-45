using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public Collider2D collisionBox;
    public Animator animator;
    public SpriteRenderer sprite;

    public float RequiredWork;

    private float CurrentWork = 0;

    [SerializeField]
    private bool ghost;

    void Start()
    {
      
    }

    void Update()
    {
        if (ghost)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void SetGhost(bool active)
    {
        ghost = active;
        //collisionBox.enabled = active;
        if (active)
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    public void BeginConstruction()
    {
        animator.SetTrigger("begin-construction");
    }

    public bool AddConstructionProgress(float work)
    {
        Debug.Log("Adding work: " + work + "; currentwork = " + CurrentWork);
        CurrentWork = Mathf.Min(RequiredWork, CurrentWork + work);
        animator.SetFloat("progress", CurrentWork / RequiredWork);

        return CurrentWork >= RequiredWork;
    }
}
