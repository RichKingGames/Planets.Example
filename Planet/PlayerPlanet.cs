using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlanet : Planet
{
	public const string Tag = "Player 1(Clone)";
	private const string Prefix = "Prefabs/Player 1";
	private const float AcceleratedSpeed = 20.0f;
	private const float ScaledSpeed = 10.0f;
	private const float ScaledScale = 3.0f;

	private const int ScaledLevel = 10;
	private const float CamSizeBase = 20.0f;

	private SpeedUpAbility _speedUpAbility;
	private ShieldUpAbility _shieldUpAbility;
	private SizeUpAbility _sizeUpAbility;

	private VariableJoystick _joystick;
	private Text _scoreText;
	public SpriteMask Mask;
	public SpriteRenderer[] Sprites;
	public GameObject _foodDestroyAnim;
	public GameObject FoodExplosion;
	public GameObject _planetDestroyAnim;
	private Vector3 _prevDirection;
	public GameObject ShieldParticle;
	public static bool NewShield;

	private int _previousBaseLevel = 1;
	public override int Level 
	{ 
		get 
		{	
			if (_shieldUpAbility.IsActive)
			{
				return MaxLevel + 1;
			}
			int baseLevel = Math.Min(MaxLevel, (int)(Mass / 100) + 1);
			if(_previousBaseLevel != baseLevel)
			{
				Controller.gameManager.GetComponent<PlanetSoundsController>().LevelUp();
				FloatingTextController.CreateFloatingImage();
				_previousBaseLevel = baseLevel;
			}
			if (_sizeUpAbility.IsActive)
			{
				return Math.Min(MaxLevel + 1, baseLevel + ScaledLevel);
			}
			return baseLevel;
		} 
	}
	public override float Speed
	{
		get
		{
			int level;
			if (_shieldUpAbility.IsActive)
			{
				level = Math.Min(MaxLevel, (int)(Mass / 100) + 1);
			}
			else
			{
				level = Level;
			}
			float baseSpeed = _initSpeed + level - 1;
			if(_speedUpAbility.IsActive)
			{
				baseSpeed += AcceleratedSpeed;
			}
			if(_sizeUpAbility.IsActive)
			{
				baseSpeed += ScaledSpeed;
			}
			return baseSpeed;
		}
	}
	private float _levelUpRatio = 0.0f;
	private int _prevLevel = 1;
	public override float Scale
	{
		get
		{
			int level;
			if(_shieldUpAbility.IsActive || _sizeUpAbility.IsActive)
			{
				level = Math.Min(MaxLevel, (int)(Mass / 100) + 1);
			}
			else 
			{
				level = Level;
			}
			//КОСТЫЛЬ
			if (_levelUpRatio > 0.0f)
			{
				_levelUpRatio = Mathf.Max(0.0f, _levelUpRatio - 0.01f);
			}
			if (_prevLevel != level)
			{
				_levelUpRatio = 1.0f;
				_prevLevel = level;
			}
			float baseScale = 1.0f + (level - 1 - _levelUpRatio) * 0.3f;
			if (_sizeUpAbility.IsActive)
			{
				baseScale += ScaledScale * Mathf.Pow(_sizeUpAbility.GetSizeUpRatio(),4.0f);
			}
			
			return baseScale;
		}
	}


	public float CamSize => CamSizeBase + Scale * 3;
	

	public bool Init(float initSpeed,
		SpeedUpAbility speedUpAbility, ShieldUpAbility shieldUpAbility, SizeUpAbility sizeUpAbility,
		GameController controller)
	{
		_speedUpAbility = speedUpAbility;
		_shieldUpAbility = shieldUpAbility;
		_sizeUpAbility = sizeUpAbility;
		_joystick = controller.CanvasJoystick.GetComponent<VariableJoystick>();
		_scoreText = controller.CanvasScore.GetComponent<Text>();
		Animator = controller.Player.GetComponent<Animator>();
		Animator.Play(controller.AnimationName);
		_speedUpAbility._trail = GetComponentInChildren<TrailRenderer>();
		_speedUpAbility._trail.gameObject.SetActive(false);
		_shieldUpAbility._animator = GameObject.Find("ShieldUpAnimator").GetComponent<Animator>();
		_sizeUpAbility._animator = GameObject.Find("SizeUpAnimator").GetComponent<Animator>();
		
		Initialization(initSpeed, controller, Prefix);
		return true;
	}


	public new void Start()
	{
		if (GameProgress.JsonLibrary.isNewShieldAnim())
		{
			GameObject Orb = Resources.Load<GameObject>("Prefabs/Orb4.4");
			if(Orb!=null)
			{
				Transform foo = ShieldParticle.transform.parent;
				Destroy(ShieldParticle);
				ShieldParticle = GameObject.Instantiate(Orb, foo);
			}
		}
		base.Start();
		FloatingTextController.Initialize();
		FoodExplosion = Resources.Load<GameObject>("Prefabs/CFX4 Hit B (Orange)");
		_planetDestroyAnim = Resources.Load<GameObject>("Prefabs/CFX2_EnemyDeathSkull");
		//CFX4 Hit B (Orange)
		//CFX4 Hit Pow
		//CFX4 Sparks Explosion B
	}   //CFX3_Hit_Misc_D
	public new void Update()
	{
		base.Update();
		_scoreText.text = "Score: " + Mass.ToString();
		_speedUpAbility.TrailTime = Scale;
		_speedUpAbility.TrailWidth = 2.0f + (42.0f / 6.4f * Scale);
	}
	public override void FixedUpdate()
	{
		if (Controller.GetStatus() == GameStatus.GameStart)
		{
			
			Vector3 direction = Vector3.up * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
			if (_joystick.Direction != new Vector2(0f, 0f))
			{
				transform.position += direction * Speed * Time.deltaTime;
				transform.up = direction * Time.deltaTime;
				_prevDirection = direction;
			}
			else
			{
				transform.up = _prevDirection * Time.deltaTime;
			}
			
			
			_speedUpAbility.Tick(Time.deltaTime);
			_shieldUpAbility.Tick(Time.deltaTime);
			_sizeUpAbility.Tick(Time.deltaTime);
		}
	}
	private const int MaxExplosions = 3;
	public static int CurrentExplosions = 0;
	public override void OnTriggerEnter2D(Collider2D col)
	{
		int score = 0;
		if (col.gameObject.tag == ObjectMap.EatTag)
		{
			score = col.gameObject.GetComponent<Enemy>().Mass;
			Mass += score;
			float mathfPow = Mathf.Pow(Scale, 2.0f) + 5.0f;
			FoodExplosion.GetComponent<Transform>().localScale = new Vector3(mathfPow,mathfPow, mathfPow);
			if (CurrentExplosions < MaxExplosions)
			{
				CurrentExplosions++;
				Instantiate(FoodExplosion, col.gameObject.transform.position, new Quaternion());
				
			}
			
			col.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(-500.0f, 500.0f), UnityEngine.Random.Range(-500.0f, 500.0f));
			FloatingTextController.CreateFloatingText("+" + score.ToString());
			Controller.gameManager.GetComponent<PlanetSoundsController>().EatSound();
		}
		else
		{
			
			
			score = (int)col.gameObject.GetComponent<BotPlanet>().Mass;
			int targetLevel = col.gameObject.GetComponent<BotPlanet>().Level;
			int myLevel = Level;
			if (targetLevel < myLevel)
			{
				Controller.gameManager.GetComponent<PlanetSoundsController>().KillPlanetSound();
				Mass += score;
				Controller.PlanetsKilled++;
				float mathfPow = Mathf.Pow(col.gameObject.GetComponent<BotPlanet>().Scale, 2.0f) + 10;
				Vector3  planetDestroyScale= new Vector3(mathfPow, mathfPow, mathfPow);
				_planetDestroyAnim.GetComponent<Transform>().localScale = planetDestroyScale;
				Instantiate(_planetDestroyAnim, col.gameObject.transform.position, new Quaternion());
				
				Controller.DestroyPlayer(col.gameObject);

				FloatingTextController.CreateFloatingTextWhenEatEnemies("+" + score.ToString());
				

			}
		}

		
			
	}

	
}
