using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject explosion;
    private float damage = 20;

    private Rigidbody rb;
    private int collidesWith = 9;
    // Start is called before the first frame update
    void Start()
    {
        collidesWith = 1 << collidesWith;
        collidesWith = ~collidesWith;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, rb.velocity);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rb.velocity.magnitude * Time.fixedDeltaTime, collidesWith))
        {
            Hit(hit);
        }
    }


    /*private void Hit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        ContactPoint contact = collision.GetContact(0);
        GameObject newExp = Instantiate(explosion, contact.point, Quaternion.LookRotation(contact.normal, Vector3.up)) as GameObject;
        DamagableBehaviour damagableBehaviour = collision.gameObject.GetComponent<DamagableBehaviour>();
        if (damagableBehaviour != null)
            damagableBehaviour.damagable.TakeDamage(damage, DamageType.physical);
        Destroy(gameObject);

    }*/

    private void Hit(RaycastHit hit)
    {
        Debug.Log(hit.collider.gameObject.name);
        GameObject newExp = Instantiate(explosion, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up)) as GameObject;
        DamagableBehaviour damagableBehaviour = hit.collider.gameObject.GetComponent<DamagableBehaviour>();
        if (damagableBehaviour != null)
            damagableBehaviour.damagable.TakeDamage(damage, DamageType.physical);
        Destroy(gameObject);

    }

    public void Fire(float velocity, float damage)
    {
        this.damage = damage;
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * velocity;
    }
}
