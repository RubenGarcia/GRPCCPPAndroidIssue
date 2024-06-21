using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class test : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void test_c();

    // Start is called before the first frame update
    void Start()
    {
        test_c();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
