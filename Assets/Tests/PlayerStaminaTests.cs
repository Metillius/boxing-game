using NUnit.Framework;
using UnityEngine;

public class PlayerStaminaTests
{
    private PlayerStamina stamina;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject();
        stamina = go.AddComponent<PlayerStamina>();
        stamina.maxStamina = 100f;
        stamina.minMaxStamina = 10f;
        stamina.punchCost = 2f;
        stamina.jumpCost = 6f;
        stamina.damageStaminaCost = 10f;
        stamina.damageMaxStaminaReduction = 6f;
        stamina._currentStamina = 100f;
        stamina._currentMaxStamina = 100f;        
    }

    [Test]
    public void Stamina_StartsAt_MaxStamina()
    {
        Assert.AreEqual(stamina.maxStamina, stamina.CurrentStamina);
    }

    [Test]
    public void UsePunchStamina_Reduces_CurrentStamina()
    {
        float before = stamina.CurrentStamina;
        stamina.UsePunchStamina();
        Assert.Less(stamina.CurrentStamina, before);
    }

    [Test]
    public void Stamina_Cannot_Go_Below_Zero()
    {
        for (int i = 0; i < 1000; i++)
            stamina.UsePunchStamina();

        Assert.GreaterOrEqual(stamina.CurrentStamina, 0f);
    }

    [Test]
    public void ReceiveDamage_Reduces_MaxStamina()
    {
        float before = stamina.CurrentMaxStamina;
        stamina.ReceiveDamageStamina();
        Assert.Less(stamina.CurrentMaxStamina, before);
    }

    [Test]
    public void MaxStamina_Cannot_Go_Below_MinMaxStamina()
    {
        for (int i = 0; i < 1000; i++)
            stamina.ReceiveDamageStamina();

        Assert.GreaterOrEqual(stamina.CurrentMaxStamina, stamina.minMaxStamina);
    }

    [Test]
    public void CurrentStamina_Never_Exceeds_MaxStamina()
    {
        stamina.ReceiveDamageStamina();
        Assert.LessOrEqual(stamina.CurrentStamina, stamina.CurrentMaxStamina);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(stamina.gameObject);
    }
}