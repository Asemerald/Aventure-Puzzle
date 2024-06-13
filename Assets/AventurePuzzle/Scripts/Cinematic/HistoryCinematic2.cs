using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoryCinematic2 : MonoBehaviour
{

    public Transform cam;
    public Transform endPos;
    public float timeToWalk;

    public bool startTraveling;
    Vector3 startPos;

    public TextMeshProUGUI text;

    public int indexSceneToLoad;
    private bool hasFinishedMoving;

    private void Start()
    {
        startPos = cam.position;
        StartCoroutine(CameraMoving());
        StartCoroutine(Text());
        hasFinishedMoving = false;
    }

    private void Update()
    {
        if (hasFinishedMoving)
        {
            SceneManager.LoadScene(indexSceneToLoad);
        }
    }

    IEnumerator CameraMoving()
    {
        float elapsedTime = 0;

        while (elapsedTime < timeToWalk)
        {
            elapsedTime += Time.deltaTime;

            Vector3 newPos = Vector3.Lerp(startPos, endPos.position, elapsedTime / timeToWalk);
            cam.position = newPos;

            yield return null;
        }
        hasFinishedMoving = true;

    }

    IEnumerator Text()
    {
        yield return null;

        text.text = "Ainsi son choix est fait.";

        yield return new WaitForSeconds(5);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "L'Être à décidé de conserver l'Ordre d'Arcana";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "De lueur d'espoir pour un monde nouveau, l'Être de la Prophétie est devenu l'instrument de l'ancien.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Il vint ajouter sa carte pour devenir la 22 ème Arcane";

        yield return new WaitForSeconds(4);
        
        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Cela scella donc le destin d'Arcana restant sans fin sous la coupe de ses Maîtres..";
        
        yield return new WaitForSeconds(4);
        
        text.text = "";
        
        yield return new WaitForSeconds(.5f);
        
        text.text = "Les Etoiles prient et implorent mais seul le silence du cosmos saura leur répondre..";

        yield return new WaitForSeconds(4);
        
        SceneManager.LoadScene(indexSceneToLoad);
    }

}
