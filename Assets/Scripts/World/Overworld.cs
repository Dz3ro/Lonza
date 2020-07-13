using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Overworld : MonoBehaviour
{
    private bool[,] _tilesThatCanBeTilled = new bool[1000,1000];
   


    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public bool CanTillhere(int posX, int posy)
    {
        return _tilesThatCanBeTilled[posX, posy];
    }
}
