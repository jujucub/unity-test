using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStatus
{
	public bool IsMoveForward{get;set;}
	public bool IsMoveBackward{get;set;}
	public bool IsMoveRight{get;set;}
	public bool IsMoveLeft{get;set;}
    public bool IsShot { get; set; }
    public bool IsRun { get; set; }
	public bool IsProstrate{get;set;} // ふせ
	public bool IsSit{get;set;}

	public bool IsMoving
	{
		get{ return IsMoveForward || IsMoveBackward || IsMoveLeft || IsMoveRight; }
	}
}
