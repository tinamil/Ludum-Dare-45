using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public Animator animator;
    public float RequiredWork;

    private float CurrentWork = 0;

    void Start()
    {
        
    }

    void Update()
    {

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
