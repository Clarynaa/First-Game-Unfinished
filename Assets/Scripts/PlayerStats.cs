using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//need to figure out time delay on calculation for anims

public class PlayerStats : MonoBehaviour {
	public float maxhp;
	public float hp;
	public float atk;
	public float xp;
	public float wepDmg;
	public float wepSpd;
	public int def;
	public int lvl = 1;
	public string skillType;
	public float nextSwing;
	public float skillDisplay;
	public float swordSkill;
	public float twoHandSwordSkill;
	public float axeSkill;
	public float twoHandAxeSkill;
	public float shieldSkill;
	public float daggerSkill;
	public float polearmSkill;
	public float maceSkill;
	public float twoHandMaceSkill;
	public float unarmedSkill;
	public float twoHandSwordSkillLevel;
	public float axeSkillLevel;
	public float twoHandAxeSkillLevel;
	public float shieldSkillLevel;
	public float daggerSkillLevel;
	public float polearmSkillLevel;
	public float maceSkillLevel;
	public float twoHandMaceSkillLevel;
	public float unarmedSkillLevel;
	public float nextTwoHandSwordSkillLevel;
	public float nextAxeSkillLevel;
	public float nextTwoHandAxeSkillLevel;
	public float nextShieldSkillLevel;
	public float nextDaggerSkillLevel;
	public float nextPolearmSkillLevel;
	public float nextMaceSkillLevel;
	public float nextTwoHandMaceSkillLevel;
	public float nextUnarmedSkillLevel;
	public int remainingAttributes;
	public int agi;
	public int str;
	int vit;
	Component anim;
	public GameObject enemy;
	public bool attackEnabled = false;
	public GameObject enemyprefab;
	float nextHeal = 0.0f;
	public string currentWeaponType;
	float hitAmount;
	int enemyCount = 10;
	public GUIText areaClear;
	public int armDef;
	public int eva;
	float dmgDealt;
	MouseLook[] mLooks;
	public GUIText hpDisplay;
	public System.Collections.Generic.List<GameObject> inventory;
	public GameObject WepHolder;
	public System.Collections.Generic.List<GameObject> armorInv;
	public System.Collections.Generic.List<GameObject> consumableInv;
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
	float nextLevel;
	bool inventoryOpen = false;
	//public Texture2D emptyHealth;
	//public Texture2D healthBar;
	public int itemcount = 0;
	public Vector2 ScrollPosition;
	float skillGainXp;
	float xpgained;
	Collider enemycollider;
	public float nextSwordSkillLevel;
	public float swordSkillLevel;
	bool isSprinting = false ;
	CharacterMotor motor;
	int stamina;
	public GameObject health;
	public GameObject stam;
	float staminamod;
	float staminareset;
	public float timeHit;
	public Text logBox;

	// Use this for initialization
	void Start () {
	logBox = GetComponentInChildren<Text>();
	motor = GetComponent<CharacterMotor>();
	maxhp = (lvl * 50) + 100;
	hp = maxhp;
	mLooks = this.gameObject.GetComponentsInChildren<MouseLook>();
	nextLevel = ((lvl * lvl) * 50) + 50;
	nextSwordSkillLevel = ((swordSkillLevel * swordSkillLevel) * 50) + 5000;
	}
	
