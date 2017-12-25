using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    int Damage = 10;

    bool AttackStateEnabled = false;
    bool AttackTrigger = false;

    TrailRenderer Trail;
	void Start () {
        Trail = GetComponentInChildren<TrailRenderer>();
        Trail.time = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAttackState(bool value)
    {
        AttackStateEnabled = value;
        if (value)
        {
            Trail.time = .4f;
        }
        else
        {
            AttackTrigger = false;
            Trail.time = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (AttackStateEnabled && !AttackTrigger)
        {
            //if (other.GetComponent<Weapon>())
            //{
            //    //Debug.Log("HitWeapon");
            //    //Debug.Log(other.GetComponentInParent<CombatController>().currentBlockState + ": BlockState");
            //    //Debug.Log(GetComponentInParent<CombatController>().currentAttackState + ": AttackState");


            //    if (other.GetComponentInParent<CombatController>().currentBlockState == CombatController.BlockState.BlockRight
            //        && GetComponentInParent<CombatController>().currentAttackState == CombatController.AttackState.AttackRight)
            //    {
            //        GetComponentInParent<CombatController>().AttackInterrupt();
            //    }
            //    else if (other.GetComponentInParent<CombatController>().currentBlockState == CombatController.BlockState.BlockRight
            //       && GetComponentInParent<CombatController>().currentAttackState == CombatController.AttackState.AttackLeft)
            //    {
            //        GetComponentInParent<CombatController>().AttackInterrupt();
            //    }
            //    else if (other.GetComponentInParent<CombatController>().currentBlockState == CombatController.BlockState.BlockOverhead
            //       && GetComponentInParent<CombatController>().currentAttackState == CombatController.AttackState.AttackOverhead)
            //    {
            //        GetComponentInParent<CombatController>().AttackInterrupt();
            //    }
            //}
            if (other.GetComponent<Enemy>() && other.GetComponentInChildren<Weapon>() != this)
            {
                AttackTrigger = true;
                other.GetComponent<Enemy>().TakeDamage(Damage);
            }
            else if (other.GetComponent<PlayerController>() && other.GetComponentInChildren<Weapon>() != this)
            {
                AttackTrigger = true;
                other.GetComponent<PlayerController>().TakeDamage(Damage);
            }
        }
    }
}
