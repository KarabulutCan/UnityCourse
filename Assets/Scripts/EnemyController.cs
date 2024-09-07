using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public float maxHealth = 100f;
    private float currentHealth;
    public EnemyManager enemyManager;  // EnemyManager referansı

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float Health
    {
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return currentHealth;
        }
    }

    public void Defeated()
    {
        animator.SetTrigger("DefeatedTrigger");
        RemoveEnemy();
    }

    public void TakeDamage()
    {
        animator.SetTrigger("TakeDamageTrigger");
    }

    public void RemoveEnemy()
    {
        StartCoroutine(RemoveEnemyCoroutine());
    }

    private IEnumerator RemoveEnemyCoroutine()
    {
        animator.SetTrigger("DefeatedTrigger");
        yield return new WaitForSeconds(2f);  // 2 saniye animasyonu oynat
        enemyManager.EnemyDefeated();  // EnemyManager'a bildir
        Destroy(gameObject);  // Düşmanı yok et
    }
}
