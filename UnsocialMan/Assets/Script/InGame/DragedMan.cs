using UnityEngine;
using System.Collections;

public class DragedMan : MonoBehaviour 
{
	//---------------- public properties ----------------
	
	public Camera m_camera;
	public GameObject m_dropedMan;
	
	
	//---------------- private members ------------------
	
	protected bool m_isDraging = false;
	
	
	//---------------- public functions ------------------
	
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( m_isDraging )
		{
			Vector3 pos = m_camera.ScreenToWorldPoint( Input.mousePosition );
			gameObject.transform.position = new Vector3( pos.x, pos.y, gameObject.transform.position.z );
		}
	}
	
	// start drag
	public void StartDrag()
	{
		//TODO 
		
		m_isDraging = true;
	}
	
	// stop darg
	public void StopDrag()
	{
		m_isDraging = false;
		
		Instantiate( m_dropedMan, gameObject.transform.position, Quaternion.identity );
		Destroy( gameObject );
	}
	
	
	//---------------- private functions ------------------ 
	
}
