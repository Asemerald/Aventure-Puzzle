using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceInteraction : MonoBehaviour
{
    [Header("Settings")]
    public Transform highestPoint;
    public Transform lowestPoint;
    public Transform plateforme1, plateforme2;
    public int weight1, weight2;
    public int previousWeight1, previousWeight2;

    [SerializeField] Transform castPos1, castPos2;
    [SerializeField] Vector3 castSize1, castSize2;

    float timeToMove = 3;

    private void Update()
    {
        CheckForWeight();

        if (previousWeight1 != weight1 || previousWeight2 != weight2)
        {
            previousWeight1 = weight1;
            previousWeight2 = weight2;

            if (weight1 > weight2)
            {
                StartCoroutine(UpdatePos(new Vector3(plateforme1.position.x, lowestPoint.position.y, plateforme1.position.z), new Vector3(plateforme2.position.x, highestPoint.position.y, plateforme2.position.z)));
                //plateforme1.position = new Vector3 (plateforme1.position.x,lowestPoint.position.y, plateforme1.position.z);
                //plateforme2.position = new Vector3 (plateforme2.position.x,highestPoint.position.y, plateforme2.position.z);
            }
            else if (weight2 > weight1)
            {
                StartCoroutine(UpdatePos(new Vector3(plateforme1.position.x, highestPoint.position.y, plateforme1.position.z), new Vector3(plateforme2.position.x, lowestPoint.position.y, plateforme2.position.z)));
                //plateforme1.position = new Vector3 (plateforme1.position.x,highestPoint.position.y, plateforme1.position.z);
                //plateforme2.position = new Vector3 (plateforme2.position.x,lowestPoint.position.y, plateforme2.position.z);

            }
            else if (weight1 == weight2)
            {
                StartCoroutine(UpdatePos(new Vector3(plateforme1.position.x, transform.position.y, plateforme1.position.z), new Vector3(plateforme2.position.x, transform.position.y, plateforme2.position.z)));
                //plateforme1.position = new Vector3(plateforme1.position.x, transform.position.y, plateforme1.position.z);
                //plateforme2.position = new Vector3(plateforme2.position.x, transform.position.y, plateforme2.position.z);
            }

            Debug.Log("Couocu");
        }
    }

    IEnumerator UpdatePos(Vector3 targetPos1, Vector3 targetPos2)
    {
        float elapsedTime = 0;

        Vector3 currentPos1 = plateforme1.position;
        Vector3 currentPos2 = plateforme2.position;

        while(elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;

            Vector3 newPos1 = Vector3.Lerp(currentPos1, targetPos1, elapsedTime / timeToMove);
            Vector3 newPos2 = Vector3.Lerp(currentPos2, targetPos2, elapsedTime / timeToMove);

            plateforme1.position = newPos1;
            plateforme2.position = newPos2;

            yield return null;
        }
    }

    void CheckForWeight()
    {
        //Check for plateforme 1
        bool weight1Set = false;
        bool weight2Set = false;

        Collider[] hits1 = Physics.OverlapBox(castPos1.localPosition + new Vector3(0, castSize1.y / 2, 0), castSize1, transform.rotation);
        if(hits1.Length > 0)
        {
            foreach(Collider hit in hits1)
            {
                if (weight1Set) break;
                if (hit.GetComponent<SizeInteraction>())
                {
                    switch(hit.GetComponent<SizeInteraction>().currentState)
                    {
                        case SizeInteraction.SizeState.Shrink:
                            weight1 = 1;
                            break;
                        case SizeInteraction.SizeState.Normal: 
                            weight1 = 5;
                            break;
                        case SizeInteraction.SizeState.Bigger: 
                            weight1 = 10;
                            break;
                    }
                    weight1Set = true;
                }
                else
                    weight1 = 0;
            }
        }


        Collider[] hits2 = Physics.OverlapBox(castPos2.localPosition + new Vector3(0, castSize2.y / 2, 0), castSize2, transform.rotation);
        if (hits2.Length > 0)
        {
            foreach (Collider hit in hits2)
            {
                if(weight2Set) break;
                if (hit.GetComponent<SizeInteraction>())
                {
                    switch (hit.GetComponent<SizeInteraction>().currentState)
                    {
                        case SizeInteraction.SizeState.Shrink:
                            weight2 = 1;
                            break;
                        case SizeInteraction.SizeState.Normal:
                            weight2 = 5;
                            break;
                        case SizeInteraction.SizeState.Bigger:
                            weight2 = 10;
                            break;
                    }
                    weight2Set = true;
                }
                else
                    weight2 = 0;
            }
        }

        Debug.Log(weight1 + " " + weight2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(castPos1.localPosition + new Vector3(0,castSize1.y / 2,0), castSize1);
        Gizmos.DrawWireCube(castPos2.localPosition + new Vector3(0, castSize2.y / 2, 0), castSize2);
    }
}
