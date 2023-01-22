using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Animator transitionAnimator;
    int beginHashID;

    WaitForSeconds waitForOneSec = new WaitForSeconds(1);
    WaitForSeconds waitForHalfSec = new WaitForSeconds(0.5f);

    void Start()
    {
        beginHashID = Animator.StringToHash("Begin");
    }

    public void PlayTransition()
    {
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
        transitionAnimator.SetTrigger(beginHashID);

        yield return waitForHalfSec;

        transitionAnimator.ResetTrigger(beginHashID);

        StopCoroutine(LoadNext());
    }
}
