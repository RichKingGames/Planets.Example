using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Planet : MonoBehaviour
{
	public const int MaxLevel = 20;
	public Vector3 Direction;
	public GameController Controller;
	public string Name;

	public float Mass { get; internal set; }
	public abstract int Level { get; }
	public abstract float Scale { get; }
	public abstract float Speed { get; }

	internal float _initSpeed;
	
	private string _prefix;
	private GameObject _gameObject;
	public Animator Animator;
	
	public bool Initialization(float initSpeed, GameController controller, string prefix)
	{
		Mass = 0.0f;
		Direction = new Vector3(0.0f, 0.0f, 0.0f);
		_initSpeed = initSpeed;
		Controller = controller;
		_prefix = prefix;
		return true;
	}
	public void Start()
	{
	}
	public void Update()
	{
		transform.localScale = new Vector3(Scale, Scale, 1);
	}
	public void Instantiate(Vector3 planetPosition,string prefix)
	{
		_gameObject = Resources.Load<GameObject>(prefix); 
		Instantiate(_gameObject, planetPosition, new Quaternion());
	}
	public abstract void FixedUpdate();
	public abstract void OnTriggerEnter2D(Collider2D col);
	
}