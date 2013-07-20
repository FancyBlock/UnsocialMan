using UnityEngine;
using System.Collections;

public class DragedMan : MonoBehaviour 
{
	//---------------- public properties ----------------
	
	public Camera m_camera;
	public GameObject m_dropedMan;
	public Judger m_judger;
	
	
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
		m_isDraging = true;
	}
	
	// stop darg
	public void StopDrag()
	{
		m_isDraging = false;
		
		GameObject dropedMan = (GameObject)Instantiate( m_dropedMan, gameObject.transform.position, Quaternion.identity );
		dropedMan.GetComponent<DropedMan>().m_judger = m_judger;
		Destroy( gameObject );
	}
	
	
	//---------------- private functions ------------------ 
	
}
