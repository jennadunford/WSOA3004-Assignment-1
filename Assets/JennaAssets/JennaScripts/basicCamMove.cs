using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCamMove : MonoBehaviour
{
    Vector3 Vec;

    Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vec = transform.localPosition;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 1;
        Vec.y += Input.GetAxis("Vertical") * Time.deltaTime * 1;
        transform.localPosition = Vec;
    }
}
