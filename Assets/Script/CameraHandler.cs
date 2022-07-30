using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CameraHandler : MonoBehaviour
    {
        // public Transform targetTransform;
        // public Transform cameraTransform;
        // public Transform cameraPivotTransform;
        // private Transform myTransform;
        // private Vector3 cameraTransformPosition;
        // private LayerMask ignoreLayers;
        //
        // public static CameraHandler singleton;
        //
        // public float lookSpeed = 0.1f;
        // public float followSpeed = 0.1f;
        // public float pivotSpeed = 0.03f;
        //
        // private float defaultPosition;
        // private float lookAngle;
        // private float pivotAngle;
        // public float minimumPivot = -35;
        // public float maximumPivot = 35;
        //
        //
        // private void Awake()
        // {
        //     singleton = this;
        //     myTransform = transform;
        //     defaultPosition = cameraTransform.localPosition.z;
        //     ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        // }
        //
        // public void FollowTarget(float delta)
        // {
        //     Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        //     myTransform.position = targetPosition;
        // }
        //
        // public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        // {
        //     lookAngle += (mouseXInput * lookSpeed) / delta;
        //     pivotAngle -= (mouseYInput * pivotSpeed) / delta;
        //     pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);
        //
        //     Vector3 rotation = Vector3.zero;
        //     rotation.y = lookAngle;
        //     Quaternion targetRotation = Quaternion.Euler(rotation);
        //     myTransform.rotation = targetRotation;
        //
        //     rotation = Vector3.zero;
        //     rotation.x = pivotAngle;
        //
        //     targetRotation = Quaternion.Euler(rotation);
        //     cameraPivotTransform.localRotation = targetRotation;
        // }

        public float topClamp;		//最大角度
        public float bottomClamp;	//最小角度
        public float pivotSpeed; //纵向速度
        private void Start()
        {
            if(topClamp < bottomClamp)
            {
                Debug.LogError("-------------第三人称摄像头控制器 bottomClamp 应该小于 topClamp");
            }
        }

        // private void LateUpdate()
        // {
        //     CameraRotation();
        // }

        private float _targetPitch;
        private float _targetYaw;
        public void CameraRotation(float mouseX, float mouseY)
        {
            // input_look = InputDefine.Instance.input_look;
            var input_look = new Vector2(mouseX, mouseY);
            //后续考虑加入手柄输入的支持
            if(input_look.sqrMagnitude >= 0.01f)
            {
                _targetYaw += input_look.x;
                _targetPitch -= input_look.y * pivotSpeed;
            }
    
            _targetYaw = ClampAngle(_targetYaw, float.MinValue, float.MaxValue);
            _targetPitch = ClampAngle(_targetPitch, bottomClamp, topClamp);
    
            this.transform.eulerAngles = new Vector3(_targetPitch, _targetYaw, 0.0f);
        }
    
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}