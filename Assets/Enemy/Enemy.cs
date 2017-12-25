using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    CombatController combatController;
    float AttackTime = 2.5f;
    float TimeSinceLastAttack = 0;
    GameManager gameManager;
    PlayerController player;

    [SerializeField]
    float RotationSpeed = 1;

    public CombatController.ActionDirection AnticipateAttackDirection;

    public void TakeDamage(int damage)
    {
        combatController.Hit();
        gameManager.EnemyDamage(damage);
    }

    // Use this for initialization
    void Start () {
        combatController = GetComponent<CombatController>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        ChoseDefence();
    }
	
	// Update is called once per frame
	void Update () {

        //// Check to Attack
        if (Time.time > TimeSinceLastAttack + AttackTime +Random.Range(0,1))
        {
            TimeSinceLastAttack = Time.time;
            ChoseDefence();
            Attack();
        }

        if (combatController.currentState == CombatController.CombatState.Idle || combatController.currentState == CombatController.CombatState.Blocking)
        {
            //find the vector pointing from our position to the target
            Vector3 _direction = (player.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        }
    }

    void ChoseDefence()
    {
        int AnticaptionAtackIndex = Random.Range(1, 4);
        switch (AnticaptionAtackIndex)
        {
            case 1:
                AnticipateAttackDirection = CombatController.ActionDirection.ActionLeft;
                break;
            case 2:
                AnticipateAttackDirection = CombatController.ActionDirection.ActionOverhead;
                break;
            case 3:
                AnticipateAttackDirection = CombatController.ActionDirection.ActionRight;
                break;
        }
    }


    void Attack()
    {
        combatController.SetBlocking(false);
        CombatController.ActionDirection AttackDirection = CombatController.ActionDirection.ActionOverhead;
        int AttackDirectionIndex = Random.Range(1, 4);
        switch (AttackDirectionIndex)
        {
            case 1:
                AttackDirection = CombatController.ActionDirection.ActionLeft;
                break;
            case 2:
                AttackDirection = CombatController.ActionDirection.ActionOverhead;
                break;
            case 3:
                AttackDirection = CombatController.ActionDirection.ActionRight;
                break;
        }


        combatController.Action(AttackDirection);
    }

    public void AttackedFrom(CombatController.ActionDirection PlayerAttackDirection)
    {
        
        if (PlayerAttackDirection == CombatController.ActionDirection.ActionRight &&
            AnticipateAttackDirection == CombatController.ActionDirection.ActionLeft)
        {
            combatController.SetBlocking(true);
            combatController.Action(AnticipateAttackDirection);
        }
        else if(PlayerAttackDirection == CombatController.ActionDirection.ActionLeft &&
            AnticipateAttackDirection == CombatController.ActionDirection.ActionRight)
        {
            combatController.SetBlocking(true);
            combatController.Action(AnticipateAttackDirection);
        }
        else if (PlayerAttackDirection == CombatController.ActionDirection.ActionOverhead &&
            AnticipateAttackDirection == CombatController.ActionDirection.ActionOverhead)
        {
            combatController.SetBlocking(true);
            combatController.Action(AnticipateAttackDirection);
        }

    } 


}
