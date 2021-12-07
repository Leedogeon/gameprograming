using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    float hAxis;
    float vAxis;
    bool sDown;
    bool jUmp;
    bool isJump;
    bool isDodge;
    bool dOdge;
    bool isBorder;
    Vector3 moveVec;
    Vector3 dodgeVec;
    Rigidbody rigid;
    Animator anim;
    bool tAke;
    GameObject nearObject;
    public Weapon equipWeapon;
    int equipWeaponIndex = -1;
    bool isSwap;
    bool cHange;
    bool sDown1;
    bool sDown2;
    int weaponIndex = -1;
    bool cLick;
    float fireDelay;
    bool isFireReady;
    bool rEload;
    public int ammo;
    bool isReload;
    public Camera followCamera;
    void Awake()
    {   
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Take();
        Change();
        ChangeOut();
        Attack();
        Reload();
;
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        sDown = Input.GetButton("Walk");
        jUmp = Input.GetButtonDown("Jump");
        dOdge = Input.GetButtonDown("Dodge");
        tAke = Input.GetButtonDown("Take");
        cHange = Input.GetButtonDown("Change");
        cLick = Input.GetButtonDown("Fire1");
        rEload = Input.GetButtonDown("Reload");

    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        
        if(isDodge)
        moveVec = dodgeVec;
        
        if(!isBorder)
        transform.position+= moveVec * speed * (sDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun",moveVec != Vector3.zero);
        anim.SetBool("isWalk", sDown);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
                if(cLick){
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit, 100)){
            Vector3 nextVec = rayHit.point - transform.position;
            nextVec.y = 0;
            transform.LookAt(transform.position + nextVec);
        }
        }
    }
    void Jump()
    {
        if (jUmp && !isJump && !isDodge) {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump",true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Dodge()
    {
        if (dOdge && moveVec != Vector3.zero && isJump == true && !isDodge) {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut",1f);
        }
    }
    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor") {
            anim.SetBool("isJump",false);
            isJump = false;
        }
    }
    void StopToWall(){
        isBorder = Physics.Raycast(transform.position, transform.forward, 5 ,LayerMask.GetMask("Wall"));
    }
    void FixedUpdate()
    {
        StopToWall();
    }

       void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
           nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
           nearObject = null;
    }

    void Change(){
        if(cHange && (!hasWeapons[0] && !hasWeapons[1]))
            return;

        if(hasWeapons[0] && hasWeapons[1]){
            if(cHange && weaponIndex < 2) weaponIndex ++;
            if(cHange && weaponIndex >= 2) weaponIndex = 0;
        }

        if(!hasWeapons[0]){
            if(cHange) weaponIndex = 1;
        }
        if(!hasWeapons[1]){
            if(cHange) weaponIndex = 0;
        }

        if((cHange)&&!isJump&&!isSwap){
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon =  weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("ChangeOut", 1f);
        }
    }
    void ChangeOut() {
        isSwap = false;
    }

    void Take()
    {
        if(tAke && nearObject != null && !isJump && !isDodge) {
            if(nearObject.tag == "Weapon") {
                item item =  nearObject.GetComponent<item>();
                int weaponIndex =item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }


        void Attack(){
        if(equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(cLick && isFireReady && !isDodge &&!isSwap){
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }

        void Reload(){
        if(equipWeapon == null)
            return;
        
        if(equipWeapon.type == Weapon.Type.Melee)
            return;

        if(ammo == 0)
            return;

        if(rEload && !isJump && !isDodge && isFireReady && !isSwap && !isReload){
            anim.SetTrigger("doReload");
            isReload = true;

            Invoke("ReloadOut",2f);
        }
    }

    void ReloadOut()
    {   
        int reAmmo;
        if(equipWeapon.curAmmo == 0){
        if(ammo < equipWeapon.maxAmmo){
        reAmmo = ammo;
        equipWeapon.curAmmo = reAmmo;
        }
        else { 
            reAmmo = equipWeapon.maxAmmo;
            equipWeapon.curAmmo = equipWeapon.maxAmmo;
            }
                ammo -= reAmmo;
        }
        else if(equipWeapon.curAmmo != 0) {
            if(ammo < equipWeapon.maxAmmo - equipWeapon.curAmmo){
                reAmmo = ammo;
                equipWeapon.curAmmo += reAmmo;
            }
            else{
                reAmmo = equipWeapon.maxAmmo - equipWeapon.curAmmo;
                equipWeapon.curAmmo = equipWeapon.maxAmmo;
            }
                    ammo -= reAmmo;
    }

        isReload = false;
}

}