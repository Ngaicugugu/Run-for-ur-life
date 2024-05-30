using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;

    public void AttackEnemy()
    {
        // Sử dụng OverlapCircleAll để tìm các quái vật trong phạm vi
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
       
        // Kiểm tra nếu không có quái vật nào trong tầm đánh
        if (enemies.Length == 0)
        {
            return;
        }

        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject);
                DOTween.Kill(enemy.transform);
                ScoreManager.Instance.AddScore(5);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
