using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("Rodas");
        anim.Play("Bracos",1); // Layer 1
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
