using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class auto_replace_corners : MonoBehaviour
{
    [Tooltip("GameObject whose scale will be used to replace corners")]
    public GameObject referenceCube;
    [Tooltip("Disable if the reference cube is parent of this gameobject. Enable elsewise")]
    public bool scaleItself = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (referenceCube != null)
        {
            Vector3 s = referenceCube.transform.localScale;
            if (scaleItself) { transform.localScale = s; }
            else { transform.localScale = Vector3.one;}
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                //transform.GetChild(i).transform. = new Vector3(1.0f / s.x, 1.0f / s.y, 1.0f / s.z);
                SetGlobalScale(transform.GetChild(i), Vector3.one);
            }
        }
        
    }

    public static void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
