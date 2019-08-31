using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusStatButton : MonoBehaviour
{
    public GameObject pointWindow;
    public Text label;
    public Text greed;
    public Text spLabel;

    private Animator animator;
    private Button button;
    private Stats stats;

    void Start()
    {
        animator = pointWindow.GetComponent<Animator>();

        button = GetComponent<Button>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        int stat;
        switch (label.name)
        {
            case "str":
                stat = stats.str;
                break;
            case "dex":
                stat = stats.dex;
                break;
            case "vit":
                stat = stats.vit;
                break;
            default:
                stat = 0;
                break;
        }

        label.text = stat.ToString();
        spLabel.text = $"Points to be sacrificed: {stats.sp}";
    }

    void Update()
    {
        if(stats.sp <= 0)
            button.interactable = false;
    }

    public void Down()
    {
        greed.enabled = false;

        stats.sp--;
        spLabel.text = $"Points to be sacrificed: {stats.sp}";

        switch (label.name)
        {
            case "str":
                stats.str--;
                if(stats.str <= 0) button.interactable = false;
                label.text = stats.str.ToString();
                break;
            case "dex":
                stats.dex--;
                if(stats.dex <= 0) button.interactable = false;
                label.text = stats.dex.ToString();
                break;
            case "vit":
                stats.vit--;
                if(stats.vit <= 0) button.interactable = false;
                label.text = stats.vit.ToString();
                break;
            default:
                break;
        }

        if(stats.sp <= 0)
        {
            Time.timeScale = 1f;
            animator.SetTrigger("close");
        }
    }
}
