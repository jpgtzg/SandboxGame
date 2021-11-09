using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] List<Transform> weaponsList = new List<Transform>();

    [SerializeField] int weaponSelected = 0;

    void Start()
    {
        GetWeaponList();
        SelectWeapon();
    }

    void Update()
    {
        changeWeapon();
    }

    void changeWeapon()
    {
        int previousSelectedWeapon = weaponSelected;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponSelected >= weaponsList.Count - 1)
            {
                weaponSelected = 0;
            }
            else
                weaponSelected++;
                
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponSelected <=  0)
            {
                weaponSelected = weaponsList.Count - 1;
            }
            else
                weaponSelected--;
        }

        if(previousSelectedWeapon != weaponSelected)
        {
            SelectWeapon();
        }
    }

    private void GetWeaponList()
    {
        foreach (Transform weapon in transform)
        {
            weaponsList.Add(weapon);
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (var weapon in weaponsList)
        {
            if (i == weaponSelected)
                weapon.transform.gameObject.SetActive(true);
            else
                weapon.transform.gameObject.SetActive(false);
            i++;
        }
    }
}
