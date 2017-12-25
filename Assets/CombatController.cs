using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    // Use this for initialization
    Animator m_Animator;
    public bool Blocking = false;
    public enum ActionDirection { ActionRight, ActionLeft, ActionOverhead }

    public enum BlockState { BlockStateRight, BlockStateLeft, BlockStateOverhead, IdleState }
    public BlockState currentBlockState = BlockState.IdleState;

    public enum CombatState { Blocking, Attacking, Idle}
    public CombatState currentState = CombatState.Idle;

    public LayerMask Hitlayer;
    public CombatController Opponent;

    //bool hit = false;
    //Vector3 HitPosition;
    //Vector3 CurruentPosition;
    //[SerializeField]
    //float StumbleSpeed = 1;

    void Start () {
        m_Animator = GetComponent<Animator>();
        currentBlockState = BlockState.IdleState;
    }
	
	// Update is called once per frame
    public void Hit()
    {
        m_Animator.SetTrigger("Hit");
        //HitPosition = transform.position - Vector3.forward * 2;
        //CurruentPosition = transform.position;
        //hit = true;

    }

    //private void FixedUpdate()
    //{
    //    if (hit)
    //    {
    //        if ((transform.position - HitPosition).magnitude > .1f)
    //        {
    //          //  transform.position = Vector3.Lerp(transform.position, HitPosition, Time.deltaTime * StumbleSpeed);
    //        }
    //    }
    //}

    public void Action(ActionDirection currentAction)
    {
        if (currentState == CombatState.Idle)
        {
            currentState = Blocking ? CombatState.Blocking : CombatState.Attacking;

            switch (currentAction)
            {
                case ActionDirection.ActionRight:
                    m_Animator.SetTrigger("ActionRight");
                    break;
                case ActionDirection.ActionLeft:
                    m_Animator.SetTrigger("ActionLeft");
                    break;
                case ActionDirection.ActionOverhead:
                    m_Animator.SetTrigger("ActionOverhead");
                    break;
            }
        }
    }

    public void EnableAttack()
    {
        currentState = CombatState.Attacking;
        GetComponentInChildren<Weapon>().SetAttackState(true);
    }

    public void DisableAttack()
    {
        GetComponentInChildren<Weapon>().SetAttackState(false);
    }

    public void SetBlockState(BlockState bState)
    {
        currentState = CombatState.Blocking;
        currentBlockState = bState;
    }

    public void CheckForBlock(BlockState bState)
    {
        Debug.Log(Opponent.currentBlockState);
        Debug.Log(bState);
        if (Opponent.Blocking && Opponent.currentBlockState == bState)
        {
            AttackInterrupt();
        }

        //RaycastHit Hit;
        
        //if (Physics.Raycast(transform.position + Vector3.forward*.5f, Vector3.forward, out Hit, 20f, Hitlayer))
        //{
        //    CombatController OpponentCombatController = Hit.transform.GetComponent<CombatController>();
        //    Debug.Log(OpponentCombatController.currentActionState);
        //    Debug.Log(currentActionState);
        //    if (OpponentCombatController.Blocking && OpponentCombatController.currentActionState == currentActionState)
        //    {
        //        Debug.Log(OpponentCombatController.currentActionState);
        //        Debug.Log(currentActionState);
        //        AttackInterrupt();
        //    }
        //}
        //else
        //{
        //    Debug.Log("NothingHit");
        //}
    }

    public void ResetCombatState()
    {
        currentState = CombatState.Idle;
    }

    public void AttackInterrupt()
    {
        m_Animator.SetTrigger("AttackInterrupt");
    }

    public void ToggleBlocking()
    {
        if (Blocking)
        {
            Blocking = false;
            m_Animator.SetBool("Blocking", Blocking);
        }
        else
        {
            Blocking = true;
            m_Animator.SetBool("Blocking", Blocking);
        }
    }

    public void SetBlocking(bool value)
    {
        Blocking = value;
        m_Animator.SetBool("Blocking", Blocking);
    }
}
