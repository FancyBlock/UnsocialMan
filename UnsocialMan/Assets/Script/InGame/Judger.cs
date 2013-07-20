using UnityEngine;
using System.Collections;

public class Judger : MonoBehaviour 
{
	public const int STATE_CHOOSE	= 0;
	public const int STATE_SHOW_ANI	= 1;
	//public const int STATE_
	
	//---------------- public properties ----------------
	
	public Camera m_camera;
	public int m_totalLevelCount;
	
	//---------------- private members ------------------
	
	protected ArrayList m_resultList;
	protected int m_state;
	protected int m_levelCount;
	
	//---------------- public functions ------------------
	

	// Use this for initialization
	void Start () 
	{
		m_resultList = new ArrayList();
		
		m_state = STATE_CHOOSE;
		m_levelCount = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO 
	}
	
	// add result
	public void AddResult( bool success )
	{
		m_resultList.Add( success );
		
		//TODO 
	}
	
	
	// getter of the state
	public int STATE
	{
		get{	return m_state;		}
	}
	
	
	//---------------- private functions ------------------ 
	
	
	
}
