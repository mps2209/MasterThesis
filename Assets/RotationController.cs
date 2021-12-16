using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public int branchedOff=0;
    public int branchNumber=0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBranchRotation()
    {
        transform.Rotate(0, 137 * branchNumber, 0);
    }


}
