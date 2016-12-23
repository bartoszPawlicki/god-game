using System.Collections;
using UnityEngine;

public class TutorialAutoScroll : MonoBehaviour {
    private int tipsCount;
    private int currentTip = 0;
	void Start () {
        tipsCount = transform.childCount;
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(AutoNextTipCoroutine());
	}
	
    void Update()
    {
        if(Input.anyKeyDown)
        {
            NextTip();
            StopAllCoroutines();
            StartCoroutine(AutoNextTipCoroutine());
        }
    }

    private IEnumerator AutoNextTipCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);
            NextTip();
        }
    }

    private void NextTip()
    {
        transform.GetChild(currentTip).gameObject.SetActive(false);
        currentTip++;
        if (currentTip == tipsCount)
            currentTip = 0;
        transform.GetChild(currentTip).gameObject.SetActive(true);
    }
}
