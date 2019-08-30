using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusStatButton : MonoBehaviour
{
    public int stat;
    public Text label;
    public Text greed;
    public int sp = 5;
    public Text spLabel;
    // Start is called before the first frame update
    void Start()
    {
        label.text = stat.ToString();
    }

    public void Down()
    {
        if (stat > 0 && sp > 0)
        {
            stat -= 1;
            sp -= 1;
            spLabel.text = "Sacrifice Points (SP): " + sp.ToString();
            greed.enabled = false;            
        }
    }

    // Update is called once per frame
    void Update()
    {
        label.text = stat.ToString();
    }
}
