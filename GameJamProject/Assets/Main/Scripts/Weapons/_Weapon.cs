using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Generic class from which all the other weapons will take place
/// </summary>
[CreateAssetMenu(fileName= "newWeapon", menuName ="Weapons/Generic")]
public class _Weapon : ScriptableObject
{
    public float damage;
    public float range;
    public float moVspeed;
    public AudioClip attackClip;
    public AudioClip changeWeaponClip;
    public float attackSpeed;
    public int capacityMagazine;
    public float reloadTime;
    public AudioClip reloadClip;
    public GameObject projectilePrefab;
    public int currMagazineIndex = 0;

    public float distanceRecoil;
    public float durationRecoil;


    public virtual IEnumerator RecoilEffect(CharacterController controller)
    {
        yield return null;
    }

}
public enum WeaponType // will have also the same order in the array
{
    chainsaw,
    gun,
    shotgun,
    rocketLauncher,
    berserkSword
}
