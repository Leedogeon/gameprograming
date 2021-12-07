using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;
    public void Use()
    {
        if(type == Type.Melee) {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range && curAmmo > 0) {
            curAmmo --;
            StopCoroutine("Shot");
            StartCoroutine("Shot");
        }
    }
    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f); //0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled =true;
        //2
        yield return new WaitForSeconds(0.3f); //0.3초 대기
        meleeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f); //0.3초 대기
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 100;
        yield return null;
    }
}
