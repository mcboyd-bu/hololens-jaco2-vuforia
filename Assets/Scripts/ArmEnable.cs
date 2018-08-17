using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmEnable : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnable()
    {
        //StartCoroutine(TextChanges());
    }

    public IEnumerator TextChanges()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        GameObject _textFound = GameObject.Find("ArmFound");
        TextMesh _textMesh = GameObject.Find("LoadingArm").GetComponent<TextMesh>();
        _textMesh.color = new Color(_textMesh.color.r, _textMesh.color.g, _textMesh.color.b, 1);
        while (_textMesh.color.a > 0.0f)
        {
            _textMesh.color = new Color(_textMesh.color.r, _textMesh.color.g, _textMesh.color.b, _textMesh.color.a - (Time.deltaTime / 1.1f));
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1.0f);
        _textFound.SetActive(false);
    }
}
