using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        startPos = cam.position;
    }

    private void Update()
    {
        if(startTraveling)
        {
            startTraveling = false;
            StartCoroutine(CameraMoving());
            StartCoroutine(Text());
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

    }

    IEnumerator Text()
    {
        yield return null;

        text.text = "Les cartes, medium qui permet de voir � travers les fils de notre destin.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Qui sont tiss�s par les 21 Arcanes majeurs cr�atrice des cartes du tarot.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "De leurs mains, les Arcanes ont cr�� le monde d�Arcana, et des �toiles filantes elles ont cr�� les habitants.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Elle contr�le le monde depuis leur palais et attendent l��toile capable de surpasser son destin, celle dont l�arriver a �t� pr�dit par les cartes : Le dernier Arcane.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Elle prendra forme � partir d�une �toile �cras�e dans le monde d�Arcana, et aura le pouvoir de lier les deux mondes qui s�pare les arcanes majeurs de leur cr�ation.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "D�cidant � elle seule du destin des Arcanes et des habitants.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(indexSceneToLoad);
    }

}
