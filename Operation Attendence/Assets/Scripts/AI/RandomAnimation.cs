using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        IdleComplete();
    }

    public void IdleComplete()
    {
        if (animator.GetFloat("Speed") < 0.1f)
        {
            int randomIdleType = Random.Range(1, 4 + 1);
            animator.SetFloat("IdleStateBlend", (float)randomIdleType);
        }
    }
}
