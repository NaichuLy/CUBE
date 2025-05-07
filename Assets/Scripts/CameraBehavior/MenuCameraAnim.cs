using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraAnim : MonoBehaviour
{
    Animator anim;
    [SerializeField]bool toGuide = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GoingToGuide()
    {
        toGuide = true;
        anim.SetBool("toGuide", toGuide);
    }

    public void GoingToMenu()
    {
        toGuide = false;
        anim.SetBool("toGuide", toGuide);
    }
}
