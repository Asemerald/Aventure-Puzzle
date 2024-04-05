using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedbacks : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    public void Feedback()
    {
        var p = Instantiate(particle, transform.position, transform.rotation);
        p.Play();
        Destroy(p,1);
    }
}
