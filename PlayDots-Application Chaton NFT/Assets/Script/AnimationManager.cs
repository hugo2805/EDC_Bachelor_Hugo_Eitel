using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class AnimationManager : MonoBehaviour
{
    public void OpenGalerie()
    {
        gameObject.GetComponent<Animation>().Play("OpenGalerie");
    }
    
    public void CloseGalerie()
    {
        gameObject.GetComponent<Animation>().Play("CloseGalerie");
    }
}
