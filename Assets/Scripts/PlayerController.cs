using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public SwordAttack swordAttack;
    public CarController carController;  // Arabayı kontrol eden script
    public float interactionDistance = 1.5f;  // Arabaya ne kadar yaklaşırsa etkileşime geçilecek mesafe

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Animate();

        // Karakter arabaya yakın mı?
        if (Vector3.Distance(transform.position, carController.transform.position) < interactionDistance)
        {
            // "E" tuşuna basıldı mı?
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // Karakteri gizle
                gameObject.SetActive(false);

                // Arabayı X=6'ya hareket ettir
                carController.StartMovingToPosition(6f);
            }
        }
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            // Hareket yönüne göre sprite ve saldırı yönünü güncelle
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                swordAttack.attackDirection = SwordAttack.AttackDirection.left;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                swordAttack.attackDirection = SwordAttack.AttackDirection.right;
            }
            else if (movementInput.y > 0)
            {
                swordAttack.attackDirection = SwordAttack.AttackDirection.up;
            }
            else if (movementInput.y < 0)
            {
                swordAttack.attackDirection = SwordAttack.AttackDirection.down;
            }

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        swordAttack.Attack(); // Saldırı yap
        animator.SetTrigger("AttackTrigger"); // Animasyonu tetikle
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Animate()
    {
        if (movementInput != Vector2.zero)
        {
            animator.SetFloat("AnimMoveX", movementInput.x);
            animator.SetFloat("AnimMoveY", movementInput.y);
            animator.SetFloat("AnimMoveMagnitude", movementInput.magnitude);
        }
        else
        {
            animator.SetFloat("AnimMoveMagnitude", 0);
        }
    }
}
