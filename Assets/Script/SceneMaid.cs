using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaid : MonoBehaviour {
	public void ChangeScene(string sceneName){
		//StartCoroutine(TransitionManager.instance.useCutOut());
		SoundManager.instance.StopBGM();
		StartCoroutine(DelayLoad(sceneName));
	}
	public void ChangeSceneFast(string sceneName){
		Debug.Log("loading "+ sceneName);
		SceneManager.LoadScene(sceneName);
	}
	IEnumerator DelayLoad(string sceneName){
		yield return new WaitForSeconds(1.33333f);
		SceneManager.LoadScene(sceneName);
	}
}