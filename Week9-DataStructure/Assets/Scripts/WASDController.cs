using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    public Rigidbody rb;
    public float forceAmt = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * forceAmt * Time.deltaTime;
        
        rb.MovePosition(transform.position + movement);
    }
}
