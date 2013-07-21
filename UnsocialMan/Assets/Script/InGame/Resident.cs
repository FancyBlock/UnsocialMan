using UnityEngine;
using System.Collections;

public class Resident : MonoBehaviour 
{
	
	public const int STATE_IDLE		= 0;
	public const int STATE_DRAG		= 1;
	public const int STATE_EMPTY	= 2;
	
	//---------------- public properties ----------------
	
	
	public bool m_isOutlier;
	public string m_stagePrefix;
	public GameObject m_dragedMan;
	public Camera m_camera;
	public Judger m_judger;
	
	
	//---------------- private members ------------------
	
	
	protected tk2dSprite m_image = null;
	protected string m_curImage = "";
	protected GameObject m_dragedManObj = null;
	protected int m_state = 0;
	
	
	//---------------- public functions ------------------
	
	
	// Use this for initialization
	void Start () 
	{
		m_image = gameObject.GetComponent<tk2dSprite>();
		m_curImage = m_image.CurrentSprite.name;
	}

	
	// when player start to drag this item
	void OnMouseDown()
	{
		if( m_state != STATE_IDLE || m_judger.STATE != Judger.STATE_CHOOSE )
		{
			return;
		}
		
		m_judger.PlayFlySound();
		
		m_image.SetSprite( CommonConstant.SPR_BLANK_WINDOW );
		
		// create the dragman
		m_dragedManObj = (GameObject)Instantiate( m_dragedMan, gameObject.transform.position, Quaternion.identity );
		m_dragedManObj.transform.Translate( 0, 0, -11.0f );		// put this in front of the window
		DragedMan man = m_dragedManObj.GetComponent<DragedMan>();
		man.m_camera = m_camera;
		man.m_judger = m_judger;
		man.StartDrag();
		
		m_judger.m_curWindow = gameObject;
		
		m_state = STATE_DRAG;
	}
	
	// when player release the mouse button
	void OnMouseUp()
	{
		if( m_state != STATE_DRAG || m_judger.STATE != Judger.STATE_CHOOSE )
		{
			return;
		}
		
		m_judger.m_curWindow = null;
		
		Vector3 mousePos = m_camera.ScreenToWorldPoint( Input.mousePosition );
		Vector3 windowPos = gameObject.transform.position;
		mousePos.z = windowPos.z;
		float distance = ( mousePos - windowPos).magnitude;
		
		// release the man 
		if( distance >= CommonConstant.RELEASE_DISTANCE )
		{
			m_dragedManObj.SendMessage( "StopDrag" );
			
			judgeResult();
			m_state = STATE_EMPTY;
		}
		// drag man out fail
		else
		{
			Revert();
		}
	}
	
	// generate this resident by index, index 1 is the outlier
	public void Generate( int index )
	{
		m_curImage = m_stagePrefix + index;
		m_image.SetSprite( m_curImage );
		
		m_isOutlier = index == 1 ? true : false;
	}
	
	// revert the drag
	public void Revert()
	{
		m_image.SetSprite( m_curImage );
		
		Destroy( m_dragedManObj );
		
		m_state = STATE_IDLE;
	}
	
	// force release the man
	public void ForceRelease()
	{
		m_judger.m_curWindow = null;
		
		m_dragedManObj.SendMessage( "StopDrag" );
			
		judgeResult();
		m_state = STATE_EMPTY;
	}
	
	
	//---------------- private functions ------------------
	
	
	// judge the result 
	protected void judgeResult()
	{
		// choose the correct man
		if( m_isOutlier )
		{
			m_image.SetSprite( CommonConstant.SPR_CORRECT_MARK );
			
			m_judger.AddResult( true );
		}
		// did the wrong choice
		else
		{
			m_image.SetSprite( CommonConstant.SPR_WRONG_MAKR );
			
			m_judger.AddResult( false );
		}
	}
	
}
