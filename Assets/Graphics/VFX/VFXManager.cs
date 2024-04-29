using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{

    public ParticleSystem space;
    public ParticleSystem enter;
    public ParticleSystem lshift;
    public ParticleSystem rshift;

    private ParticleSystem[] psArray;
    private string[] shortcuts = new[] { "space", "return", "left shift", "right shift"};
    // Start is called before the first frame update
    void Start()
    {
        psArray = new[] { space, enter, lshift, rshift };
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0 ; i<psArray.Length ; i++)
        {
            if (Input.GetKeyDown(shortcuts[i]))
            {
                Debug.Log(shortcuts[i] + " pressed");
                psArray[i].Play();
                List<GameObject> children = GetChildren(psArray[i].gameObject);
                foreach (var go in children)
                {
                    if (go.GetComponent<Animation>() != null)
                    {
                        go.GetComponent<Animation>().Play();
                    }
                }
            }
        }
    }
    
    List<GameObject> GetChildren(GameObject parent)
    {
        List<GameObject> allChildren;
        allChildren = new List<GameObject>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            allChildren.Add(parent.transform.GetChild(i).gameObject);
            if (parent.transform.GetChild(i).childCount > 0)
            {
                foreach (GameObject go in GetChildren(parent.transform.GetChild(i).gameObject))
                {
                    allChildren.Add(go);
                }
            }
        }

        return allChildren;
    }
}
