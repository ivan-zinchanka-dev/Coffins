using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour{

    public static SceneManager Instance;
    public static bool FirstGame = true;
    [SerializeField] private FloatingObject[] skeletons;
    [SerializeField] private FloatingObject bomb;
    [SerializeField] private Collision collisions;
    [SerializeField] private TMPro.TMP_Text msg;

    ObjectPool BombsPool;
    ObjectPool SkeletonsLeftPool;
    ObjectPool SkeletonsRightPool;

    private float delay = 1.5f;
    private const float minDelay = 0.5f;
    private float speed = 10.0f;
    private byte stage = 1;
    private bool viewMsg = false;
    private const float EcranUpperBorder = 11.0f;

    private bool gameOver;

    public bool GameOver {

        get {
            
            return gameOver;
        }

        set {

            if (value == true)
            {
                msg.text = "Game over!\nTap to restart";
            }
            else {

                msg.text = "";
            }
            
            gameOver = value;
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Создано несколько объектов SceneManager!");
        }

        Instance = this;
    }

    private void Start(){

        BombsPool = new ObjectPool(3, bomb);
        SkeletonsLeftPool = new ObjectPool(2, skeletons[0]);
        SkeletonsRightPool = new ObjectPool(2, skeletons[1]);

        gameOver = false;

        if (FirstGame)
        {
            msg.text = "Tap to start";
            return;
        }

        StartCoroutine(Spawn());
    }

    private void Update(){

        if (gameOver || FirstGame) {

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

            choise = (byte) Random.Range(0, 2);

            if (choise == 0)
            {
                FloatingObject bomb_clone = BombsPool.GetObject() as FloatingObject;
                bomb_clone.GetComponent<PrefsPhysics>().Restart();
                bomb_clone.transform.position = new Vector2(Random.Range(-5, 6), EcranUpperBorder);             
            }
            else
            {

                choise = (byte)Random.Range(0, 2);

                if (choise == 0)
                {
                    FloatingObject skeleton_clone = SkeletonsLeftPool.GetObject() as FloatingObject;
                    skeleton_clone.GetComponent<PrefsPhysics>().Restart();
                    skeleton_clone.transform.position = new Vector2(Random.Range(-5, 6), EcranUpperBorder);
                }
                else {

                    FloatingObject skeleton_clone = SkeletonsRightPool.GetObject() as FloatingObject;
                    skeleton_clone.GetComponent<PrefsPhysics>().Restart();
                    skeleton_clone.transform.position = new Vector2(Random.Range(-5, 6), EcranUpperBorder);
                }             
                
            }
            
            yield return new WaitForSeconds(delay);
        }
    }

    public float GetGravity(){

        return speed * (1.0f + (float)stage / 10);
    }

}
