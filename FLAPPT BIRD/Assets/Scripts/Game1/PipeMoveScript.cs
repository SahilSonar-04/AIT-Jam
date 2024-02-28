using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float speed = 5;
    public float deadzone = -45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left *2*speed)*Time.deltaTime;
        if(transform.position.x < deadzone)
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
}