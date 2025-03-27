using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Hero : MonoBehaviour
{
    static public Hero S {get; private set;}

    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Header("Dynamic")] [Range(0,4)]
    public float shieldLevel = 1;
    [Tooltip( "This field holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGo = null;

    void Awake(){
        if(S == null){
            S = this;
        }
        else{
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis*speed*Time.deltaTime;
        pos.y += vAxis*speed*Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(vAxis*pitchMult, hAxis*rollMult,0);
    }
    
    void OnTriggerEnter(Collider other){
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //Debug.Log("Shield trigger hit by: " +go.gameObject.name);

        if(go == lastTriggerGo) return;
        lastTriggerGo = go;

        Enemy enemy = go.GetComponent<Enemy>();
        if(enemy != null){
            shieldLevel--;
            Destroy(go);
        }
        else{
            Debug.LogWarning("Shield trigger hit by non-Enemy: " +go.name);
        }
    }

}
