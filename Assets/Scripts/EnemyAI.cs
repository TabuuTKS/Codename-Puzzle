using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] GameObject player;
    [SerializeField] float speed;

        //private
    //Movement
    private bool CanMove = false;

    private void Update()
    {
        if (CanMove)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = this.transform.position;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanMove = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanMove = false;
        }
    }
}