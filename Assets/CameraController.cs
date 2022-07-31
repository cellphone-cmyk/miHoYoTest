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
        public Transform follow;
        [SerializeField]
        private Transform LookAt;
        [SerializeField]
        private Transform player;
        [SerializeField]
        private Transform[] enemies;
        //[SerializeField]
        //private AimMark aimMark;

        private int lockedEnemyIndex; //����

        InputHandler inputHandler;

        #region lock on
        public Transform currentLockOnTarget;
        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public Transform nearestLockOnTarget;
        public float maximumLockOnDistance = 30;
        #endregion


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

        private void LateUpdate()
        {
            follow.position = player.position;
            follow.LookAt(nearestLockOnTarget);
        }

        public void HandleLockOn()
        {
            if (inputHandler.lockOnFlag == true)
            {
                //lockedEnemyIndex = (lockedEnemyIndex + 1) % enemies.Length;

                ////ʹfollow��λ�ú����һ��
                //follow.position = player.position;

                ////ʹlookAt��λ�ú���ת����������Ŀ�걣��һ��
                //LookAt.position = (enemies[lockedEnemyIndex].position + player.position) / 2;
                //LookAt.rotation = enemies[lockedEnemyIndex].rotation;

                ////ʹfollow��z��������ָ��lookAt
                //follow.LookAt(LookAt);
                //Debug.Log("look at enemy:" + LookAt.position);

                float shortestDistance = Mathf.Infinity;

                Collider[] colliders = Physics.OverlapSphere(player.position, 26);

                for (int i = 0; i < colliders.Length; i++)
                {
                    CharacterManager character = colliders[i].GetComponent<CharacterManager>();

                    if (character != null)
                    {
                        Vector3 lockTargetDirection = character.transform.position - player.position;
                        float distanceFromTarget = Vector3.Distance(player.position, character.transform.position);
                        float viewableAngle = Vector3.Angle(lockTargetDirection, CmfreeLook.transform.forward);

                        if (character.transform.root != player.transform.root
                            && viewableAngle > -50 && viewableAngle < 50
                            && distanceFromTarget <= maximumLockOnDistance)
                        {
                            availableTargets.Add(character);
                        }
                    }
                }

                for (int k = 0; k < availableTargets.Count; k++)
                {
                    float distanceFromTarget = Vector3.Distance(player.position, availableTargets[k].transform.position);

                    if (distanceFromTarget < shortestDistance)
                    {
                        shortestDistance = distanceFromTarget;
                        nearestLockOnTarget = availableTargets[k].lockOnTransform;
                        
                    }
                }
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

        public void ClearLockOnTargets()
        {
            availableTargets.Clear();
            nearestLockOnTarget = null;
            currentLockOnTarget = null;
        }
    }
}

