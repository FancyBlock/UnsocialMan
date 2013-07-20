using UnityEngine;
using System.Collections;

public class DropedMan : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		//TODO 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( gameObject.transform.position.y <= CommonConstant.BOTTOM_LINE )
		{
			Destroy( gameObject );
		}
	}
}
