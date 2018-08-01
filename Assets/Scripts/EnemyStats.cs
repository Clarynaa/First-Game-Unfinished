using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	public float hp;
	public float maxhp;
	public float atk;
	public float aspd;
	public int def;
	public int level;
	float targetHp;
	float nextAttack;
	public GameObject player;
	GameObject hand;
	public bool isDead = false;
	bool playerIsDead = false;
	int playerdef;
	public NavMeshAgent nav;
	RaycastHit hit;
	float playerSighting;
	public bool isAttacking;
	public System.Collections.Generic.List<GameObject> droptable;
	public Transform[] waypoints;
	int waypointRandom;
	bool hasMoved;
	bool newDestCalled;
	public MeshRenderer renderer;

	// Use this for initialization
	void Start () {
		maxhp = (level * 50 ) + 100;
		hp = maxhp;
		renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();

		
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast (transform.position, transform.forward, out hit, 10.0f))
		{
			if (hit.collider.gameObject.tag == "player")
		   {
				playerSighting = Time.time + 3.0f;

				Debug.DrawLine (transform.position, hit.point, Color.white);

			}
		}
		if (playerSighting > Time.time && isAttacking == false)
		{

			nav.SetDestination(player.transform.position);
		}
		else 
		{
			if (nav.remainingDistance < 0.1)
			{
				nav.Stop();
				nav.ResetPath();
				if(!newDestCalled)
				{
					Debug.Log ("Trying to call New Destination");
					newDestCalled = true;
					StartCoroutine(NewDestination ());

				}
			}

		}
		if (hp < maxhp / 2){
			renderer.material.color = new Color(230, 230, 230, 50);
		}
	}
	
	void calcDmg()
	{
		{
			targetHp = player.GetComponent <PlayerStats>().hp;
			playerdef = player.GetComponent<PlayerStats>().armDef;
			if(targetHp - atk + playerdef > 0)
			{
				if(Random.Range (player.GetComponent<PlayerStats>().eva - 100.0f, player.GetComponent<PlayerStats>().eva) > 0)
				{
					Debug.Log ("You dodged the attack!");
				}
				else
				{
				player.GetComponent<PlayerStats>().hp = targetHp - atk + playerdef;
				player.GetComponent<PlayerStats>().timeHit = Time.time + 15f;
				Debug.Log ("You took " + atk + " damage!");
				}
			}
			else
			{
				if(Random.Range (player.GetComponent<PlayerStats>().eva - 100.0f, player.GetComponent<PlayerStats>().eva) > 0)
				{
					Debug.Log ("You dodged the attack!");
				}
				else
				{
				player.GetComponent<PlayerStats>().hp = 0;
				Debug.Log ("You have been killed.");
				playerIsDead = true;
				}
			}
		}
	}
	
	void OnTriggerStay(Collider col)
	{
		if(col.tag == "player")
		{
			player = col.gameObject;
		}
		if(!isDead && !playerIsDead)
		{
			if(col.tag == "player" && Time.time > nextAttack)
			{
				nextAttack = Time.time + aspd;
				calcDmg ();
				StartCoroutine (AttackMotionStop());


			}
		}
	}

	IEnumerator NewDestination()
	{
		yield return new WaitForSeconds(3);
		waypointRandom = Random.Range(0, waypoints.Length);
		Debug.Log (waypointRandom);
		nav.SetDestination(waypoints[waypointRandom].position);
		newDestCalled = false;

	}

	IEnumerator AttackMotionStop()
	{
		nav.Stop();
		isAttacking = true;
		Debug.Log ("Waiting " + aspd + "Seconds");
		yield return new WaitForSeconds(aspd);
		isAttacking = false;
		nav.Resume();
	}
	
	void toggleDeath()
	{
		isDead = true;
		int dropnum = Random.Range (0, droptable.Count) ;
		Instantiate (droptable[dropnum], this.gameObject.transform.position, this.gameObject.transform.rotation);
		Destroy (this.gameObject);
	}
}
