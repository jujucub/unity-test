﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static private Player _player;

    static public Player GetPlayer()
    {
        return _player;
    }

    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private float _rotateSpeed = 0.5f;

    [SerializeField]
    private Camera _camera;
    public Camera Camera
    {
        get { return _camera; }
    }

    private Vector3 _accumrateVec = Vector3.zero;
    private float _horizontalAngle = 0;
    private float _verticalAngle = 0;
    private Vector3 _prevPos = Vector3.zero;

    [SerializeField]
    private Transform _modelTrans;

    [SerializeField]
    private float _speed = 0.5f;

    private IState _state = null;

    public Vector3 Forward
    {
        get { return _modelTrans.forward; }
    }
    public Vector3 Right
    {
        get { return _modelTrans.right; }
    }

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    private void Awake()
    {
        if (_player != null)
        {
            Debug.LogWarning("Player already exist.");
            Destroy(gameObject);
            return;
        }

        _player = this;
        _state = new StateIdle(this);

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();

		var inputStatus = _state.GetInputStatus();
		if (inputStatus.IsMoveForward)
        {
            MoveForward();
        }

        if (inputStatus.IsMoveBackward)
        {
            MoveBackward();
        }

        if (inputStatus.IsMoveLeft)
        {
            MoveLeft();
        }

        if (inputStatus.IsMoveRight)
        {
            MoveRight();
        }

        if(inputStatus.IsShot)
        {
        	Debug.Log("ショット！");
        }

        _state.Action();

		Debug.Log("State : " + _state.GetStateType());
    }

    private void MoveForward()
    {
        _accumrateVec += Forward * _speed;
    }

    private void MoveBackward()
    {
        _accumrateVec -= Forward * _speed;
    }

    private void MoveLeft()
    {
        _accumrateVec -= Right * _speed;
    }

    private void MoveRight()
    {
        _accumrateVec += Right * _speed;
    }

    private void Squat()
    {
        Debug.Log("しゃがむ");
    }

    private void LateUpdate()
    {
        Vector3 newPos = Rigidbody.position + _accumrateVec;
        Rigidbody.MovePosition(newPos);
        _accumrateVec = Vector3.zero;
    }

    private void Rotate()
    {
        Vector3 delta = (Input.mousePosition - _prevPos) * _rotateSpeed;
        _verticalAngle = (_verticalAngle + delta.x) % 360f;
        _horizontalAngle -= delta.y;
        if (_horizontalAngle < 2f)
        {
            _horizontalAngle = 2f;
        }
        if (_horizontalAngle > 90f)
        {
            _horizontalAngle = 90f;
        }

        _prevPos = Input.mousePosition;

        float xPos = Mathf.Sin(_verticalAngle * Mathf.Deg2Rad) * Mathf.Cos(_horizontalAngle * Mathf.Deg2Rad) * _distance;
        float yPos = Mathf.Sin(_horizontalAngle * Mathf.Deg2Rad) * _distance;
        float zPos = Mathf.Cos(_verticalAngle * Mathf.Deg2Rad) * Mathf.Cos(_horizontalAngle * Mathf.Deg2Rad) * _distance;

        Vector3 pos = new Vector3(xPos, yPos, zPos);
        Camera.transform.localPosition = pos;

        Camera.transform.LookAt(transform);

        Vector3 forward = Vector3.ProjectOnPlane(Camera.transform.forward, Vector3.up);
        _modelTrans.forward = forward;
    }

	public void ChangeState(StateType state)
    {
    	if(_state != null)
    	{
    		_state.Exit();
    	}

    	switch(state)
    	{
		case StateType.Idle:
    		_state = new StateIdle(this);
    		break;
		case StateType.Walk:
    		_state = new StateWalk(this);
    		break;
		case StateType.Run:
    		_state = new StateRun(this);
    		break;
		case StateType.Prostrate:
    		_state = new StateProstrate(this);
    		break;
		case StateType.Sit:
			_state = new StateSit(this);
			break;
		case StateType.Jump:
			throw new System.NotImplementedException("まだ未実装です : " + state.ToString());
		default:
			throw new System.NotImplementedException("想定外のステートです : " + state.ToString());
    	}

		_state.Enter();
    }


    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

}


