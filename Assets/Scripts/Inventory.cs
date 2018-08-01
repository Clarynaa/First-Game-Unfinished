using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public System.Collections.Generic.List<GameObject> inventory;
	int x = 0 - 1;
	int y = 0 - 1;
	GameObject Weapon;
	public GameObject WepHolder;
	public System.Collections.Generic.List<GameObject> armorInv;
	GameObject Armor;
	public GameObject armorHolder;
	GameObject yourWeapon;
	GameObject clone1;
	GameObject clone2;
	bool hasCloned = false;
	bool toUpdate = false;
	bool updateArmor = false;
	bool armorCloned = false;
	public GUIText itemTooltip;
	public GUIText armorTooltip;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	Weapon = GameObject.FindWithTag("weaponEquipped");
	Armor = GameObject.FindWithTag ("armorEquipped");
	if(Input.GetButton("Shift"))
		{
			if(Input.GetAxis ("Mouse ScrollWheel") < 0 &&  y > 0)
			{
				y--;
				if(armorCloned)
				{
					Destroy (clone2);
				}
				updateArmor = true;
				hasCloned = true;
				
			}
			if(Input.GetAxis ("Mouse ScrollWheel") > 0 && y + 1 < armorInv.Count)
			{
				armorTooltip.guiText.enabled = true;
				y++;
				if(armorCloned)
				{
					Destroy (clone2);
				}
				updateArmor = true;
				armorCloned = true;
			}
			if(updateArmor)
			{
				if(Armor != null)
				{
					Armor.SetActive(false);
					Armor.tag = "armor";
				}
				clone2 = Instantiate (armorInv[y], armorHolder.transform.position, armorHolder.transform.rotation) as GameObject;
				clone2.transform.parent = GameObject.Find ("playerGraphics").transform;
				clone2.SetActive(true);
				Debug.Log("You are now using: " + armorInv[y].name);
				clone2.tag = "armorEquipped";
				updateArmor = false;
			}
		}
		else
		{
		if(Input.GetAxis("Mouse ScrollWheel") < 0 && x > 0) //back
			{
				//Weapon.SetActive(false);
				//Weapon.tag = "weapon";
				x--;
				if(hasCloned)
				{
					Destroy (clone1);
				}
				toUpdate = true;
				//clone1 = Instantiate(inventory[x], WepHolder.transform.position, WepHolder.transform.rotation) as GameObject;
				//clone1.transform.parent = GameObject.Find ("Main Camera").transform;
				hasCloned = true;
				//clone1.SetActive (true);
				//Debug.Log ("You are now using: " + inventory[x].name);
				//clone1.tag = "weaponEquipped";
			}
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && x + 1 < inventory.Count)  //forward
			{
				itemTooltip.guiText.enabled = true;
				//Weapon.SetActive(false);
				//Weapon.tag = "weapon";
				x++;
				if(hasCloned)
				{
					Destroy (clone1);
				}
				toUpdate = true;
				//clone1 = Instantiate(inventory[x], WepHolder.transform.position, WepHolder.transform.rotation) as GameObject;
				//clone1.transform.parent = GameObject.Find ("Main Camera").transform;
				hasCloned = true;
				//clone1.SetActive (true);
				//Debug.Log ("You are now using: " + inventory[x].name);
				//clone1.tag = "weaponEquipped";
			}
			if (toUpdate)
			{
				if(Weapon != null)
				{
			    Weapon.SetActive(false);
			    Weapon.tag = "weapon";
				}
			 
			    clone1 = Instantiate(inventory[x], WepHolder.transform.position, WepHolder.transform.rotation) as GameObject;
			    clone1.transform.parent = GameObject.Find ("Main Camera").transform;
			    clone1.SetActive (true);
				Debug.Log ("You are now using: " + inventory[x].name);
			    clone1.tag = "weaponEquipped";
				toUpdate = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "groundwep")
		{
			col.tag = "weapon";
			inventory.Add (col.gameObject);
			col.gameObject.SetActive(false);
		}
		if(col.tag == "groundarmor")
		{
			col.tag = "armor";
			armorInv.Add (col.gameObject);
			col.gameObject.SetActive (false);
		}
	}
}
