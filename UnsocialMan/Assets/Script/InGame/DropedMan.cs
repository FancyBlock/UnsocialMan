using UnityEngine;
using System.Collections;

public class DropedMan : MonoBehaviour 
{
	//---------------- public properties ----------------
	
	public Judger m_judger;
	
	//---------------- public functions ------------------

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Translate( 0,  CommonConstant.DROP_VELOCITY * Time.deltaTime, 0 );
		
		if( gameObject.transform.position.y <= CommonConstant.BOTTOM_LINE )
		{
			m_judger.EndCurLevel();
			
			Destroy( gameObject );
		}
	}
	
}
