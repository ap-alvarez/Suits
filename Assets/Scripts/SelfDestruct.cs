using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime = 1;

    private float living = 0;


    // Update is called once per frame
    void Update()
    {
        living += Time.deltaTime;
        if (living > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
