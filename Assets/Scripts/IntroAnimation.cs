using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    public GameObject orc;
    public GameObject devil;
    public GameObject player;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        orc.GetComponent<Animator>().SetBool("walking", true);
        devil.GetComponent<Animator>().SetBool("walking", true);    
        player.GetComponent<Animator>().SetBool("PlayerRun", true);
        boss.GetComponent<Animator>().SetBool("walking", true);
    }

    // Update is called once per frame
    void Update()
    {
        orc.transform.position = new Vector3(orc.transform.position.x -  Time.deltaTime*2 ,-1.5f,0f) ;
        devil.transform.position = new Vector3(devil.transform.position.x - Time.deltaTime*2, -1.5f, 0f);
        boss.transform.position = new Vector3(devil.transform.position.x - Time.deltaTime * 2, -1.5f, 0f);
    }
}
