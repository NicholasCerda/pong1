using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float movex, movez,spawn;
    public Vector3 rand;
    public int lscore, rscore, win, count;
    public GameObject p1, p2,ScoreMnger,col;
    public Text LScore, RScore,lwin,rwin;
    public ScoreScript scSc;
    public BallScript otherBall;
    public AudioSource audioData;
    public string[] scores = new string[] { "nulla", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        scSc = ScoreMnger.GetComponent<ScoreScript>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
        spawn = 1.0f;
        win = 0;
        respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -20 || gameObject.transform.position.x > 20 || gameObject.transform.position.z < -20 || gameObject.transform.position.z > 20)
        {
            respawn();
        }
        if (win == 0)
        {
            count = 0;
            rigidbody.velocity = new Vector3(0, 0, 0);
            rand = new Vector3(movex, 0, movez);
            if (movez == 0)
            {
                movez = 1.0f;
            }
            if (Mathf.Abs(movez) < .5)
            {
                movez *= 1.1f;
            }
            rigidbody.velocity = rand;
        }
        else if (win == -1 || win==1)
        {
            StartCoroutine(mywait());
        }
    }
    void reset()
    {
        //reset score;
        
        scSc.Lscore = 0;
        scSc.Rscore = 0;
        LScore.text = scores[scSc.Lscore];
        RScore.text = scores[scSc.Rscore];
        respawn();
    }
    void respawn()
    {
        //spawn in center of game
        transform.position = new Vector3(0.0f,0.0f,0.0f);
        gameObject.transform.localScale= new Vector3(1, 1, 1);
        movex = Random.Range(1.0f, 10.0f)*spawn;
        movez = Random.Range(-10.0f, 10.0f);
        //ensure vertical movement
        while (movez == 0)
        {
            movez = Random.Range(-10.0f, -10.0f);
        }
        //ensure some speed to vertical movement
        while (Mathf.Abs(movez) < 1)
        {
            movez *= 1.1f;
        }
        rand = new Vector3(movex, 0, movez);
        p1.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        p2.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);

    }
    IEnumerator mywait()
    {
        if (win == -1)
        {
            win = 9;
            for (int i = 0; i < 5; i++)
            {
                lwin.transform.position = new Vector3(290, 250, 0);
                yield return new WaitForSeconds(1);
                lwin.transform.position = new Vector3(0, 0, -1000);
                yield return new WaitForSeconds(1);
            }
            lwin.transform.position = new Vector3(290, 250, 0);
            yield return new WaitForSeconds(4);
            lwin.transform.position = new Vector3(0, 0, -1000);
        }else if (win == 1)
        {
            win = 9;
            for (int i = 0; i < 5; i++)
            {
                rwin.transform.position = new Vector3(290, 250, 0);
                yield return new WaitForSeconds(1);
                rwin.transform.position = new Vector3(0, 0, -1000);
                yield return new WaitForSeconds(1);
            }

            rwin.transform.position = new Vector3(290, 250, 0);
            yield return new WaitForSeconds(4);
            rwin.transform.position = new Vector3(0, 0, -1000);
        }
        win = 0;
        reset();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //col = collision.gameObject;
            otherBall=collision.gameObject.GetComponent<BallScript>();
            //gameObject otherBall = collision.gameObject;
            //x
            if (Mathf.Abs(movex) == Mathf.Abs(otherBall.movex))
            {
                movex *= -1;//dont add for otherball since the other ball is doing the same calc.
            }else if (Mathf.Abs(movex) < Mathf.Abs(otherBall.movex))
            {
                if ((movex<0 && otherBall.movex < 0)|| (movex > 0 && otherBall.movex > 0))
                {
                    movex = movex + (2 * (otherBall.movex - movex) / 3);
                }
                else
                {
                    movex = otherBall.movex;//temp
                }
            }
            else
            {
                if ((movex < 0 && otherBall.movex < 0) || (movex > 0 && otherBall.movex > 0))
                {
                    movex = movex - (2 * (otherBall.movex - movex) / 3);
                }
                else
                {
                    movex = otherBall.movex;//temp
                }
            }
            //z
            if (Mathf.Abs(movez) == Mathf.Abs(otherBall.movez))
            {
                movez *= -1;
            }
            else if (Mathf.Abs(movex) < Mathf.Abs(otherBall.movex))
            {
                if ((movez < 0 && otherBall.movez < 0) || (movez > 0 && otherBall.movez > 0))
                {
                    movez = movez + (2 * (otherBall.movez - movez) / 3);
                }
                else
                {
                    movez = otherBall.movez;//temp
                }
            }
            else
            {
                if ((movez < 0 && otherBall.movez < 0) || (movez > 0 && otherBall.movez > 0))
                {
                    movez = movez - (2 * (otherBall.movez - movez) / 3);
                }
                else
                {
                    movez = otherBall.movez;//temp
                }
            }
            if (movex < 0 && otherBall.movex > 0)
            {
                movex *= -1.0f;
                movez *= -1.0f;
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            movez *= -1;
        }
        if (collision.gameObject.tag == "Player1")
        {
            movex = Mathf.Abs(movex)+0.5f;
            collision.gameObject.transform.localScale -= new Vector3(0.0f, 0.0f, 1.0f);
        }
        else if (collision.gameObject.tag == "Player2")
        {
            movex = -Mathf.Abs(movex)-0.5f;
            collision.gameObject.transform.localScale -= new Vector3(0.0f, 0.0f, 1.0f);
        }
        if (collision.gameObject.tag == "Goal")
        {

            //audio.Play();
            if (gameObject.transform.position.x < 0)
            {
                audioData.panStereo = -1;
                audioData.Play(0);
                scSc.Rscore += 1;
                //rscore += 1;
                Debug.Log("Player1 scored against Player2, the score is (P1)" + scSc.Lscore + " to (P2)"+ scSc.Rscore + "\n");
                spawn = -1.0f;
            }else{
                audioData.panStereo = 1;
                audioData.Play(0);
                scSc.Lscore += 1;
                Debug.Log("Player2 scored against Player1, the score is (P1)" + scSc.Lscore + " to (P2)" + scSc.Rscore + "\n");
                spawn = 1.0f;
            }
            if (scSc.Lscore == 11)
            {
                //put player1 wins text
                win = -1;
                transform.position = new Vector3(0, -1000, 0);
                Debug.Log("Game Over, Left Paddle Wins");
                return;
            }
            else if (scSc.Rscore == 11)//dont forget to add other stuff to reset them.
            {
                //put player2 wins text
                win = 1;
                transform.position = new Vector3(0, -1000, 0);
                Debug.Log("Game Over, Left Paddle Wins");
                return;
            }

            LScore.text = scores[scSc.Lscore];
            RScore.text = scores[scSc.Rscore];
            respawn();
        }
        
    }
}
