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
	public GameObject m_wrongMark;
	public GameObject m_curWindow;
	public GameObject m_mockupResident;
	public GameObject m_mockupWindow;
	public GameObject m_mockupCurtain;
	public string[] m_levelPrefix;
	public string[] m_hintTexts;
	public int m_windowCnt;
	public tk2dSprite m_hintText;
	public AudioClip m_seRight;
	public AudioClip m_seWrong;
	public AudioClip m_seHit;
	public AudioClip m_seFly;
	public AudioClip m_seOpenWindow;
	
	//---------------- private members ------------------
	
	protected ArrayList m_resultList;
	protected int m_state;
	protected int m_levelCount;
	protected float m_time;
	protected ArrayList m_windowPosOffset;
	protected ArrayList m_residentPosOffset;
	
	protected Vector3 m_windowScale;
	protected Vector3 m_residentScale;
	
	//---------------- public functions ------------------
	

	// Use this for initialization
	void Start () 
	{
		m_resultList = new ArrayList();
		m_windowPosOffset = new ArrayList();
		m_residentPosOffset = new ArrayList();
		
		// save the position of the window and resident
		getWindowAndResidentOffset();
		
		m_levelCount = 1;
		createNextLevel();
		
		startChoose();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// player turn
		if( m_state == STATE_CHOOSE )
		{
			if( m_time >= GlobalWork.CurrentTimeOut )
			{
				GlobalWork.LastLevelRestTime = 0.0f;
				
				m_state = STATE_WAIT;
				
				if( m_curWindow == null )
				{
					m_resultList.Add( false );
					StartCoroutine( "chooseTimeout" );
				}
				else
				{
					m_curWindow.SendMessage( "ForceRelease" );
				}
			}
			
			m_time += Time.deltaTime;
		}
	}
	
	// add result
	public void AddResult( bool success )
	{		
		m_resultList.Add( success );
		
		if( success )
		{
			AudioSource.PlayClipAtPoint( m_seRight, Vector3.zero);
		}
		else
		{
			AudioSource.PlayClipAtPoint( m_seWrong, Vector3.zero);
		}
		
		m_state = STATE_WAIT;
	}
	
	
	public void PlayFlySound()
	{
		AudioSource.PlayClipAtPoint( m_seFly, Vector3.zero );
	}
	
	
	public void PlayHitSound()
	{
		AudioSource.PlayClipAtPoint( m_seHit, Vector3.zero );
	}
	
	
	public void PlayOpenWindowSound()
	{
		AudioSource.PlayClipAtPoint( m_seOpenWindow, Vector3.zero );
	}
	
	
	// getter of the state
	public int STATE
	{
		get{	return m_state;		}
	}
	
	
	public float TIME
	{
		get{	return m_time;		}
	}
	
	
	// getter of the level count
	public int CUR_LEVEL_CNT
	{
		get{	return m_levelCount;	}
	}
	
	
	// return the result list
	public ArrayList GetResultList()
	{
		return m_resultList;
	}
	
	
	// callback when droped man hit the ground
	public void EndCurLevel()
	{
		GlobalWork.LastLevelRestTime = GlobalWork.CurrentTimeOut - m_time;
		
		// transform to the next level
		if( m_levelCount < m_totalLevelCount )
		{
			m_levelCount++;
			
			createNextLevel();
			StartCoroutine( "gotoNextLevel" );
			
			m_state = STATE_TRANS;
		}
		// all levels complete
		else
		{
			processResult();
			
			m_state = STATE_SHOW_ANI;
		}
	}
	
	
	//---------------- private functions ------------------ 
	
	
	// trans to the next sub level
	protected IEnumerator gotoNextLevel()
	{
		m_hintText.gameObject.SetActive( false );
		m_time = 0.0f;
		
		// move to the next level
		for( int i = 0; i < 100; i++ )
		{
			m_camera.transform.Translate( 0, 2.0f/100.0f, 0 );
			yield return new WaitForFixedUpdate();
		}
		
		startChoose();
	}
	
	
	// choose timeout
	protected IEnumerator chooseTimeout()
	{
		yield return new WaitForSeconds( 0.5f );
		addWrongMark();
		yield return new WaitForSeconds( 1.5f );
		EndCurLevel();
	}
	
	
	// add a 'X' to the correct window
	protected void addWrongMark()
	{
		AudioSource.PlayClipAtPoint( m_seWrong, Vector3.zero);
		
		Vector3 cornor1 = m_camera.ScreenToWorldPoint( Vector3.zero );
		Vector3 cornor2 = m_camera.ScreenToWorldPoint( new Vector3( Screen.width, Screen.height, 0.0f ) );
		
		float left = cornor1.x < cornor2.x ? cornor1.x : cornor2.x;
		float right = cornor1.x > cornor2.x ? cornor1.x : cornor2.x;
		float bottom = cornor1.y < cornor2.y ? cornor1.y : cornor2.y;
		float top = cornor1.y > cornor2.y ? cornor1.y : cornor2.y;
		
		GameObject[] residents = GameObject.FindGameObjectsWithTag( CommonConstant.TAG_RESIDENT );
		
		foreach( GameObject obj in residents )
		{
			if( obj.GetComponent<Resident>().m_isOutlier )
			{
				if( obj.transform.position.x > left &&
					obj.transform.position.x < right &&
					obj.transform.position.y < top &&
					obj.transform.position.y > bottom )
				{
					Vector3 newPos = obj.transform.position;
					newPos.z = 90.0f;
					Instantiate( m_wrongMark, newPos, Quaternion.identity );
				}
			}
		}
	}
	
	
	// start to choose
	protected void startChoose()
	{
		m_curWindow = null;
		m_time = 0.0f;
		m_state = STATE_CHOOSE;
		
		m_hintText.gameObject.SetActive( true );
		m_hintText.SetSprite( m_hintTexts[m_levelCount-1] );
	}
	
	
	// create next level
	protected void createNextLevel()
	{
		Vector3 newPos;
		int i;
		
		int flashWindowCnt = 0;
		int curtainCnt = 0;				// add curtain
		
		// create windows
		foreach( Vector3 pos in m_windowPosOffset )
		{
			newPos = pos;
			newPos.y += 2.0f * ( m_levelCount - 1 );
			GameObject window = (GameObject)Instantiate( m_mockupWindow, newPos, Quaternion.identity );
			window.transform.localScale = m_windowScale;
			
			// random flash
			if( Random.value < 0.2f )
			{
				window.GetComponent<Window>().m_isFlash = true;
				flashWindowCnt++;
			}
			
			// random curtain
			if( Random.value < 0.2f )
			{
				GameObject curtain = (GameObject)Instantiate( m_mockupCurtain, newPos, Quaternion.identity );
				curtain.GetComponent<Curtain>().m_judger = this;
				curtain.transform.localScale = m_windowScale;
				newPos.z = 100;
				newPos.y += ( 0.01801158f * Mathf.Abs( m_windowScale.x ) );
				curtain.transform.position = newPos;
				curtainCnt++;
			}
		}
		
		// create residents
		string curImgPrefix = m_levelPrefix[m_levelCount-1];
		GameObject[] residents = new GameObject[m_windowCnt];
		for( i = 0; i < m_windowCnt; i++ )
		{
			residents[i] = (GameObject)Instantiate( m_mockupResident, Vector3.right * 2, Quaternion.identity );
			residents[i].transform.localScale = m_windowScale;			//[TEMP]
			Resident resident = residents[i].GetComponent<Resident>();
			resident.m_camera = m_camera;
			resident.m_judger = this;
			tk2dSprite imgResident = residents[i].GetComponent<tk2dSprite>();
			
			if( i == 0 )
			{
				imgResident.SetSprite( curImgPrefix + 1 );
				resident.m_isOutlier = true;
			}
			else if( i < ( m_windowCnt / 2 + 1 ) )
			{
				imgResident.SetSprite( curImgPrefix + 2 );
				resident.m_isOutlier = false;
			}
			else
			{
				imgResident.SetSprite( curImgPrefix + 3 );
				resident.m_isOutlier = false;
			}
			
			// flip 
			if( i % 2 != 0 )
			{
				imgResident.scale = new Vector3( -1, 1, 1 );
			}
		}
		
		// random the sequence
		for( i = 0; i < m_windowCnt; i++ )
		{
			int idx1 = Random.Range( 0, m_windowCnt - 1 );
			int idx2 = Random.Range( 0, m_windowCnt - 1 );
			GameObject tmp = residents[idx1];
			residents[idx1] = residents[idx2];
			residents[idx2] = tmp;
		}
		
		i = 0;
		foreach( Vector3 pos in m_residentPosOffset )
		{
			newPos = pos;
			newPos.y += 2.0f * ( m_levelCount - 1 );
			residents[i].transform.position = newPos;
			
			i++;
		}
		
		GlobalWork.CurrentTimeOut = 2.0f + m_windowCnt * 0.2f + flashWindowCnt * 0.25f + curtainCnt * 0.5f - GlobalWork.LastLevelRestTime * 0.25f;
		
		if( GlobalWork.CurrentTimeOut < 3.0f )
		{
			GlobalWork.CurrentTimeOut = 3.0f;
		}
	}
	
	
	// process result
	protected void processResult()
	{
		int correctCnt = 0;
		
		foreach( bool result in m_resultList )
		{
			if( result )
			{
				correctCnt++;
			}
		}
		
		if( correctCnt >= CommonConstant.WIN_MAN_CNT )
		{
			// win
			if( GlobalWork.CurrentBuilding == 1 )
			{
				GlobalWork.CurrentBuilding++;
				Application.LoadLevel( "InGameLv2" );
			}
			else if( GlobalWork.CurrentBuilding == 2 )
			{
				GlobalWork.CurrentBuilding++;
				Application.LoadLevel( "InGameLv3" );
			}
			else
			{
				Application.LoadLevel( "Win" );
			}
		}
		else
		{
			// fail
			Application.LoadLevel( "Lose" );
		}
	}
	
	
	// get window and resident position offset
	protected void getWindowAndResidentOffset()
	{
		GameObject[] residents = GameObject.FindGameObjectsWithTag( CommonConstant.TAG_RESIDENT );
		GameObject[] windows = GameObject.FindGameObjectsWithTag( CommonConstant.TAG_WINDOW );
		
		foreach( GameObject resident in residents )
		{
			m_residentPosOffset.Add( new Vector3( resident.transform.position.x,
													resident.transform.position.y,
														resident.transform.position.z ) );
		}
		
		foreach( GameObject window in windows )
		{
			m_windowPosOffset.Add( new Vector3( window.transform.position.x,
												window.transform.position.y,
												window.transform.position.z ) );
		}
		
		// save the scale
		m_windowScale = windows[0].transform.localScale;
		m_residentScale = residents[0].transform.localScale;
		
		// destory all the gameobject
		foreach( GameObject resident in residents )
		{
			Destroy( resident );
		}
		foreach( GameObject window in windows )
		{
			Destroy( window );
		}
		
	}
	
}
