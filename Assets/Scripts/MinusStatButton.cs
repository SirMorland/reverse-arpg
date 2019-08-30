using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusStatButton : MonoBehaviour
{
    public int stat;
    public Text label;
    public Text greed;
    public int sp;
    public Text spLabel;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        sp = player.GetComponent<Stats>().sp;
        switch (label.name)
        {
            case "str":
                stat = player.GetComponent<Stats>().str;
                break;
            case "dex":
                stat = player.GetComponent<Stats>().dex;
                break;
            case "vit":
                stat = player.GetComponent<Stats>().vit;
                break;
            default:
                break;
        }
             
        label.text = stat.ToString();
    }

    public void Down()
    {
        
        if (stat > 0 && sp > 0)
        {
            stat -= 1;
            //player.GetComponent<Stats>().sp -= 1;
            player.SendMessage("SpDecrease");
            spLabel.text = "Sacrifice Points (SP): " + sp.ToString();
            greed.enabled = false;            
        }
    }

    // Update is called once per frame
    void Update()
    {
        sp = player.GetComponent<Stats>().sp;
        label.text = stat.ToString();
    }
}
