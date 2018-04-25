using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {
    //地图的中的各种信息
    Dictionary<GameObject, GameObject> point = new Dictionary<GameObject, GameObject>();//存储地图上tank和点的对应信息
    public Vector2 miniMapborad = new Vector2(50,50);//地图的边界
    public RectTransform miniBackground;//地图的长宽

    Vector2 PlayerTankRectPosition(Transform m)
    {
        float s = m.position.x/miniMapborad.x;//在地图位置中x的比例
        float t = m.position.z/miniMapborad.y;//在地图位置中z的比例
        return new Vector2((miniBackground.sizeDelta.x/2)*s,(miniBackground.sizeDelta.y/2)*t);
    }

    //创建红点
    public void CreatPoint(string tag,Transform tank)
    {
        GameObject m = Instantiate(Resources.Load("Point")) as GameObject;
        if (tag=="player") m.GetComponent<Image>().color = Color.red;
        if(tag=="AITank") m.GetComponent<Image>().color = Color.blue;
        m.transform.parent = miniBackground.transform;
        m.GetComponent<RectTransform>().anchoredPosition = PlayerTankRectPosition(tank);
        m.GetComponent<RectTransform>().sizeDelta = new Vector2(10,10);
        point.Add(tank.gameObject,m);
    }

    //删除红点
    public void DeletePoint(Transform tank)
    {
        if (point.ContainsKey(tank.gameObject))
        {
            Destroy(point[tank.gameObject]);
            point.Remove(tank.gameObject);
        }
    }

    //更新点信息
    public void UpdatePosition(Transform tank)
    {
        if(point.ContainsKey(tank.gameObject))
        {
            point[tank.gameObject].GetComponent<RectTransform>().anchoredPosition = PlayerTankRectPosition(tank);
        }
    }
}
