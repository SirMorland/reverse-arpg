using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObjects/Armor", order = 1)]
public class Armor : ScriptableObject
{
	public new string name;
	public float armorValue;

	public int buyPrice;
	public int sellPrice;

	public Sprite sprite;

	public Armor downGrade;
}
