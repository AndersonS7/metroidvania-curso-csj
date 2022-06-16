using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosPlayer : MonoBehaviour
{

    private Transform player;

    public static PosPlayer instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        CheckPoint();
    }

    public void CheckPoint()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        player.position = transform.position;
    }
}
