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
        var hitObj = other.gameObject;

        if (hitThisPunch.Contains(hitObj)) return;
        hitThisPunch.Add(hitObj);

        if (other.CompareTag("enemy"))
        {
            var hp = hitObj.GetComponent<EnemyHealth>();
            if (hp != null) hp.TakeDamage(damage);
            return;
        }


        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            var myRoot = transform.root.gameObject;


            if (hitObj == myRoot) return;

            var stamina = hitObj.GetComponent<PlayerStamina>();
            if (stamina != null) stamina.ReceiveDamageStamina();


        }
    }
}