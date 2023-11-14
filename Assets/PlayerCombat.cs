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

    private InputAction attack;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        attack = playerInputs.Player.Attack;
        attack.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.IsPressed())
        {
            animator.SetBool("isAttacking", true);
            animator.SetTrigger("MeleeAttack");
        }

        if (!attack.IsPressed())
        {
            animator.ResetTrigger("MeleeAttack");
            animator.SetBool("isAttacking", false);
        }
    }

    public void Attack()
    {
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
