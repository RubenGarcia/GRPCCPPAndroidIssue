using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GRPC : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void grpc_init();

    // Start is called before the first frame update
    void Start()
    {
        grpc_init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
