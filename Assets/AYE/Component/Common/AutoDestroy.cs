using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float cd = 3f;
    private void Start()
    {
        Destroy(this.gameObject, cd);
    }
}
