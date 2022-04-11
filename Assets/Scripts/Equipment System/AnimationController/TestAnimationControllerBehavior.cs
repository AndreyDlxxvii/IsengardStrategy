using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipSystem;
using TMPro;
using System;


public class TestAnimationControllerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject CharacterPrefab;
    [SerializeField]
    private Animator CharacterAnimator;        
    [SerializeField]
    private EquipSystemBehaviour _characterEQSysBehavior;
    [SerializeField]
    private GameObject MageAnimUI;
    [SerializeField]
    private TMP_Text actualLayerTxt;

    

    private int MeleeWeaponLayerID, ArcherLayerID, MageLayerID, WorkerLayerID, SpearmanLayerID;

    private bool _inCombat=false, _walkFlag=false, _runFlag=false, _chargeFlag=false, _castLoad=false, _areYouDeath=false,_resursesFlag=false,_bagFlag=false;
    

    public void Awake()
    {
        CharacterPrefab = gameObject;
        CharacterAnimator = CharacterPrefab.GetComponent<Animator>();
        _characterEQSysBehavior= CharacterPrefab.GetComponent<EquipSystemBehaviour>();

        MeleeWeaponLayerID = CharacterAnimator.GetLayerIndex("MeleeWariorLayer");
        ArcherLayerID = CharacterAnimator.GetLayerIndex("ArcherLayer");
        MageLayerID = CharacterAnimator.GetLayerIndex("MageLayer");
        WorkerLayerID = CharacterAnimator.GetLayerIndex("WorkerLayer");
        SpearmanLayerID = CharacterAnimator.GetLayerIndex("SpearmanLayer");

    }

    public void SetActiveLayer(int idLayer)
    {
        actualLayerTxt.text = CharacterAnimator.GetLayerName(idLayer);
        for (int i = 0; i < CharacterAnimator.layerCount; i++)
        {
            if (idLayer== i)
            {
                CharacterAnimator.SetLayerWeight(i, 1);                
            }
            else
            {
                CharacterAnimator.SetLayerWeight(i, 0);
            }
        }
    }        
    
    public void SetBoolAnimatorParametresValue()
    {
        CharacterAnimator.SetBool(0, _walkFlag);
        CharacterAnimator.SetBool(1, _runFlag);
        CharacterAnimator.SetBool(2, _chargeFlag);
        CharacterAnimator.SetBool(3, _inCombat);
        CharacterAnimator.SetBool(4, _castLoad);
    }
    public void CheckStateCharacter()
    {
        EquipType tempEQType = _characterEQSysBehavior.CheckPlayerState();
        MageAnimUI.SetActive(false);
        switch (tempEQType)
        {
            case EquipType.RightHandMeleeWeapon:
                SetActiveLayer(MeleeWeaponLayerID);                
                break;
            case EquipType.LeftHandMeleeWeapon:
                SetActiveLayer(MeleeWeaponLayerID);
                break;
            case EquipType.LeftHandShield:
                SetActiveLayer(MeleeWeaponLayerID);
                break;
            case EquipType.Bow:
                SetActiveLayer(ArcherLayerID);
                break;
            case EquipType.Spear:
                SetActiveLayer(SpearmanLayerID);
                break;
            case EquipType.MageStaff:
                SetActiveLayer(MageLayerID);
                MageAnimUI.SetActive(true);
                break;
            case EquipType.WorkerWeapon:
                SetActiveLayer(WorkerLayerID);
                break;
        }
    }

    #region Sharing Animator Parametrs Value
    public void InCombat()
    {
        _inCombat = !_inCombat;
        CharacterAnimator.SetBool("InCombat", _inCombat);
    }
    public void HeIsRun()
    {
        _runFlag = !_runFlag;
        CharacterAnimator.SetBool("RunFlag", _runFlag);
    }
    public void HeIsWalk()
    {
        _walkFlag = !_walkFlag;
        CharacterAnimator.SetBool("WalkFlag", _walkFlag);        
    }
    public void HeIsCharge()
    {
        _chargeFlag = !_chargeFlag;
        CharacterAnimator.SetBool("ChargeFlag", _chargeFlag);
    }
    public void HeIsCastLoad()
    {
        _castLoad = !_castLoad;
        CharacterAnimator.SetBool("CastLoad", _castLoad);
    }
    public void AreYouDeath()
    {
        _areYouDeath = !_areYouDeath;
        CharacterAnimator.SetBool("AreYouDeath", _areYouDeath);
    }
    public void AreYouDeath2()
    {
        _areYouDeath = !_areYouDeath;
        CharacterAnimator.SetBool("AreYouDeath2", _areYouDeath);
    }
    public void CheckWorkerEquipment()
    {
        GetBug(_characterEQSysBehavior.CheckBagHolders());
        GetResurses(_characterEQSysBehavior.CheckResurseHolders());
    }
    public void GetBug(bool value)
    {
        _bagFlag = value;
        CharacterAnimator.SetBool("BagFlag", _bagFlag);
    }
    public void GetResurses(bool value)
    {
        _resursesFlag = value;
        CharacterAnimator.SetBool("ResursesFlag", _resursesFlag);
    }

    public void StartAttack()
    {
        CharacterAnimator.SetTrigger("Attack");
        
    }
    public void StartSecondAttack()
    {
        CharacterAnimator.SetTrigger("Attack2");
    }
    public void TakeDamage()
    {
        CharacterAnimator.SetTrigger("TakeDamage");
    }    
    public void BuffCast()
    {
        CharacterAnimator.SetTrigger("BuffCast");
    }
    public void AttackCast()
    {
        CharacterAnimator.SetTrigger("AttackCast");
    }
    #endregion
   

}
