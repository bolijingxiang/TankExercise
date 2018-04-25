using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TankAI
{
    public class AITank : MonoBehaviour
    {
        //状态
        public enum State
        {
            Idle,
            Patrol,
            Attack
        }

        public float speed;

        private List<Transform> m = new List<Transform>();

        private Transform gun;

        private void OnEnable()
        {
            if (Singleton<Gamemanager>.getInstance.miniMap.gameObject.activeInHierarchy)
            {
                Singleton<Gamemanager>.getInstance.miniMap.CreatPoint(tag, transform);
            }
        }

        // Use this for initialization
        void Start()
        {
            gun = transform.Find("TankRenderers/TankTurret");
            navMeshAgent = GetComponent<NavMeshAgent>();
            GetComponent<NavMeshAgent>().destination = RandomPosition();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<NavMeshAgent>().destination = RandomPosition();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Singleton<BulletManager>.getInstance.CreateBullet(gun, "AIBullet");
            }

            if (Singleton<Gamemanager>.getInstance.miniMap.gameObject.activeInHierarchy)
            {
                Singleton<Gamemanager>.getInstance.miniMap.UpdatePosition(transform);
            }
        }

        //随机选取位置，然后到达该位置
        Vector3 RandomPosition()
        {
            Vector3 m = transform.position;
            float x_Left = transform.position.x + 30;
            float x_Right = -transform.position.x + 30;
            float z_up = 30 - transform.position.y;
            float z_down = transform.position.y + 30;


            float x01, x02, z01, z02;

            x01 = x_Left < 10 ? x_Left : 10;
            x02 = x_Right < 10 ? x_Right : 10;
            z01 = z_down < 10 ? z_down : 10;
            z02 = z_up < 10 ? z_up : 10;

            float random_x = Random.Range(-x01, x02);
            float random_z = Random.Range(-z01, z02);


            float x_postion = transform.position.x + (random_x > 0 ? random_x + 10 : random_x - 10) / 2;
            float z_position = transform.position.z + (random_z > 0 ? random_z + 10 : random_z - 10) / 2;

            return new Vector3(x_postion, 0, z_position);
        }

        private NavMeshAgent navMeshAgent;

        //判断AITank是不是在移动
        private bool isMoving()
        {
            if (!navMeshAgent.enabled)
            {
                return false;
            }
            bool r = navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance || navMeshAgent.velocity != Vector3.zero;
            r = navMeshAgent.enabled ? r : false;
            return r;
        }

        //关闭小地图上的红点
        private void OnDisable()
        {
            Singleton<Gamemanager>.getInstance.miniMap.DeletePoint(transform);
        }

        //放射线检测

    }
}



