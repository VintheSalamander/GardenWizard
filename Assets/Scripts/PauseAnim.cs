using UnityEngine;

public class PauseAnim : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            if (animator.speed != 0){
                PauseAnimation();
            }else{
                ResumeAnimation();
            }
        }
    }

    void PauseAnimation(){
        animator.speed = 0; 
    }

    void ResumeAnimation(){
        animator.speed = 1;
    }
}
