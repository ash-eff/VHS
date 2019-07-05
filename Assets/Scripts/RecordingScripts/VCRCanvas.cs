using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// for testing only. get rid of this 

public class VCRCanvas : MonoBehaviour
{
    public TextMeshProUGUI vcrText;
    private bool isPaused = false;

    private void OnEnable()
    {
        Player.OnPressedRewind += SetVCRTextRewind;
        Player.OnPressedPlay += SetVCRTextPlay;
        Player.OnPressedPause += SetVCRTextPause;
    }

    private void OnDisable()
    {
        Player.OnPressedRewind -= SetVCRTextRewind;
        Player.OnPressedPlay -= SetVCRTextPlay;
        Player.OnPressedPause -= SetVCRTextPause;
    }

    void SetVCRTextRewind()
    {
        if (!isPaused)
        {
            vcrText.text = "REWIND";
        }
    }

    void SetVCRTextPlay()
    {
        vcrText.text = "PLAY";
    }

    void SetVCRTextPause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            vcrText.text = "PAUSE";
        }
        else
        {
            SetVCRTextPlay();
        }

    }
}
