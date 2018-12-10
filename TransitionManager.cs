using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {
    public static TransitionManager ins = null;
    [Header("References")]
    public Image image;

    private Queue<IEnumerator> queuedTransitions = new Queue<IEnumerator>();
    private bool inTransition = false;
    
    void Awake() {
        if(ins == null) ins = this;
        else if(ins != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if(!inTransition && queuedTransitions.Count > 0) {
            StartCoroutine(queuedTransitions.Dequeue());
            inTransition = true;
        }
    }

    public void Transition(System.Action before, System.Action middle, System.Action after, float inSpeed, float outSpeed) {
        queuedTransitions.Enqueue(TransitionCoroutine(before,middle,after,inSpeed,outSpeed));
    }


    IEnumerator TransitionCoroutine(System.Action before, System.Action middle, System.Action after, float inSpeed, float outSpeed) {
        if(before != null) before();
        yield return null;
        
        while(image.color.a < 1) {
            image.color = new Color(image.color.r,image.color.g,image.color.b,Mathf.Clamp01(image.color.a + inSpeed * Time.deltaTime));
            yield return null;
        }

        if(middle != null) middle();
        yield return null;

        while(image.color.a > 0) {
            image.color = new Color(image.color.r,image.color.g,image.color.b,Mathf.Clamp01(image.color.a - outSpeed * Time.deltaTime));
            yield return null;
        }

        if(after != null) after();
        yield return null;

        inTransition = false;
    }

    public void ChangeScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
