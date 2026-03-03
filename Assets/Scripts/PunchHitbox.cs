using UnityEngine;
using System.Collections.Generic;

public class PunchHitbox : MonoBehaviour
{
    public int damage = 1;

    private readonly HashSet<GameObject> hitThisPunch = new HashSet<GameObject>();
 
    public void BeginPunchDamage()
    {
        hitThisPunch.Clear();

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;
    }

    public void EndPunchDamage()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        hitThisPunch.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("enemy")) return;

        var enemyObj = other.gameObject;
        if (hitThisPunch.Contains(enemyObj)) return;

        hitThisPunch.Add(enemyObj);

        var hp = enemyObj.GetComponent<EnemyHealth>();
        if (hp != null)
            hp.TakeDamage(damage);
    }
}