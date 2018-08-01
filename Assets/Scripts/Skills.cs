using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {
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
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
}
