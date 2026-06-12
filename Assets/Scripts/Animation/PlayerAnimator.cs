using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Animator happinessAnimator;
    
    private Vector2 lastDirection = Vector2.down;

    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private static readonly int HappinessState = Animator.StringToHash("happinessState");
    
    void Awake() => animator = GetComponent<Animator>();

    private void Start()
    {
        HappinessManager.instance.OnStateChanged += UpdateHappinessAnimation;
        UpdateHappinessAnimation(HappinessManager.instance.CurrentState);
    }

    void OnDestroy()
    {
        HappinessManager.instance.OnStateChanged -= UpdateHappinessAnimation;
    }

    public void UpdateAnimation(Vector2 direction, bool isMoving)
    {
        if(isMoving) lastDirection = direction;
        
        animator.SetFloat(MoveX, lastDirection.x);
        animator.SetFloat(MoveY, lastDirection.y);
        animator.SetBool(IsMoving, isMoving);
    }

    void UpdateHappinessAnimation(global::HappinessState state)
    {
        int index = state switch
        {
            global::HappinessState.Happy => 0,
            global::HappinessState.Stable => 1,
            global::HappinessState.Bad => 2,
            global::HappinessState.Depressed => 2,
            global::HappinessState.Collapse => 3,
            _ => 1
        };
        Debug.Log($"[Anim] 행복도 상태: {state} → index: {index}");
        happinessAnimator.SetInteger(HappinessState, index);
    }
}
