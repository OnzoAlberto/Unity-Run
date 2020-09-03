using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    Animator PlayerAnimator;
    BoxCollider colliderForJump;
    CapsuleCollider colliderForSlide;
    public GameObject Placeholder1,Placeholder2, life;
    public GameObject[] obstacles;
    private int LifePoint = 3;
   


    private GameObject obj = null;

    private void Start()
    {
        PlayerAnimator = this.GetComponent<Animator>();
        colliderForJump = this.GetComponent<BoxCollider>();
        colliderForSlide = this.GetComponent<CapsuleCollider>();
    }


    private void Update()
    {
        if(UIScript.pause)
        Time.timeScale = Time.timeScale + 0.0001f;

        if ((Input.GetKey("up") || Input.GetKey("w")) && this.transform.localPosition.y < 1)
           {
            PlayerAnimator.Play("up");
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 0.1f, 4);
           }
       
        if ((Input.GetKey("down") || Input.GetKey("s")) && this.transform.localPosition.y > -1)
            {
            PlayerAnimator.Play("down");
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.1f, 4);
            }
   
        if ((Input.GetKey("left") || Input.GetKey("a")) && this.transform.localPosition.x > -1.5)
        {
            PlayerAnimator.Play("left");
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - 0.1f, this.transform.localPosition.y, 4);
            }

        if ((Input.GetKey("right") || Input.GetKey("d")) && this.transform.localPosition.x < 1.5)
        {
            PlayerAnimator.Play("right");
            this.transform.localPosition = new Vector3(this.transform.localPosition.x + 0.1f, this.transform.localPosition.y, 4);
        }

    }

    private void spawnObstacle()
    {
        Destroy(obj,.5f);

        var random = Random.Range(0, obstacles.Length);

        obj = Instantiate(obstacles[random],Placeholder1.transform,false);
    }

    private void spawnObstacle2()
    {
        Destroy(obj,.5f);

        var random = Random.Range(0, obstacles.Length);

        obj= Instantiate(obstacles[random], Placeholder2.transform, false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Obstacle"))
        {
            PlayerAnimator.Play("collision");
            if (LifePoint > 0)
            {
                LifePoint--;
                life.GetComponent<Text>().text = "Life: " + LifePoint.ToString();
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Destroy(other.transform.parent.gameObject, 2f);
            }

            if (LifePoint== 0)

            {
                Destroy(other.transform.parent.gameObject);
                UIScript.EndGame();
            }
        }

        if (other.gameObject.tag.Equals("Bonus"))
        {
           // LifePoint++;
            //life.GetComponent<Text>().text = "life: " + LifePoint.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name.Equals("Placeholder1") && (LifePoint > 0))
            spawnObstacle2();
        if (other.gameObject.name.Equals("Placeholder2") && (LifePoint > 0))
            spawnObstacle();

    }



}
