using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    IntroBossController introBossController;

    IntroCameraController introCameraController;

    [SerializeField]
    GameObject tempPlayer;

    [SerializeField]
    GameObject boss;

   
    private void Awake()
    {
        introBossController = GameObject.FindObjectOfType<IntroBossController>(true);
        introCameraController = GameObject.FindObjectOfType<IntroCameraController>();
    }

    private void Start()
    {
        StartCoroutine(introCameraController.MoveCamera(tempPlayer, boss , introBossController.BossIn()));
    }    
}