	// Update is called once per frame
	void Update () {
	health.GetComponent<MonoHealthbar>().Health = Mathf.RoundToInt( (GameObject.FindWithTag("player").GetComponent<PlayerStats>().hp / GameObject.FindWithTag("player").GetComponent<PlayerStats>().maxhp) * 100);
	stam.GetComponent<MonoHealthbar>().Health = stamina;
		
	if(staminareset < Time.time)
		{
			staminamod = 1;
		}
		
	if(stamina < 100 && !isSprinting)
		{
			stamina++;
		}
	if(isSprinting)
		{
			stamina--;
		}
	if(GetComponentInChildren<Animation>() != null)
		{
		anim = GetComponentInChildren<Animation>();
		}
	if(anim != null)
		{
			if(anim.animation.name == "swordswing1")
				{
			if(anim.animation["swordswing1"].enabled == true)
				{
					attackEnabled = true;
				}
			if(anim.animation["swordswing1"].enabled == false)
				{
					attackEnabled = false;
				}
			}
		}
	if(isSprinting)
		{
			motor.movement.maxForwardSpeed = 10;
		}
	else
		{
			motor.movement.maxForwardSpeed = 6;
		}
	if(hp > maxhp)
		{
			hp = maxhp;
			Debug.Log ("hp was over max.  Adjusted. " );
		}
	if(xp >= nextLevel)
		{
			lvl++;
			nextLevel = ((lvl * lvl) * 50) + 50;
			remainingAttributes += 3;

		}
	if(swordSkill >= nextSwordSkillLevel)
		{
			swordSkillLevel++;
			nextSwordSkillLevel = ((swordSkillLevel * swordSkillLevel) * 50) + 50;
		}
	if(Input.GetButton("Shift"))
		{
			isSprinting = true;
		}
	else
		{
			isSprinting = false;
		}
		
		
	if(Input.GetKeyUp ("i"))
		{
			if(!inventoryOpen)
			{
				inventoryOpen = true;
				foreach(MouseLook mLook in mLooks)
				{
					mLook.enabled = false;
				}
			}
			else
			{
				inventoryOpen = false;
				foreach(MouseLook mLook in mLooks)
				{
					mLook.enabled = true;
				}
			}

		}
	transform.position = transform.position + Vector3.zero;
	maxhp = (lvl * 50) + 100;
	atk = lvl * 30;
	hpDisplay.guiText.text= ("Hp: " + hp + "     Max Hp: " + maxhp + "    " + (hp/maxhp)*100 + "%");
	if((hp/maxhp) < 0.7  &&  (hp/maxhp) >= 0.3)
		{
			hpDisplay.guiText.font.material.color = Color.yellow;
		}
	else if((hp/maxhp) < 0.3)
		{
			hpDisplay.guiText.font.material.color = Color.red;
		}
	else
		{
			hpDisplay.guiText.font.material.color = Color.white;
		}
	if(timeHit  > Time.time)
		{
				if(hp+(maxhp / 10)<=maxhp && Time.time > nextHeal)
				{
					nextHeal = Time.time + 3;
					hp+= maxhp / 10;
					Debug.Log ("You have regened");
				}
		}
	if(Input.GetKeyUp ("7") && remainingAttributes != 0)
		{
			remainingAttributes --;
			str++;
		}
			if(Input.GetKeyUp ("8") && remainingAttributes != 0)
		{
			remainingAttributes --;
			agi++;
		}
			if(Input.GetKeyUp ("9") && remainingAttributes != 0)
		{
			remainingAttributes --;
			vit++;
		}
		
	hitAmount = (atk * 3) + ((agi * 4) / wepSpd) + ((str * 10) / wepSpd) + wepDmg;
		
	if(Input.GetButtonUp ("Fire1") && Time.time > nextSwing)
		{
			//attackEnabled = true;
			anim = GetComponentInChildren<Animation>();
			
				if(currentWeaponType == "2h sword")
				{
					if(stamina > 30 * staminamod)
					{
						anim.animation.Play ("swordswing2");
						stamina -= Mathf.FloorToInt(30 * staminamod);
						StartCoroutine (swinglock(wepSpd));
						StartCoroutine(wait((nextSwing-Time.time)));
					}
				}
				if(currentWeaponType == "1h sword")
				{
					if(stamina > 20 * staminamod)
					{
					
						anim.animation.Play ("swordswing1");
						stamina -= Mathf.FloorToInt(20 * staminamod);
						StartCoroutine (swinglock(wepSpd));
						StartCoroutine(wait((nextSwing-Time.time)));
					}
				}
		}
	}
	
	IEnumerator swinglock(float secs)
	{
		GetComponent<MouseLook>().enabled = false;
		GetComponent<CharacterMotor>().canControl = false;
		yield return new WaitForSeconds(secs);
		GetComponent<CharacterMotor>().canControl = true;
		GetComponent<MouseLook>().enabled = true;
	}
	
	IEnumerator wait(float sec)
	{
		nextSwing = Time.time + wepSpd;
		staminamod += 0.3f;
		staminareset = Time.time + wepSpd + 1;
		yield return new WaitForSeconds(sec);
	}
	
