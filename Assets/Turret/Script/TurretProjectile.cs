using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    public float force = 10f;
    [HideInInspector]public Transform target;
    private Rigidbody2D rb;
    private float damage = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = target.position - transform.position;
        Vector3 rotation = transform.position - target.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<Player>().OnTakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ia")
        {
            collision.gameObject.GetComponent<IaController>().OnTakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

}
