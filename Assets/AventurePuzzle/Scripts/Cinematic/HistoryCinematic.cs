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
        StartCoroutine(CameraMoving());
        StartCoroutine(Text());
    }

    /*private void Update()
    {
        if(startTraveling)
        {
            startTraveling = false;
            StartCoroutine(CameraMoving());
            StartCoroutine(Text());
        }
    }*/

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

        text.text = "Les Cartes.";

        yield return new WaitForSeconds(5);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "De leur force infinie les Arcanes ont créé Arcana, un monde sur lequel régner";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Pour remplir ce monde ils l'ont peuplés d'Etoiles pour asseoir leur volonté...";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Il est dit que de cet équilibre instable va naître un être...Différent ";

        yield return new WaitForSeconds(4);
        
        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Un Être dont la nature à été annoncée comme symbole du Renouveau d'Arcana...";
        
        yield return new WaitForSeconds(4);
        
        text.text = "";
        
        yield return new WaitForSeconds(.5f);
        
        text.text = "Dernier Arcane que la Création appelera, cet engeance nouvelle aura le pouvoir de faire le lien entre les Divins Arcanes et les Mortelles Etoiles.";

        yield return new WaitForSeconds(4);

        text.text = "";

        yield return new WaitForSeconds(.5f);

        text.text = "Tenant dans ses mains le Destin d'Arcana";

        yield return new WaitForSeconds(4);

        text.text = "";

        SceneManager.LoadScene(indexSceneToLoad);
    }

}
