using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float topClamp;		//最大角度
    public float bottomClamp;	//最小角度
    public float pivotSpeed; //纵向速度
    public Transform cameraTransform;        
    public Transform aimTarget;
    public bool isAiming;
        
    private void Start()
    {
        if(topClamp < bottomClamp)
        {
            Debug.LogError("-------------第三人称摄像头控制器 bottomClamp 应该小于 topClamp");
        }
    }

    private float _targetPitch;
    private float _targetYaw;
    public void CameraRotation(float mouseX, float mouseY, float delta)
    {
        if (isAiming && aimTarget != null)
        {
            //cameraPosition.z = Mathf.Lerp(cameraTransform.localPosition.z,aimTarget.transform.localPosition.z, delta/0.2f );
                
            this.transform.LookAt(aimTarget.transform);
            _targetPitch = ClampAngle(this.transform.eulerAngles.x, 60, 0);
            this.transform.eulerAngles = new Vector3(_targetPitch, this.transform.eulerAngles.y,0.0f);
        }
        else
        {
            // input_look = InputDefine.Instance.input_look;
            var input_look = new Vector2(mouseX, mouseY);
            if(input_look.sqrMagnitude >= 0.01f)
            {
                _targetYaw += input_look.x;
                _targetPitch -= input_look.y * pivotSpeed;
            }
    
            _targetYaw = ClampAngle(_targetYaw, float.MinValue, float.MaxValue);
            _targetPitch = ClampAngle(_targetPitch, bottomClamp, topClamp);
    
            this.transform.eulerAngles = new Vector3(_targetPitch, _targetYaw, 0.0f);
        }
    }
    
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
