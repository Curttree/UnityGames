using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour, IPointerClickHandler
{

    public GameObject hanks;
    public AudioSource tomTalk;
    public int sceneToLoad = 1;
    public void OnPointerClick(PointerEventData eventData)
    {
        tomTalk.Play();
        hanks.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(sceneToLoad);

    }
}
