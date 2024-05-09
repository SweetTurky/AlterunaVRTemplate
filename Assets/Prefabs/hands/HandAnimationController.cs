using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Avatar = Alteruna.Avatar;

public class HandAnimationController : MonoBehaviour
{
    public InputActionProperty peaceAnimation;
    public InputActionProperty grabAnimation;

    public Animator handAnimation;

    private Avatar _avatar;
    // Start is called before the first frame update
    private void Awake()
    {
        _avatar = GetComponentInParent<Avatar>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (!_avatar.IsMe)
        {
            return;
        }

        float peaceValue = peaceAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Peace", peaceValue);
        float grabValue = grabAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Grab", grabValue);
    }
}
