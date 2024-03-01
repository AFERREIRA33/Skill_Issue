using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

public class Projectile : MonoBehaviour
{
    public float force;
    public float damage = 10;
    private GameObject player;
    private Vector3 mousePos;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (Gamepad.all.Count >= 1)
        {
            Quaternion rotationJoy = player.transform.rotation;
            float angleInRadians = rotationJoy.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -1;
            Vector3 direction = mousePos - transform.position;
            Vector3 rotation = transform.position - mousePos;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ia")
        {
            if (player.GetComponent<Player>().stunProjectile)
            {
                //collider.GetComponent<Enemy>().Stuned();
                Destroy(gameObject);
            }
            if (collider.tag != "ProjectilePlayer" && collider.tag != "Player" && collider.tag != "DetectionArea")
            {
                Destroy(gameObject);
            }

            if (collider.tag == "PlayerTurret" /*|| collider.tag == "EnemyTurret"*/)
            {
                collider.gameObject.GetComponent<Turret>().OnTakeDamage(damage);
                Destroy(gameObject);
            }
            collider.GetComponent<IaController>().OnTakeDamage(damage);
        } if (collider.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
}
