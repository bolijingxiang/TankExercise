﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public static List<Tank> tankList = new List<Tank>();

    private void Start()
    {
        if (Singleton<Gamemanager>.getInstance.miniMap.gameObject.activeInHierarchy)
        {
            Singleton<Gamemanager>.getInstance.miniMap.CreatPoint(tag, transform);
        }
        tankList.Add(this);
    }

    private void Update()
    {
        if(Singleton<Gamemanager>.getInstance.miniMap.gameObject.activeInHierarchy)
        {
            Singleton<Gamemanager>.getInstance.miniMap.UpdatePosition(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        tankList.Remove(this);
    }

}
