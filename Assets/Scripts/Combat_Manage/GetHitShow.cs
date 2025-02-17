using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitShow : MonoBehaviour
{
    public float DesTime = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DesTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
