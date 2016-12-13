using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreditsMachine : Machine
{
    public List<string> credits = new List<string>();
    int counter = 0;

    protected override void PrepareOutput(Product p)
    {
        if (credits.GetCount() > 0)
        {
            string name = credits[(counter++) % credits.GetCount()];
            p.GetComponentInChildren<Text>().text = name;
        }
    }

}
