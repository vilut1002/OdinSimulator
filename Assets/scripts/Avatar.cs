using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public enum Avatars
    {


    }

    public double[] probability;
    
    public Avatar()
    {
        probability = new double[10];
    }

}
