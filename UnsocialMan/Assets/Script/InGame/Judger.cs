using UnityEngine;
using System.Collections;

public class Judger : MonoBehaviour 
{
	public const int STATE_CHOOSE	= 0;
	public const int STATE_SHOW_ANI	= 1;
	public const int STATE_WAIT		= 2;
	public const int STATE_TRANS	= 3;
	
	//---------------- public properties ----------------
	
	public Camera m_camera;
	public int m_totalLevelCount;
	
	//---------------- private members ------------------
	
	protected ArrayList m_resultList;
	protected int m_state;
	protected int m_levelCount;
	protected float m_time;
	
	//---------------- public functions ------------------
	

	// Use this for initialization
	void Start () 
	{
		m_resultList = new ArrayList();
		
		m_levelCount = 1;
		
		startChoose();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// player turn
		if( m_state == STATE_CHOOSE )
		{
			if( m_time >= CommonConstant.TIMEOUT_TIME )
			{
				addWrongMark();
				//TODO 
			}
			
			m_time += Time.deltaTime;
		}
	}
	
	// add result
	public void AddResult( bool success )
	{
		m_resultList.Add( success );
		
		m_state = STATE_WAIT;
	}
	
	
	// getter of the state
	public int STATE
	{
		get{	return m_state;		}
	}
	
	
	// callback when droped man hit the ground
	public void EndCurLevel()
	{
		// transform to the next level
		if( m_levelCount < m_totalLevelCount )
		{
			StartCoroutine( "gotoNextLevel" );
			
			m_levelCount++;
			m_state = STATE_TRANS;
		}
		// all levels complete
		else
		{
			//TODO 
			
			m_state = STATE_SHOW_ANI;
		}
	}
	
	
	//---------------- private functions ------------------ 
	
	
	// trans to the next sub level
	protected IEnumerator gotoNextLevel()
	{
		// move to the next level
		for( int i = 0; i < 100; i++ )
		{
			m_camera.transform.Translate( 0, 2.0f/100.0f, 0 );
			yield return new WaitForFixedUpdate();
		}
		
		startChoose();
	}
	
	
	// add a 'X' to the correct window
	protected void addWrongMark()
	{
		//TODO 
	}
	
	
	// start to choose
	protected void startChoose()
	{
		m_time = 0.0f;
		m_state = STATE_CHOOSE;
	}
	
}
