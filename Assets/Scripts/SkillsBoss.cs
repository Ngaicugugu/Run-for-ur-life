using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsBoss : MonoBehaviour
{
    [SerializeField] private GameObject prefabCreep;
    [SerializeField] private GameObject prefabFireBall;
    [SerializeField] private Transform fireBallSpawnPos;
    [SerializeField] private Transform creepSpawnPos;
    [SerializeField] private float summonCooldown;

    private float lastSummonTime;
    
    // Start is called before the first frame update
    void Start()
    {
        lastSummonTime = -summonCooldown;
    }


    public void Summon()
    {
        //check thời gian chạy game có lớn hơn thời gian cool down và thời gian thực hiện summon cuối
        if (Time.time >= lastSummonTime + summonCooldown)
        {
            Instantiate(prefabCreep, creepSpawnPos);
            lastSummonTime = Time.time;
        }
    }
        
    public void FireBall()
    {
        Instantiate(prefabFireBall, fireBallSpawnPos);
    }
}
