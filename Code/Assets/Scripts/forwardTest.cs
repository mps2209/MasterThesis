using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardTest : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // forward test moves in Z direction(blue arrow)
        //transform.position += transform.forward * Time.deltaTime;
        // Quaternion Test        
       //transform.Rotate(Vector3.forward, 1);
    }
}