	float calcDmg(float attack, float defense, GameObject mob)
	{
		float damageDealt;
		damageDealt = attack - defense;
		nextHeal = Time.time + 3;
		if(mob.GetComponent<EnemyStats>().hp - damageDealt > 0)
		{
			mob.GetComponent <EnemyStats>().hp -= damageDealt;
			skillGainXp = (damageDealt / mob.GetComponent <EnemyStats>().maxhp) * ((mob.GetComponent<EnemyStats>().level / 2) * (mob.GetComponent<EnemyStats>().level / 2 )+ 10);
			if(currentWeaponType == "1h sword")
			{
				swordSkill += skillGainXp;
			}
			else if(currentWeaponType == "2h sword")
			{
				twoHandSwordSkill += skillGainXp;
			}
		}
		else
		{
			xpgained = (mob.GetComponent<EnemyStats>().level / 2) * (mob.GetComponent<EnemyStats>().level / 2 )+ 10;
			skillGainXp = (mob.GetComponent<EnemyStats>().hp / mob.GetComponent<EnemyStats>().maxhp) * xpgained;
			mob.SendMessage ("toggleDeath");
			logBox.text = ("!!!!!!YOU HAVE KILLED THE ENEMY!!!!!!");
			xp += xpgained;
			if(currentWeaponType == "1h sword")
			{
				swordSkill += skillGainXp;
			}
			else if(currentWeaponType == "2h sword")
			{
				twoHandSwordSkill += skillGainXp;
			}
			enemyCount--;
			if(enemyCount == 0)
			{
				areaClear.guiText.enabled = true;
			}
		}
		xpgained = 0;
		skillGainXp = 0;
		logBox.text = ("You dealt " + damageDealt.ToString() + " damage");
		return damageDealt;

	}
	
	
//	void OnTriggerExit(Collider col)
//	{
//		if(col.tag == "enemy")
//		{
//			attackEnabled = false;
//		}
//	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "enemy")
		{
			attackEnabled = true;
		}
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
		if(col.tag == "consumable")
		{
			consumableInv.Add (col.gameObject);
			col.gameObject.SetActive (false);
		}
	}
	
	void OnTriggerStay(Collider col)
	{
//		if(col.tag == "enemy")
//		{
//		enemy = col.gameObject;
//		}
		if(col.tag == "armorEquipped")
		{
			armDef = col.GetComponent<Armor>().def;
			eva = col.GetComponent<Armor>().eva;
		}
		if(col.tag == "weaponEquipped")
		{
			wepDmg = col.GetComponent<Item>().dmg + Mathf.FloorToInt(skillDisplay);
			wepSpd = col.GetComponent<Item>().aspd;
			if(col.GetComponent<Item>().currentWepType == "1h sword")
			{
				currentWeaponType = "1h sword";
				skillType = "swordSkill";
				skillDisplay = swordSkillLevel;

			}
			else if(col.GetComponent<Item>().currentWepType == "2h sword")
			{
				currentWeaponType = "2h sword";
				skillType = "twoHandSwordSkill";
				skillDisplay = twoHandSwordSkill;
			}
			else if(col.GetComponent<Item>().currentWepType == "axe")
			{
				currentWeaponType = "1h axe";
				skillType = "axeSkill";
				skillDisplay = axeSkill;
			}
			else
			{
				currentWeaponType = "unarmed";
				skillType = "unarmed";
			}
		}
	}
	
	
	int skillLevel(float skill)
	{
		int skillLvl = Mathf.FloorToInt (skill);
		return skillLvl;
	}
	
	float skillXp(float skill)
	{
		float skillExperience = skill - Mathf.Floor(skill);
		return skillExperience;
	}	
	
	void ItemUse(GameObject item)
	{
		Debug.Log ("You have used : " + item.name);
		if(item.GetComponent<Consumable>().itemName == "potion")
		{
			hp += 30;
			consumableInv.Remove(item);
		}
	}
	
	void enemycol(Collider col)
	{
		enemycollider = col;
		Debug.Log ("enemycol called");
	}
	
	void dmg()
	{
		float dmgdone = calcDmg(hitAmount, enemycollider.gameObject.GetComponent<EnemyStats>().def, enemycollider.gameObject);
		Debug.Log ("You dealt " + dmgdone + " dmg");
	}
	
	GUIContent[] iteminfo;
	
	void OnGUI()
	{
		if(inventoryOpen)
		{
			GUILayout.BeginArea(new Rect(700, 100, 150, 200));
			GUILayout.Box("Character");
			GUILayout.Label("Level: " + lvl);
			GUILayout.Label("Sword Skill: " + swordSkillLevel);
			GUILayout.Label ("2h Sword Skill: " + twoHandSwordSkill);
			GUILayout.Label ("Agility: " + agi);
			GUILayout.Label ("Strength: " + str);
			GUILayout.EndArea();
			int tempinv = 0;
			GUILayout.BeginArea (new Rect(40, 40, 500, 500));
			GUILayout.Box ("Inventory");
			GUILayout.BeginVertical();
			ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, GUILayout.Width (500), GUILayout.Height (75));
			GUILayout.BeginHorizontal();

			foreach(GameObject items in inventory)
			{
				if(GUILayout.Button (  new GUIContent("Weapon",  items.GetComponent<Item>().itemname + "\n" + items.GetComponent<Item>().currentWepType + "\n" + items.GetComponent<Item>().dmg + " dmg\n" + items.GetComponent<Item>().aspd + " seconds per attack"), GUILayout.Width (50), GUILayout.Height (50)))
				{
					Debug.Log ("Equipping " + items.name);
					if(hasCloned)
					{
						Destroy (clone1);
					}
					toUpdate = true;
					hasCloned = true;
					if(toUpdate)
					{
						clone1 = Instantiate(items, WepHolder.transform.position, WepHolder.transform.rotation) as GameObject;
					    clone1.transform.parent = GameObject.Find ("Main Camera").transform;
					    clone1.SetActive (true);
						Debug.Log ("You are now using: " + items.name);
					    clone1.tag = "weaponEquipped";
						toUpdate = false;
					}
					
				}
			}
			foreach(GameObject arm in armorInv)
			{
				if(GUILayout.Button (  new GUIContent("Armor",  arm.GetComponent<Armor>().itemname + "\n" + arm.GetComponent<Armor>().currentArmorType + "\n" + arm.GetComponent<Armor>().def + " def\n" + arm.GetComponent<Armor>().eva + " Evasion"), GUILayout.Width (50), GUILayout.Height (50)))
				{
					Debug.Log ("Equipping " + arm.name);
					if(armorCloned)
					{
						Destroy (clone1);
					}
					updateArmor = true;
					armorCloned = true;
					if(updateArmor)
					{
						clone1 = Instantiate(arm, armorHolder.transform.position,  armorHolder.transform.rotation) as GameObject;
					    clone1.transform.parent = GameObject.Find ("playerGraphics").transform;
					    clone1.SetActive (true);
						Debug.Log ("You are now using: " + arm.name);
					    clone1.tag = "armorEquipped";
						updateArmor = false;
					}
					
				}
			}
			foreach(GameObject consumable in consumableInv)
			{
				if(GUILayout.Button (  new GUIContent("Item",  consumable.GetComponent<Consumable>().name + "\n" + consumable.GetComponent<Consumable>().Description + "\n" + consumable.GetComponent<Consumable>().Description2), GUILayout.Width (50), GUILayout.Height (50)))
				{
				ItemUse(consumable);
					
				}
			}

			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
			

		if(GUI.tooltip != null)
			{
				GUILayout.BeginVertical();
				GUILayout.BeginHorizontal();
				GUILayout.Box (new GUIContent(GUI.tooltip), GUILayout.Height(67));
				GUI.tooltip = null;
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();
			}
			
		GUILayout.EndArea ();
		}
		
		
		
	//GUI.DrawTexture(new Rect(Screen.width/4, 10, 100, 15), emptyHealth, ScaleMode.StretchToFill, true, 10.0f);
	//GUI.DrawTexture (new Rect(Screen.width/4, 10, 100*(hp/maxhp),15), healthBar, ScaleMode.StretchToFill, true, 10.0f);
	}
}
