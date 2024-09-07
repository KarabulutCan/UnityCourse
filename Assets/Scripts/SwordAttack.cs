using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public enum AttackDirection
    {
        left, right, up, down
    }
    public float damage = 10f;
    public AttackDirection attackDirection;
    Collider2D swordCollider;
    Vector2 attackOffset;
    public float attackDuration = 0.5f; // How long the collider stays active

    public void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
        swordCollider.enabled = false; // Make sure the collider is disabled at the start
    }

    public void Attack()
    {
        swordCollider.enabled = true; // Enable the collider when the attack starts

        switch (attackDirection)
        {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
        }

        // Disable the collider after attackDuration
        Invoke(nameof(StopAttack), attackDuration);
    }

    public void AttackRight()
    {
        swordCollider.offset = new Vector2(0.1f, 0); // Move collider to the right
    }

    public void AttackLeft()
    {
        swordCollider.offset = new Vector2(-0.12f, -0.1f); // Move collider to the left
    }

    public void AttackUp()
    {
        swordCollider.offset = new Vector2(0, 0); // Move collider upward
    }

    public void AttackDown()
    {
        swordCollider.offset = new Vector2(0, -0.1f); // Move collider downward
    }

    public void StopAttack()
    {
        swordCollider.enabled = false; // Disable the collider after the attack
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            print("Dusmana temas");
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Health -= damage;
                enemy.TakeDamage();
            }
        }
    }
}
