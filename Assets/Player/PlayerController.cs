using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public Image SwordImage;
    public Image ShieldImage;


    Enemy enemy;
    CombatController combatController;
    GameManager gameManager;
    bool BlockingValue = true;

    public Transform[] Positions;
    int positionIndex = 0;

    Vector3 MovePosition;
    Rigidbody m_rigidbody;

    [SerializeField]
    float MoveSpeed = 20;

    public void TakeDamage(int damage)
    {
        //   Debug.Log(this.name + " is taking damage");
        combatController.Hit();
        gameManager.PlayerDamge(damage);
    }

    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        combatController = GetComponent<CombatController>();
        gameManager = FindObjectOfType<GameManager>();

        SwordImage.gameObject.SetActive(BlockingValue);
        ShieldImage.gameObject.SetActive(!BlockingValue);

        m_rigidbody = GetComponent<Rigidbody>();
       // m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;


        MovePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Actions
        if (Input.GetButtonDown("ActionRight"))
        {
            combatController.Action(CombatController.ActionDirection.ActionRight);
        }
        if (Input.GetButtonDown("ActionLeft"))
        {
            combatController.Action(CombatController.ActionDirection.ActionLeft);
        }
        if (Input.GetButtonDown("ActionOverhead"))
        {
            combatController.Action(CombatController.ActionDirection.ActionOverhead);
        }
        if (Input.GetButtonDown("BlockToggle"))
        {
            BlockingValue = !BlockingValue;

            SwordImage.gameObject.SetActive(BlockingValue);
            ShieldImage.gameObject.SetActive(!BlockingValue);
            combatController.ToggleBlocking();
        }

        if ((MovePosition - transform.position).magnitude > .1)
        {
            transform.LookAt(enemy.transform);
            transform.position = Vector3.Lerp(transform.position, MovePosition, Time.deltaTime * MoveSpeed);
        }
        else
        {
            // Moving Around Enemy Code
            if (Input.GetButtonDown("DodgeRight"))
            {
                Debug.Log("Move Right");
                positionIndex += 1;
                if (positionIndex == Positions.Length) { positionIndex = 0; }
                MovePosition = Positions[positionIndex].position;
            }

            if (Input.GetButtonDown("DodgeLeft"))
            {
                Debug.Log("Move Left");
                positionIndex -= 1;
                if (positionIndex == -1) { positionIndex = Positions.Length - 1; }
                MovePosition = Positions[positionIndex].position;
            }
        }

    }

    public void AttackEnemyMessage(CombatController.ActionDirection AttackDirection)
    {
        enemy.AttackedFrom(AttackDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .7f);
    }
}
