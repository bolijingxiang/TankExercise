using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    public static BulletManager Instance;
    public static BulletManager getInStance()
    {
        return Instance;
    }

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }

    public enum BulletType
    {
        One=1,
        Two
    }

    public BulletType bulletType = BulletType.One;

    private float bulletLaunchTime = 0.3f;

    public int BulletNumber=10;

    public int m=0;

    Transform gun;

    private void Start()
    {
        gun = GameObject.Find("Tank/TankRenderers/TankTurret").transform;
        //InvokeRepeating("CreateBullet", 1,0.3f);
    }

    public void CreateBullet(Transform gun,string tag=null)
    {
       // yield return new WaitForSeconds(bulletLaunchTime);
        m = ++m;
        m = m % BulletNumber;
        Transform parent;
        if (!GameObject.Find("LinShi"))
        {
            parent = new GameObject("LinShi").transform;
        }
        parent = GameObject.Find("LinShi").transform;
        ObjectPool<Bullet>.getInstance().GetOut(m.ToString(),bulletType,parent,gun,tag);
        ObjectPool<Bullet>.getInstance().SetIn(m.ToString(), ObjectPool<Bullet>.getInstance().prefab);
    }

}

public class ObjectPool<T> where T:Bullet,new()
{
    public static ObjectPool<T> instance;
    public static ObjectPool<T> getInstance()
    {
        if(instance==null)
        {
            instance = new ObjectPool<T>();
        }
        return instance;
    }

    public GameObject prefab;

    private Dictionary<string, List<T>> pool = new Dictionary<string, List<T>>();

    //读取对象池
    public void GetOut(string objectName,BulletManager.BulletType bulletType,Transform parent=null,Transform gun=null,string tag=null)
    {

        if(pool.ContainsKey(objectName)&&pool[objectName].Count>0&&pool[objectName][0].bulletWorkType==Bullet.BulletWorkType.Idle)//如果存在子弹是关闭状态
        {
            Bullet pre = pool[objectName][0];
            pre.transform.parent = parent;
           // pre.transform.position = BulletPosition(gun);//gun.position;
          //  pre.transform.rotation = pre.m_transfrom.rotation;
            pre.gameObject.SetActive(true);
            BulletEmission(pre.transform,gun.transform,tag);
        }
        else
        {
            if(!pool.ContainsKey(objectName))
            {
                GameObject gameObject = GameObject.Instantiate(Resources.Load(((int)bulletType).ToString())) as GameObject;             
                gameObject.GetComponent<Bullet>().bulletWorkType = Bullet.BulletWorkType.Work;
                gameObject.transform.parent = parent;
                gameObject.name = objectName;
                prefab = gameObject;
                Debug.Log("gameObject.transform:"+gameObject.name);
                Debug.Log("gun.transform:"+gun.name);
                BulletEmission(gameObject.transform, gun.transform,tag);
            }
        }
    }

    Vector3 BulletPosition(Transform gun)
    {
        Vector3 n;
        n = gun.transform.localPosition + new Vector3(0,0.507f,1.133f);
        return n;
    }



    void BulletEmission(Transform bullet,Transform gun,string tag=null)
    {
        Quaternion rotation = Quaternion.LookRotation(gun.forward);      
        bullet.transform.rotation = rotation;
        bullet.transform.position = gun.transform.position;
        bullet.transform.position += bullet.forward *1.5f + bullet.up * 0.5f;
        bullet.GetComponent<Rigidbody>().isKinematic = false;
        if(tag!=null)
        {
            bullet.tag = tag;
            Debug.Log("HelloWorld");
        }
    }

    //存入对象池
    public void SetIn(string objectName,GameObject bullet)
    {
        if(!pool.ContainsKey(objectName))
        {
            pool.Add(objectName,new List<T>());
            pool[objectName].Add(bullet.GetComponent<T>());
        }
    }


    public void DestoryPool(string objectName)
    {
        if(pool.ContainsKey(objectName))
        {
            pool[objectName] = null;
            pool.Remove(objectName);
        }
    }
}
