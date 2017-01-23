using UnityEngine;
using System.Collections;
using System;

public class WildGooseFly : MonoBehaviour {

    [SerializeField]
    private Sprite[] WildGooses;
    [SerializeField]
    private float AnimTime = /*Time.deltaTime * 20*/0.4f;

    private bool isFly = true;
    private int count = 0;

    // Use this for initialization
    void Start () {
        StartCoroutine(Fly());
	}

    private IEnumerator Fly()
    {
        while (isFly)
        {
            this.GetComponent<SpriteRenderer>().sprite = WildGooses[count];
            if (count == 0)
            {
                ++count;
            }
            else
            {
                --count;
            }
            yield return new WaitForSeconds(AnimTime);
        }
    }
    void Destroy()
    {
        isFly = false;
        StopCoroutine(Fly());
    }
}
