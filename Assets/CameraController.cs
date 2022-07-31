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

            //在UI层锁定标记的跟随对象
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
            //按下鼠标中键切换锁定的敌人
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    lockedEnemyIndex = (lockedEnemyIndex + 1) % enemies.Length;
            //}

            if (inputHandler.lockOnFlag == true)
            {
                lockedEnemyIndex = (lockedEnemyIndex + 1) % enemies.Length;

                //使follow的位置和玩家一致
                follow.position = player.position;

                //使lookAt的位置和旋转都和锁定的目标保持一致
                LookAt.position = (enemies[lockedEnemyIndex].position + player.position) / 2;
                LookAt.rotation = enemies[lockedEnemyIndex].rotation;

                //使follow的z轴正方向指向lookAt
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

