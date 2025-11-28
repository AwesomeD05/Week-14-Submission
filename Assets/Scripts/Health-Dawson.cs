using System.Collections;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       StartCoroutine(SelfDestruct()); 
    }

    IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(5f);
            Destroy(this.gameObject);
        }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().GainALife();
            gameManager.PlaySound(3);
            Destroy(this.gameObject);
        }
    }
}
