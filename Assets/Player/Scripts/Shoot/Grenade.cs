using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grenade : MonoBehaviour
{
    public float force;
    private GameObject player;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private GameObject poison;
    private float poisonTimer = 10f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        poison = transform.GetChild(0).gameObject;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Ia")
        {
            force = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            poison.SetActive(true);
            StartCoroutine(PoisonTimer());
        }

    }

    IEnumerator PoisonTimer()
    {
        while (poisonTimer >0) 
        {
            poisonTimer -= Time.deltaTime;
            yield return null;
        }
        if (poisonTimer <= 0)
        {
            poison.SetActive(false);
            Destroy(gameObject);
        }
    }

}
