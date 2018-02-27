using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioControl : MonoBehaviour {

    #region variables

    //biological factors (for the animal)
    //[Tooltip("Factors that affect the specific animals")]
    [Header("Biological Factors")]

    //Setup values
    [Header("Setup values")]
    [Tooltip("Please don't put values less then 0")]
    public Vector2 lifeSpanRange;
    [Tooltip("Please don't put values less then 0")]
    public Vector2 speedRange; 
    [Tooltip("Please don't put values less then 0")]
    public Vector2 healthRange;

    public float hungerCapacity = 100;

    //life values (used in updates)
    public float age = 0;
    public float deathAge = 2;
    public float currentHunger;
    public float movementSpeed;
    private float standardMoveSpeed;
    public float health;
    public float lifeSpan;
    public bool canReproduce = false;
    public float reproductionTime = 0;

    #region Not in Inspector

    //p.s these variable will be same for all animals so they are private
    private int hungerDecreaseTime = 5;
    private float agingTime = 40;
    //for speed decrease(they are variables because it is more performance effective as it wont create these variables everytime in Update)
    private int speedDecreasePercentMin = 20;
    private int speedDecreasePercentMax = 10;
    private float RateOfSpeedDecrease = 0.1f;
    private int difference; 


    #endregion



    #endregion
    private void Start()
    {
        //Setup the life values
        movementSpeed = Mathf.Round(Random.Range(speedRange.x, speedRange.y) *10f)/10f;
        health = Mathf.Round(Random.Range(healthRange.x, healthRange.y));
        lifeSpan = Mathf.Round(Random.Range(lifeSpanRange.x, lifeSpanRange.y) * 10f) / 10f;
        currentHunger = hungerCapacity;
        standardMoveSpeed = movementSpeed;
        difference = speedDecreasePercentMin - speedDecreasePercentMax;
        //start hungerDecrease and Aging
        StartCoroutine(HungerDecrease());
        StartCoroutine(Aging(agingTime));


    }


    private void Update()
    {

        //check if animal can stay alive
        if (currentHunger <= 0 || age >= deathAge)
        {
            Destroy(gameObject);
        }
        SpeedDecrease(speedDecreasePercentMin,speedDecreasePercentMax,RateOfSpeedDecrease);
        currentHunger = Mathf.Clamp(currentHunger, 0, hungerCapacity);
    }

    #region aging and hunger coroutines

    IEnumerator HungerDecrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungerDecreaseTime);
            currentHunger -= 1;
        }
    }

    IEnumerator Aging(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            age += 0.1f;
        }
    }
    #endregion
    
    private void SpeedDecrease(int start,int end,float rate)
    {
        
        float currentHungerPercentage = currentHunger / (hungerCapacity / 100); // 1
        
        movementSpeed = standardMoveSpeed - (start - (Mathf.Clamp(currentHungerPercentage, end, start) - difference + end)) * rate;
       

    }
}
