using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.01f,0f,0f);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.01f,0f,0f);
        }
    }
}
