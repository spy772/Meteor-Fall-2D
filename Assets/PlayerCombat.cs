using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerInputs playerInputs;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 100;

    public LayerMask enemyLayers;
    [SerializeField] GameObject fireball;

    private InputAction attack;
    private InputAction upwardsAttack;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        attack = playerInputs.Player.Attack;
        upwardsAttack = playerInputs.Player.UpwardsShot;
        attack.Enable();
        upwardsAttack.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
        upwardsAttack.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (upwardsAttack.IsPressed() && !animator.GetBool("isAttacking"))
        {
            UnityEngine.Debug.Log("UpwardsAttack");
            animator.SetBool("isAttacking", true);
            animator.SetTrigger("UpwardsAttack");
            UpwardsAttack();
        }

        if (attack.IsPressed() && !animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", true);
            animator.SetTrigger("MeleeAttack");
            Attack();
        }

        if (!attack.IsPressed())
        {
            animator.ResetTrigger("MeleeAttack");
            animator.SetBool("isAttacking", false);
        }

        if (!upwardsAttack.IsPressed())
        {
            animator.SetBool("isAttacking", false);
            animator.ResetTrigger("UpwardsAttack");
        }
    }


    public void UpwardsAttack()
    {
        UnityEngine.Debug.Log("InFunction");
        Instantiate(fireball, transform.position, Quaternion.identity);
    }

    public void Attack()
    {
        UnityEngine.Debug.Log("InAttack");
        // Play an attack animation
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Meteor>().TakeDamage(attackDamage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
