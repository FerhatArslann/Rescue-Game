using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAbility
{
    FireExtinguish,
    CallDog,
    EmergencyPlanket
}

public class ItemAbilities
{
    private bool dogActive = false;

    public void UseAbility(ItemAbility ability) {        
        switch (ability)
        {
            case ItemAbility.FireExtinguish:
                FireExtinguish();
                break;
            case ItemAbility.EmergencyPlanket:
                EmergencyPlanket();
                break;
            case ItemAbility.CallDog:
                CallDog();
                break;
            default: break;
        }
    }

    private void FireExtinguish()
    {
        Debug.Log("Fire Extinguisher Used");
    }

    private void EmergencyPlanket()
    {
        Debug.Log("Emergency Planket Used");
    }

    private void CallDog()
    {
        if (!dogActive)
        {
            var pos  = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            GameObject dog = Resources.Load<GameObject>("Prefab/Dog");
            dog = GameObject.Instantiate(dog, new Vector3(pos.x + 2.5f, pos.y, 0), Quaternion.identity);
            dog.name = "Dog";
            dogActive = true;
        }
        else
        {
            dogActive = false;
            GameObject dog = GameObject.Find("Dog");
            Object.Destroy(dog);
        }
    }
}
