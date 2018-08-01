using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour {
	public GUIText tooltips;
	public string currentArmorType;
	public int def;
	public int eva;
	public string itemname;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider col)
	{
		if(col.tag == "player")
		{
			tooltips.text = "Armor type: " + currentArmorType + "\n Armor def: " + def.ToString() + "\n Evasion chance: " + eva;
		}
		
	}
}
