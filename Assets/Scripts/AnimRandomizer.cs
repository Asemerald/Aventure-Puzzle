using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandomizer : MonoBehaviour
{
    public float maxWaitTime = 1f;
    Animation anim;
    float randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        randomOffset = Random.Range(0f, maxWaitTime);
        StartCoroutine(OffsetStart());
    }

    IEnumerator OffsetStart()
    {
        yield return new WaitForSeconds(randomOffset);
        anim.Play();
    }


}
