using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    public InputActionProperty peaceAnimation;
    public InputActionProperty grabAnimation;

    public Animator handAnimation;

    // Update is called once per frame
    private void Update()
    {
        float peaceValue = peaceAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Peace", peaceValue);
        float grabValue = grabAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Grab", grabValue);
    }
}
