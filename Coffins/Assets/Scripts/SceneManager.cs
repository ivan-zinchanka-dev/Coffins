using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour{

    [SerializeField] private GameObject skeleton;
    [SerializeField] private GameObject bomb;
    [SerializeField] private Collision collisions;
    [SerializeField] private TMPro.TMP_Text msg;

    private float delay = 1.5f;

    private const float minDelay = 0.5f;

    private static float speed = 10.0f;
    private static byte stage = 1;
    private static bool gameOver = false;

    private bool viewMsg = false;

    private const float EcranUpperBorder = 11.0f;

    private void Start(){

        stage = 1;
        gameOver = false;
        StartCoroutine(Spawn());
    }

    private void Update(){

        if (gameOver) {

            if (!viewMsg)
            {
                msg.text = "Game over!\nTap to restart";
                viewMsg = true;               
            }

            return;           
        } 

        if (collisions.GetScore() / 100 > stage - 1) {

            if (stage < 20){

                stage++;
                Debug.Log("STAGE:" + stage);
            }

            if (delay > minDelay)
            {
                delay -= 0.2f;
            }

        }
        
  
    }

    private IEnumerator Spawn(){

        byte choise;

        while (!gameOver) {

            choise = (byte) Random.Range(0, 4);

            if (choise == 0)
            {
                Instantiate(bomb, new Vector2(Random.Range(-5, 6), EcranUpperBorder), Quaternion.identity);
            }
            else
            {
                Instantiate(skeleton, new Vector2(Random.Range(-5, 6), EcranUpperBorder), Quaternion.identity);
            }
            
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
