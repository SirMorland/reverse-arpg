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

	public Text lvl;
	public Text dmg;
	public Text atkspd;
	public Text hp;
	public Text armor;

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

		UpdateStatDisplay();
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

		UpdateStatDisplay();

		if (stats.sp <= 0)
        {
            Time.timeScale = 1f;
            animator.SetTrigger("close");
			Destroy(pointWindow, 1f);
		}
    }

	private void UpdateStatDisplay()
	{
		lvl.text = $"{stats.lvl}/30";
		dmg.text = stats.Damage.ToString();
		atkspd.text = stats.AttackSpeed.ToString();
		hp.text = $"{stats.currentHp}/{stats.MaxHp.ToString()}";
		armor.text = stats.armor.armorValue.ToString();
	}
}
