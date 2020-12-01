using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour{

    [SerializeField] private GameObject skeleton;
    [SerializeField] private Collision collision;
    public TMPro.TMP_Text t_msg;

    private float delay = 1.5f;
    private static float speed = 10.0f;
    private static byte stage = 0;
    private static bool gameOver = false;

    private void Start(){

        StartCoroutine(Spawn());
    }

    private void Update(){

        if (gameOver) {

            t_msg.text = "Game over!\nTap to restart";
        }

        if (!gameOver) {

            if (collision.GetScore() / 100 > stage) {

                if (stage < 20){

                    stage++;
                }

                if (delay > 0.2f){

                    delay -= (float)stage / 10;
                }

            }
        }
  
    }

    IEnumerator Spawn(){

        while (!gameOver) {

            Instantiate(skeleton, new Vector2(Random.Range(-5.0f, 5.0f), 11.0f), Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }

    }

    public static float GetGravity(){

        return speed * (1.0f + (float)stage / 10);
    }

    public static void SetGameOverState(bool state){

        gameOver = state;
    }

    public static bool isGameOver() {

        return gameOver;
    }

 
}
