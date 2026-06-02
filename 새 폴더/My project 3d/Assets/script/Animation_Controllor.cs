using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

public class Animation_Controllor : MonoBehaviour
{
    public Animator animator;
    public ExampleCharacterController character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", character.Motor.Velocity.magnitude);
    }
}
