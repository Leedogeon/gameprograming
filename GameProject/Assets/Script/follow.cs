using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    // 캐릭터 따라가는 카메라
    public Transform target; //캐릭터
    public Vector3 offset; //위치 보정값
    void Update()
    {
        transform.position = target.position + offset;
    }
}