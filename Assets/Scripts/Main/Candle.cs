using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Candle : Item
{
    // POLYMORPHISM
    public override string GetItemId()
    {
        return "candle-red";
    }
    
}
