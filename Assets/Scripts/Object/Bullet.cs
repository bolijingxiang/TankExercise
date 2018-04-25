using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed=1;

    public enum BulletWorkType
    {
        Idle,
        Work
    }

    public BulletWorkType bulletWorkType = BulletWorkType.Idle;

    private float bulletRecoverTime = 5f;

    GameObject bulletRecoverParent;

    public Transform m_transfrom;

    public Vector3 m_position;

    private void Awake()
    {
       m_transfrom = transform;
       m_position = m_transfrom.position;
    }

    bool first = true;

    private Vector3 oldPosition=Vector3.zero;

    private Vector3 OldPosition
    {
        get
        {
            return oldPosition;
        }
        set
        {
            if(first)
            {
                oldPosition = transform.position;
                first = false;
            }
            oldPosition = value;
        }
    }

    private void OnEnable()
    {
       // Debug.Log("helloWorld:" + transform.rotation);
      //  transform.position += transform.forward * speed * Time.deltaTime;
        StartCoroutine(BulletRecover());
    }

    private void Start()
    {
        if (!GameObject.Find("Recover"))
        {
            bulletRecoverParent = new GameObject("Recover");
        }
        bulletRecoverParent = GameObject.Find("Recover");
    }

    //子弹回收
    IEnumerator BulletRecover()
    {
        yield return new WaitForSeconds(bulletRecoverTime);
        transform.parent = bulletRecoverParent.transform;
        this.bulletWorkType = BulletWorkType.Idle;
        transform.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.rotation = new Quaternion(0,0,0,1);
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        StopCoroutine(BulletRecover());
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    //子弹发射
    public void BulletEmission()
    {
        //transform.position = OldPosition;
        //Vector3 relativePos = transform.position - bulletRecoverParent.transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = rotation;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag != "Player")
    //    {
    //        this.gameObject.SetActive(false);
    //        this.bulletWorkType = BulletWorkType.Idle;
    //        this.transform.parent = GameObject.Find("Recover").transform;

    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            ContactPoint contactPoint = collision.contacts[0];
            GameObject m = Instantiate(Resources.Load("Smoke", typeof(GameObject)) as GameObject);
            m.transform.position = contactPoint.point;
            this.gameObject.SetActive(false);
            this.bulletWorkType = BulletWorkType.Idle;
            this.transform.parent = GameObject.Find("Recover").transform;           
        }
    }

}
