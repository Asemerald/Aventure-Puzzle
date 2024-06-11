using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCards : MonoBehaviour
{
    public static RotationCards Instance { get; private set; }

    public Transform player;

    public Transform[] cards;
    public float distance;
    public float speed;
    public float timeForCardPos;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        if (PlayerController.Instance.hasAstralPocket)
            SetAngle(3);
    }

    private void Update()
    {
        RotateCard();
    }

    public void SetAngle(int cardLeft)
    {
        foreach (var card in cards)
        {
            card.gameObject.SetActive(false);
        }

        for(int i = 0; i < cardLeft; i++)
        {
            cards[i].gameObject.SetActive(true);

            //Calculate angle
            float x = Mathf.Sin(360f / cardLeft * i * Mathf.Deg2Rad) * distance;
            float z = Mathf.Cos(360f / cardLeft * i * Mathf.Deg2Rad) * distance;

            //cards[i].transform.localPosition = new Vector3(x,0,z);

            StartCoroutine(SetToPos(new Vector3(x, 0, z), cards[i].transform));
        }

        foreach(var c in cards)
            if(!c.gameObject.activeSelf)
                c.transform.localPosition = Vector3.zero;
    }

    IEnumerator SetToPos(Vector3 endPos, Transform card)
    {
        Vector3 startPos = card.localPosition;
        float elapsedTime = 0;
        while (elapsedTime < timeForCardPos)
        {
            elapsedTime += Time.deltaTime;

            Vector3 newPos = Vector3.Lerp(startPos, endPos, elapsedTime / timeForCardPos);
            card.localPosition = newPos;
            yield return null;
        }
    }

    void RotateCard()
    {
        transform.position = player.position;
        transform.rotation *= Quaternion.Euler(0, speed * Time.deltaTime, 0);
    }
}
