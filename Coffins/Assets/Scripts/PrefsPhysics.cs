using UnityEngine;

public class PrefsPhysics : MonoBehaviour{

    [SerializeField] private float gravity = 9.0f;

    private void Start(){

        gravity = Generator.GetGravity();   
    }

    private void Update(){
 
        this.transform.position -= new Vector3(0, gravity * Time.deltaTime, 0);

        if (this.transform.position.y < -11.0f) {
           
            Destroy(this.gameObject);
            Generator.SetGameOverState(true);      
        }

    }
}
