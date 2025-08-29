using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePoint : MonoBehaviour
{    
    public float min_x;
    public float max_x;
    public float min_y;
    public float max_y;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(min_x, max_x);
        float y = Random.Range(min_y, max_y);
        if (Random.Range(-1, 1) < 0) x *= -1;
        if (Random.Range(-1, 1) < 0) y *= -1;

        gameObject.GetComponent<Transform>().position = new Vector3(x, y);
    }
}
