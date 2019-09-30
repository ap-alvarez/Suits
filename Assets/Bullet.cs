using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float lifetime = 3;

    private float living = 0;

    //private int collidesWith = 9;
    // Start is called before the first frame update
    void Start()
    {
        //collidesWith = 1 << collidesWith;
        //collidesWith = ~collidesWith;
    }

    // Update is called once per frame
    void Update()
    {
        living += Time.deltaTime;
        if (living >= lifetime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 9)
            Hit(collision);
    }

    private void Hit(Collision collision)
    {
        Destroy(gameObject);
    }

    public void Fire(float velocity, float damage)
    {
        this.damage = damage;
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * velocity;
    }
}
