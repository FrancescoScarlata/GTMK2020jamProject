using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour,IDamageble
{
    public static CharacterController instance;


    [Header("Characteristics")]
    public float HPs;
    public float maxHP=10;

    [Space(10)]

    public float movSpeed=2;
    public Animator anim;
    public Rigidbody2D rigidBody2D;
    public LayerMask NotIgnoredLayers;
    public _Weapon[] weapons;
    public bool isReloading=false;
    public bool isKnocked = false;
    [Space(10)]

    public Transform gunSpawnProjectile;
    public Transform shotgunSpawnProjectile;
    public Transform rocketSpawnProjectile;

    [Space(10)]

    public int currWeaponIndex=-1;
    protected bool isAlive=true;
    protected bool isDoingSomeSpecialRecoil;

    protected Coroutine shootCoroutine;
    protected Coroutine recoilEffectCoroutine;
    protected WaitForSeconds waitAttackSpeed;
    protected WaitForSeconds waitReloadTime;
    protected RaycastHit2D hitFront;
    protected RaycastHit2D hitLeft;
    protected RaycastHit2D hitRight;

    // Start is called before the first frame update
    void Start()
    {
        HPs = maxHP;
        foreach (_Weapon weapon in weapons)
            weapon.currMagazineIndex = 0;

        ChangeWeapon(currWeaponIndex);
    }

    private void Awake() // singleton
    {
        if (instance)
            Destroy(this.gameObject);
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (isKnocked||!isAlive)
            return;
        #region weapon choice
        if (!isReloading)
        {
            if (Input.GetButtonDown("BareMetal"))
            {
                ChangeWeapon(-1);
            }
            if (Input.GetButtonDown("ChainsawWeapon"))
            {
                ChangeWeapon((int)WeaponType.chainsaw);
            }
            if (Input.GetButtonDown("GunWeapon"))
            {
                ChangeWeapon((int)WeaponType.gun);
            }
            if (Input.GetButtonDown("ShotgunWeapon"))
            {
                ChangeWeapon((int)WeaponType.shotgun);
            }
            if (Input.GetButtonDown("RocketWeapon"))
            {
                ChangeWeapon((int)WeaponType.rocketLauncher);
            }
            if (Input.GetButtonDown("BerserkWeapon"))
            {
                ChangeWeapon((int)WeaponType.berserkSword);
            }

        }
            #endregion

         if (!isDoingSomeSpecialRecoil)
        {
            // check the shoot -> the recoil effects is in the shoot
            //character goes everytime in front of him
            hitFront = Physics2D.Raycast(transform.position, transform.up, 1.5f, NotIgnoredLayers);
            hitLeft = Physics2D.Raycast(transform.position, -transform.right, 0.55f, NotIgnoredLayers);
            hitRight = Physics2D.Raycast(transform.position, transform.right, 0.55f, NotIgnoredLayers);

            Debug.DrawRay(transform.position, transform.up, Color.red, .5f);
            Debug.DrawRay(transform.position, -transform.right, Color.yellow, .5f);
            Debug.DrawRay(transform.position, transform.right, Color.green, .5f);
            if (hitFront.collider==null && hitLeft.collider == null && hitRight.collider == null)
            {
                rigidBody2D.velocity = (transform.up * movSpeed);
            }
            else
            {
                /*
                if(hitFront.collider)
                    Debug.Log($"hit point front is:{hitFront.collider.name} ");
                if (hitLeft.collider)
                    Debug.Log($"hit point right is:{hitLeft.collider.name} ");
                if (hitRight.collider)
                    Debug.Log($"hit point left is:{hitRight.collider.name} ");
                */
                rigidBody2D.velocity = Vector2.zero;
            }
                

        }
    }


    public void SetIsDoindRecoil(bool isItDoingIt)
    {
        isDoingSomeSpecialRecoil = isItDoingIt;
    }

    public void GetDamage(float dmg)
    {
        HPs -= dmg;
        InGameUIManager.instance.UpdateHP(HPs,maxHP);
        CameraShake.instance.ExecuteShake();
        if(HPs<=0)
        {
            // game manager to do something
        }
    }

    /// <summary>
    /// Changes the weapon type equipped
    /// </summary>
    /// <param name="weaponType"></param>
    public void ChangeWeapon(int weaponType)
    {
        if (!isReloading)
        {
            currWeaponIndex=weaponType;
            isDoingSomeSpecialRecoil = false;
            // resetImage is position if needed
            if (currWeaponIndex >= 0)
            {
                waitAttackSpeed = new WaitForSeconds(weapons[weaponType].attackSpeed);
                waitReloadTime = new WaitForSeconds(weapons[weaponType].reloadTime);
                movSpeed = weapons[weaponType].moVspeed;
            }
            else
            {
                InGameUIManager.instance.UpdateMagazineInfo(0, 0);
                movSpeed = 4;
            }
            anim.SetInteger("weaponUsed", currWeaponIndex);
            InGameUIManager.instance.UpdateCurrWeapon(currWeaponIndex);
            if (shootCoroutine != null)
                StopCoroutine(shootCoroutine);
            shootCoroutine=StartCoroutine(Shoot());
        }
    }


    /// <summary>
    /// Method called while the robot is alive
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        while (isAlive && currWeaponIndex>=0)
        {
            if (recoilEffectCoroutine != null)
                StopCoroutine(recoilEffectCoroutine);
            recoilEffectCoroutine= StartCoroutine( weapons[currWeaponIndex].RecoilEffect(this) );
            weapons[currWeaponIndex].currMagazineIndex++;
            InGameUIManager.instance.UpdateMagazineInfo(weapons[currWeaponIndex].currMagazineIndex, weapons[currWeaponIndex].capacityMagazine);
            // shoot audio

            yield return waitAttackSpeed;
            if (currWeaponIndex >=0 && weapons[currWeaponIndex].currMagazineIndex >= weapons[currWeaponIndex].capacityMagazine)
            {
                isReloading = true;

                InGameUIManager.instance.StartReloading();
                if (weapons[currWeaponIndex].reloadClip != null)
                    SoundEffectManager.instance.PlaySFX(weapons[currWeaponIndex].reloadClip);
                yield return waitReloadTime;
                InGameUIManager.instance.StopReloading();
                weapons[currWeaponIndex].currMagazineIndex = 0;
                InGameUIManager.instance.UpdateMagazineInfo(weapons[currWeaponIndex].currMagazineIndex, weapons[currWeaponIndex].capacityMagazine);
                isReloading = false;
            }
           
        }
    }
}
