using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 噴血效果 : MonoBehaviour
{
    public static 噴血效果 instance = null;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ParticleSystem 粒子 = null;
    public void 噴血()
    {
        粒子.Play();
    }
}
