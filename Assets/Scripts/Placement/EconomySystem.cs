using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomySystem : MonoBehaviour
{ 
    public int money = 0;
    public int startingMoney = 100;

    private void Start()
    {
        money = startingMoney;
    }

    public void EarnMoney(int moneyEarnt)
    {
        money = money + moneyEarnt;
    }

    public int CurrentMoney()
    {
        return money;
    }
}
