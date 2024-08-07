using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    public static EconomySystem Instance { get; private set; }

    public int money = 0;
    public int startingMoney = 100;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        money = startingMoney;
    }

    public void EarnMoney(int moneyEarnt)
    {
        money = money + moneyEarnt;
    }

    public void SpendMoney(int moneySpent)
    {
        money = money - moneySpent;
    }

    public int CurrentMoney()
    {
        return money;
    }
}
