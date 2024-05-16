using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBounceSpeed : MonoBehaviour
{
    private float intialX;
    private float intialZ;
    private Animator animator;


    void Start()
    {
        intialX = transform.localPosition.x;
        intialZ = transform.localPosition.z;


        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.speed = Random.Range(0.5f, 1.5f);
        }
    }

    void Update()
    {
        Vector3 position = transform.localPosition;
        position.x = intialX;
        position.z = intialZ;
        transform.localPosition = position;
    }
}
