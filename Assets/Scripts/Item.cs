using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public string itemname;
	public int dmg;
	public float aspd;
	public string currentWepType;
	public GameObject player;
	PlayerStats playerstats;
	int previousdmg;
	float nextSkill;

	// Use this for initialization
	void Start () 
	{
			nextSkill = Time.time;
			playerstats = GameObject.FindWithTag("player").GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("1") && nextSkill < Time.time)
		{
			nextSkill += Time.time + aspd + 0.3f;
			if(currentWepType == "1h sword" && playerstats.swordSkillLevel >= 1)
			{
				animation.Play ("swordswing1");
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.3f);
				StartCoroutine(wepWait (aspd));
			}
			if(currentWepType == "2h sword" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.2f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "axe" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.4f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "2h axe" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.3f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "mace" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.35f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "2h mace" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.25f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "dagger" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 1.0f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "polearm" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.4f);
				StartCoroutine(wepWait(aspd));
			}
			if(currentWepType == "2h sword" && playerstats.twoHandSwordSkillLevel >= 1)
			{
				//animation here
				previousdmg = dmg;
				dmg += Mathf.FloorToInt(dmg * 0.2f);
				StartCoroutine(wepWait(aspd));
			}
		}
	}
	
	IEnumerator wepWait(float secs)
	{
		yield return new WaitForSeconds(secs);
		dmg = previousdmg;
	}
	
	void OnTriggerEnter(Collider col)
	{

		if(playerstats.attackEnabled)
		{
				if(col.tag == "enemy")
				{
				Debug.Log ("calling functions");
		playerstats.SendMessage("enemycol", col);
		playerstats.SendMessage ("dmg");
				}
		}
//		if(playerstats.attackEnabled)
//		{
//			if(col.tag == "enemy")
//			{
//				if(!col.GetComponent<EnemyStats>().isDead && playerstats.currentWeaponType != "unarmed")
//				{
//					float hitAmount = (playerstats.atk * 3) + ((playerstats.agi * 4) / playerstats.wepSpd) + ((playerstats.str * 10) / playerstats.wepSpd) + playerstats.wepDmg;
//					player.SendMessage("calcDmg", hitAmount, col.GetComponent<EnemyStats>().def, col.gameObject);
//				}
//			}
//		}
	}
	
//	float calcDmg(float attack, float defense, GameObject mob)
//	{
//		float damageDealt;
//		damageDealt = attack - defense;
//		playerstats.nextHeal = Time.time + 3;
//		if(mob.GetComponent<EnemyStats>().hp - damageDealt > 0)
//		{
//			mob.GetComponent <EnemyStats>().hp -= damageDealt;
//			playerstats.skillGainXp = (damageDealt / mob.GetComponent <EnemyStats>().maxhp) * ((mob.GetComponent<EnemyStats>().level / 2) * (mob.GetComponent<EnemyStats>().level / 2 )+ 10);
//			if(playerstats.currentWeaponType == "1h sword")
//			{
//				swordSkill += skillGainXp;
//			}
//			else if(playerstats.currentWeaponType == "2h sword")
//			{
//				twoHandSwordSkill += skillGainXp;
//			}
//		}
//		else
//		{
//			playerstats.xpgained = (enemy.GetComponent<EnemyStats>().level / 2) * (enemy.GetComponent<EnemyStats>().level / 2 )+ 10;
//			skillGainXp = (enemy.GetComponent<EnemyStats>().hp / enemy.GetComponent<EnemyStats>().maxhp) * xpgained;
//			enemy.SendMessage ("toggleDeath");
//			enemy = null;
//			Debug.Log ("!!!!!!YOU HAVE KILLED THE ENEMY!!!!!!");
//			xp += xpgained;
//			if(currentWeaponType == "1h sword")
//			{
//				swordSkill += skillGainXp;
//			}
//			else if(currentWeaponType == "2h sword")
//			{
//				twoHandSwordSkill += skillGainXp;
//			}
//			enemyCount--;
//			if(enemyCount == 0)
//			{
//				areaClear.guiText.enabled = true;
//			}
//		}
//		xpgained = 0;
//		skillGainXp = 0;
//		return damageDealt;
//
//	}
}