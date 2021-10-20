using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class PlayerInteraction : MonoBehaviour
{
    private int crystalCollected = 0;
    private int noOfCrystals = 0;
    public TextMeshProUGUI crystalScore;
    public GameOverScreen gameOverScreen;

    private void Start()
    {
        noOfCrystals = GameObject.FindGameObjectsWithTag("Crystal").Length;
        crystalScore.text = crystalCollected.ToString() + " / " + noOfCrystals.ToString();
    }

    private void GameOver(string txt)
    {
        gameOverScreen.Setup(txt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish" && crystalCollected == noOfCrystals)
        {
            GameOver("YOU WIN!");
        }

        if (collision.gameObject.tag == "Enemy")
        {
            GameOver("YOU DIED!");
        }

        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Crystal")
        {
            crystalCollected++;
            crystalScore.text = crystalCollected.ToString() + " / " + noOfCrystals.ToString();
            FindObjectOfType<EnemyMovement>().ChaseCrystal(
                (int)collision.gameObject.transform.position.x, (int)collision.gameObject.transform.position.y);

            if (crystalCollected == noOfCrystals) FindObjectOfType<EnemyMovement>().Crazy();

            Destroy(collision.gameObject);
        }
       
        if (collision.gameObject.tag == "Battery")
        {
            FindObjectOfType<FlashLight>().rechargeBattery();
            FindObjectOfType<FlashLight>().batteryDead = false;
            Destroy(collision.gameObject);
        }
    }
}
