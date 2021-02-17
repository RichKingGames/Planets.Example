using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotPlanet : Planet
{
	public const string Tag = "Bot";
	private const float InitSpeed = 21.0f;
	private GameObject _target;
	private GameObject _food;
	private ObjectMap _objMap;
	public GameObject Mask;
	public GameObject[] Sprites;
	public float SeeDistance = 47f;//max=138 = Math.Max(47f, 47+Controller.Player.Level * _seeDistance)
	private const string Prefix = "Prefabs/Bot";
	public override int Level => Math.Min(MaxLevel, (int)(Mass / 100) + 1);
	public override float Scale => 1.0f + (Level - 1) * 0.3f;
	public override float Speed => _initSpeed + (Level - 1);

	public bool Init(GameController controller)
	{
		Initialization(InitSpeed, controller, Prefix);
		_target = Controller.Player;
		_objMap = GameObject.Find("Spawner2").GetComponent<ObjectMap>();
		return true;
	}

	public void Start()
	{
		base.Start();
		

	}
	public void Update()
	{
		base.Update();
		if (_food == null)
		{
			_food = FindClosestEnemy(transform.position, _objMap); //Food finding
			if (_food == null)
			{
				return;
			}
		}
		SeeDistance = Controller.SeeDistance;
		//targetlvl = GameObject.Find("Player").GetComponent<PlayerController>().level;
	}
	public bool EdgeTransform()
	{
		if(transform.position.x <= 500f && transform.position.y <= 500f && transform.position.x >= -500f && transform.position.y >= -500f)
		//if (transform.position.x <= 500f && transform.position.y <= 500f ||
		//	transform.position.x <= 500f && transform.position.y >= -500f ||
		//	transform.position.x >= -500f && transform.position.y <= 500f ||
		//	transform.position.x >= -500f && transform.position.y >= -500f)
		{
			return true;
		}
		else return false;
	}
	public override void FixedUpdate()
	{
		Vector2 targetPosition = _target.transform.position;
		//Vector3 previousPosition = transform.position;
		if (Controller.GetStatus() == GameStatus.GameStart)
		{
			if (Vector2.Distance(transform.position, targetPosition) < SeeDistance && EdgeTransform())
			{
				int targetLevel = _target.GetComponent<PlayerPlanet>().Level;
				int myLevel = Level;

				if (targetLevel < myLevel)
				{
					_food = null;
					transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
				}
				else if (targetLevel > myLevel)
				{
					_food = null;
					transform.position = Vector2.MoveTowards(transform.position, targetPosition, -Speed * Time.deltaTime);
				}
				else if (targetLevel == myLevel && _food != null)
				{
					transform.position = Vector2.MoveTowards(transform.position, _food.transform.position, Speed * Time.deltaTime);
				}
			}
			else
			{
				if (_food != null)
				{
					transform.position = Vector2.MoveTowards(transform.position, _food.transform.position, Speed * Time.deltaTime);
				}
			}
			//Vector3 direction = transform.position - previousPosition;
			//transform.up = direction * Time.deltaTime / 10.0f;
		}
	}
	private static GameObject FindClosestEnemy(Vector3 pos, ObjectMap objectMap)
	{
		GameObject closest = null;
		Vector3 diff;
		float distance = Mathf.Infinity;
		foreach (GameObject go in objectMap.GetClosestFood(pos))
		{
			diff = go.transform.position - pos;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	public override void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == ObjectMap.EatTag)
		{
			Mass += 2;
			// Destroy(col.gameObject);
			col.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(-500.0f, 500.0f), UnityEngine.Random.Range(-500.0f, 500.0f));
		}
		else if (col.gameObject == Controller.Player)
		{
			int targetLevel = _target.GetComponent<PlayerPlanet>().Level;
			int myLevel = Level;
			if (targetLevel < myLevel)
			{
				Mass += 100;
				// Destroy(col.gameObject);
				//col.gameObject.SetActive(false);
				Controller.SetStatus(GameStatus.GameOver);
				//Controller.GetComponent<GameController>().Gameover = true;
			}
		}
		else
		{
			int score = (int)col.gameObject.GetComponent<BotPlanet>().Mass;
			int targetLevel = col.gameObject.GetComponent<BotPlanet>().Level;
			int myLevel = Level;
			if (targetLevel < myLevel)
			{
				Mass += score;
				Controller.DestroyPlayer(col.gameObject);
			}
		}
		_food = null;
	}
	public void OnDestroy()
	{
		
	}
}