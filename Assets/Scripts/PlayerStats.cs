using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int Currency;
    public int startingCurrency = 500;
    public static int Lives;

    void Start()
    {
        Currency = startingCurrency;
        Lives = 100;
    }

}
