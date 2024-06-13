using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoryCinematic : MonoBehaviour
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

        text.text = "Ainsi son Choix est fait.";

        yield return new WaitForSeconds(8);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "L'Être des Prophéties a renversé la table d'Arcana";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);
        
        text.text = "Il a brisé les lois fondatrices de son monde d'accueil";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);
        
        text.text = "Les liens entre Arcanes et Etoiles sont rompus à jamais";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);
        text.text = "C'est la mort d'un Monde mais la naissance d'un peuple éphémère et libéré, les Etoiles.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Un royaume capable de prendre son Destin en Main, c'est le Nouvel Age d'Arcana";

        yield return new WaitForSeconds(4);
        
        text.text = "";
        
        SceneManager.LoadScene(indexSceneToLoad);
    }

}
