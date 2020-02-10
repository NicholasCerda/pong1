using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public int counter,types,max;
    public GameObject myPrefab,pUp;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        myPrefab = GameObject.FindGameObjectWithTag("PowerUp");
        counter = 0;
        types = 3;
        type = 0;
        max = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < max)
        {
            StartCoroutine(nextSpawn());
        }

    }
    IEnumerator nextSpawn()
    {
        counter += 1;
        yield return new WaitForSeconds(0);
        //place spawn

        pUp =Instantiate(myPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 0, Random.Range(-6.0f, 6.0f)), Quaternion.identity);
        type = Random.Range(0, types);
        yield return new WaitForSeconds(5);
        Destroy(pUp);
        counter = 0;
    }
    

}
