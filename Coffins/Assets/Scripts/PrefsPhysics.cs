using UnityEngine;

public class PrefsPhysics : MonoBehaviour {

    private float gravity = 9.0f;
    private int id;

    private void Start(){

        gravity = SceneManager.GetGravity();   
    }

    private void Update(){
 
        this.transform.position -= new Vector3(0, gravity * Time.deltaTime, 0);

        if (this.transform.position.y < -11.0f) {
                     
            if(this.gameObject.tag == "Skeleton")
            {
                SceneManager.SetGameOverState(true);
            }

            Destroy(this.gameObject);

        }

    }
}
