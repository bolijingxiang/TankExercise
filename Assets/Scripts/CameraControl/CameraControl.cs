using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float distanceAway = 5;
    public float distanceUp = 2;
    public float smooth = 3;

    private Camera m_Camera;
    private float m_ZoomSpeed;
    private GameObject hovercraft;
    private Vector3 targetPosition;
    private Vector3 m_MoveVelocity;

    Transform follow;

	// Use this for initialization
	void Start () {

        m_Camera = GetComponentInChildren<Camera>();

        follow = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        //Move();
        //Zoom();
	}

    private void LateUpdate()
    {
        targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);

        transform.LookAt(follow);
    }

    private void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref m_MoveVelocity,0.2f);
    }


    private void Zoom()
    {
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize,1,ref m_ZoomSpeed,0.2f);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
    }

    //private float FindRequiredSize()
    //{
    //   // Vector3 desiredLocalPos = transform.InverseTransformPoint(targetPosition);
    //   // float size = 0;

    //}
}
