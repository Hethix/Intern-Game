/************************************************************************************
\file       AnimateHandOnInput.cs
\brief      This script ...
\author     #AUTHOR#
\date       #CREATIONDATE#
\copyright  © #YEAR#, #COMPANY# ApS. All rights reserved.
************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class AnimateHandOnInput : MonoBehaviour
{
	//==============================================================================
    // Fields
    //==============================================================================
	[SerializeField] private InputActionProperty pinchAnimationAction;
	[SerializeField] private InputActionProperty gripAnimationAction;

    private Animator animator;
	
	//==============================================================================
    // MonoBehaviour
    //=============================================================================
    void Start(){
        animator = GetComponent<Animator>();
    }


	void Update() {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);

    }
	
	//==============================================================================
    // Public Methods
    //==============================================================================
	
    
	//==============================================================================
    // Private Methods
    //==============================================================================
	
}
