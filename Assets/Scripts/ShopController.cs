using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
	private GameObject player;
	private PlayerController pc;

	private GameObject head;
	private GameObject speechBubble;
	private Text speechText;

	private GameObject xboxButton;
	private GameObject prompt;
	private Text promptText;

	private Armor armor;

    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		pc = player.GetComponent<PlayerController>();
		if (pc.stats.armor == null && pc.stats.oldArmor != null)
			armor = pc.stats.oldArmor.downGrade;

		head = transform.Find("Torso").Find("Head").gameObject;
		speechBubble = transform.Find("SpeechBubble").gameObject;
		speechText = GetComponentInChildren<Text>();

		Transform canvas = Camera.main.transform.Find("Canvas");
		xboxButton = canvas.Find("Image").gameObject;
		prompt = canvas.Find("Text").gameObject;
		promptText = prompt.GetComponent<Text>();
    }
	
    void Update()
    {
		float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
		head.transform.localScale = new Vector3(direction, 1f, 1f);

		if(Mathf.Abs(player.transform.position.x - transform.position.x - 2f) < 2f)
		{
			speechBubble.transform.localScale = Vector3.Lerp(speechBubble.transform.localScale, new Vector3(-1f, 1f, 1f), Time.deltaTime * 5f);

			if (armor == null && pc.stats.armor != null)
			{
				speechText.text = $"Nice armor you got there...\nI'll give you {pc.stats.armor.sellPrice} gold for it.";

				xboxButton.transform.localScale = Vector3.Lerp(xboxButton.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				prompt.transform.localScale = Vector3.Lerp(prompt.transform.localScale, new Vector3(0.01f, 0.01f, 1f), Time.deltaTime * 5f);
				promptText.text = "SELL";

				if (Input.GetButtonDown("Fire2"))
				{
					pc.stats.gold += pc.stats.armor.sellPrice;
					armor = pc.stats.armor.downGrade;
					pc.stats.oldArmor = pc.stats.armor;
					pc.PutOnArmor(null);
				}
			}
			else if(armor != null && pc.stats.armor == null && pc.stats.gold >= armor.buyPrice)
			{
				speechText.text = $"Awfully naked you look...\nI can sell you a{(armor.name.StartsWith("I") ? "n" : "")} {armor.name} for {armor.buyPrice} gold.";

				xboxButton.transform.localScale = Vector3.Lerp(xboxButton.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				prompt.transform.localScale = Vector3.Lerp(prompt.transform.localScale, new Vector3(0.01f, 0.01f, 1f), Time.deltaTime * 5f);
				promptText.text = "BUY";

				if (Input.GetButtonDown("Fire2"))
				{
					pc.stats.gold -= armor.buyPrice;
					pc.PutOnArmor(armor);
				}
			}
			else
			{
				speechText.text = "See you again soon...";

				xboxButton.transform.localScale = Vector3.Lerp(xboxButton.transform.localScale, new Vector3(0f, 0f, 1f), Time.deltaTime * 10f);
				prompt.transform.localScale = Vector3.Lerp(prompt.transform.localScale, new Vector3(0f, 0f, 1f), Time.deltaTime * 10f);
			}
		}
		else
		{
			speechBubble.transform.localScale = Vector3.Lerp(speechBubble.transform.localScale, new Vector3(0f, 0f, 1f), Time.deltaTime * 5f);
			xboxButton.transform.localScale = Vector3.Lerp(xboxButton.transform.localScale, new Vector3(0f, 0f, 1f), Time.deltaTime * 10f);
			prompt.transform.localScale = Vector3.Lerp(prompt.transform.localScale, new Vector3(0f, 0f, 1f), Time.deltaTime * 10f);
		}
    }

	void OnDashEnded()
	{
	}
}
