using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    public GameObject parent;

    void Update()
    {
        gameObject.transform.position = parent.transform.position;
    }
}
