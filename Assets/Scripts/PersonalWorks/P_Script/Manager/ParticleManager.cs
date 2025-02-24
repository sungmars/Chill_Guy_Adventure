
using System;
using System.Collections.Generic;
using UnityEngine;





[Serializable]
public class StringIntDictionaryEntry
{
    public string key;
    public int value;
}

public class ParticleManager : MonoSingleton<ParticleManager>
{
    [SerializeField] private ParticleSystem impactParticleSystem;
    public List<StringIntDictionaryEntry> dictionaryList = new List<StringIntDictionaryEntry>();

    private Dictionary<string, int> dictionary = new Dictionary<string, int>();


    public void CreateImpactParticlesAtPostion(Vector3 position)
    {
        impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(5)));
        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = 10f;
        impactParticleSystem.Play();
    }
}