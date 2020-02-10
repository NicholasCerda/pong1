using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pUpEvents : MonoBehaviour
{
    public int type;
    public GameObject multiBall,ballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        type = Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        type = Random.Range(0, 2);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(type);
        switch (type)
        {
            case 0:
                multiBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);

                Destroy(multiBall, 10.0f);
                //double
                break;
            case 1:
                Vector3 oldSize = collision.gameObject.transform.localScale;
                collision.gameObject.transform.localScale = new Vector3(.5f, .5f, .5f);
                break;
            default:
                Debug.Log("Error, not valid power up type");
                break;
        }
        transform.position = new Vector3(0, -1000, 0);
    }
}
