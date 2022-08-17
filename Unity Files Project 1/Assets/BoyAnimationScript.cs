using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyAnimationScript : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update

    public void MoveAnimation()
    {
        animator.SetFloat("Speed", gameObject.GetComponent<BoyMoveScript>().forwardSpeed);
    }
}
