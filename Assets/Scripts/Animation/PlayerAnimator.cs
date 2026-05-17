using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    
    void Awake() => animator = GetComponent<Animator>();

    public void UpdateAnimation(Vector2 direction, bool isMoving)
    {
        if(isMoving) lastDirection = direction;
        
        animator.SetFloat(MoveX, lastDirection.x);
        animator.SetFloat(MoveY, lastDirection.y);
        animator.SetBool(IsMoving, isMoving);
    }
}
