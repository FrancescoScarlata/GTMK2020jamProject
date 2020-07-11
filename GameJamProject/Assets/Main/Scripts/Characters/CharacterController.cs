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

    [Space(10)]

    public float movSpeed=2;
    public Animator anim;
    public Rigidbody2D rigidBody2D;
    public _Weapon[] weapons;
    public bool isReloading=false;
    public bool isKnocked = false;
    [Space(10)]

    public Transform gunSpawnProjectile;
    public Transform shotgunSpawnProjectile;
    public Transform rocketSpawnProjectile;


    public int currWeaponIndex=-1;

    float recoilVector;

    int currIndexInMagazine;
    protected bool isAlive=true;
    protected bool isDoingSomeSpecialRecoil;

    protected Coroutine shootCoroutine;
    protected WaitForSeconds waitAttackSpeed;
    protected WaitForSeconds waitReloadTime;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeWeapon(currWeaponIndex);
        shootCoroutine=StartCoroutine(Shoot());
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
        }
       

        #endregion

        if (!isDoingSomeSpecialRecoil)
        {
            // check the shoot -> the recoil effects is in the shoot
            //character goes everytime in front of him
            rigidBody2D.velocity = (transform.up * movSpeed);
        }
    }


    public void SetIsDoindRecoil(bool isItDoingIt)
    {
        isDoingSomeSpecialRecoil = isItDoingIt;
    }

    public void GetDamage(float dmg)
    {
        HPs -= dmg;
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
            recoilVector = weapons[currWeaponIndex].distanceRecoil;
             StartCoroutine( weapons[currWeaponIndex].RecoilEffect(this) );
            weapons[currWeaponIndex].currMagazineIndex++;
            // shoot animation
            // shoot audio
            yield return waitAttackSpeed;
            if (currWeaponIndex >=0 && weapons[currWeaponIndex].currMagazineIndex >= weapons[currWeaponIndex].capacityMagazine)
            {
                isReloading = true;
                weapons[currWeaponIndex].currMagazineIndex = 0;
                yield return waitReloadTime;
                isReloading = false;
            }
           
        }
    }
}
