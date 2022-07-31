using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SG
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineFreeLook CmfreeLook;
        [SerializeField]
        private Transform follow;
        [SerializeField]
        private Transform LookAt;
        [SerializeField]
        private Transform player;
        [SerializeField]
        private Transform[] enemies;
        //[SerializeField]
        //private AimMark aimMark;

        private int lockedEnemyIndex;

        InputHandler inputHandler;

        private void Awake()
        {
            CmfreeLook.Follow = follow;
            CmfreeLook.LookAt = LookAt;

            //��UI��������ǵĸ������
            //
            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void Start()
        {
            follow.position = player.position;
            LookAt.position = player.position;
            LookAt.rotation = player.rotation;
            follow.LookAt(LookAt);
        }

        private void Update()
        {

        }

        public void HandleLockOn()
        {
            //��������м��л������ĵ���
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    lockedEnemyIndex = (lockedEnemyIndex + 1) % enemies.Length;
            //}

            if (inputHandler.lockOnFlag == true)
            {
                lockedEnemyIndex = (lockedEnemyIndex + 1) % enemies.Length;

                //ʹfollow��λ�ú����һ��
                follow.position = player.position;

                //ʹlookAt��λ�ú���ת����������Ŀ�걣��һ��
                LookAt.position = (enemies[lockedEnemyIndex].position + player.position) / 2;
                LookAt.rotation = enemies[lockedEnemyIndex].rotation;

                //ʹfollow��z��������ָ��lookAt
                follow.LookAt(LookAt);
                Debug.Log("look at enemy:" + LookAt.position);
            }
            else
            {
                follow.position = player.position;
                LookAt.position = player.position;
                LookAt.rotation = player.rotation;
                follow.LookAt(LookAt);
                Debug.Log("look at:"+LookAt.position);
            }
        }
    }
}

