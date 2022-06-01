using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInFrontofUser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetInFront();
    }

    private void SetInFront()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 100;
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        transform.LookAt(Camera.main.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
