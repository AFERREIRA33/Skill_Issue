using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float Hp = 20;
    [HideInInspector] public Transform targetTransform;
    public GameObject projectile;
    [HideInInspector]public bool isInArea;
    private bool canFire = true;
    void Update()
    {
        if (isInArea && canFire)
        {

            canFire = false;
            Fire();
        }
    }

    public void OnTakeDamage(float damage)
    {
        Hp -= damage;

        if (Hp <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        projectile.GetComponent<TurretProjectile>().target = targetTransform;
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
    }


}
