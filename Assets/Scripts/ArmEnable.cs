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
        float countdown = 3.0f;
        //yield return new WaitForSecondsRealtime(2.5f);
        //GameObject _textFound = GameObject.Find("ArmFound");
        GameObject _LookingArm = GameObject.Find("LookingArm");
        GameObject _HelpHolder = GameObject.Find("HelpHolder");
        //_textFound.SetActive(false);
        TextMesh _tm = _LookingArm.GetComponent<TextMesh>();
        _tm.text = "Arm Found!";
        //_tm.color = new Color(0.102f, 1f, 0.0314f, 1);
        //TextMesh _textMesh = GameObject.Find("LoadingArm").GetComponent<TextMesh>();
        _tm.color = new Color(_tm.color.r, _tm.color.g, _tm.color.b, 1);
        while (countdown > 0.0f)
        {
            countdown -= Time.deltaTime;
            _tm.color = new Color(_tm.color.r, _tm.color.g, _tm.color.b, _tm.color.a - (countdown / 3.1f));
            //yield return null;
        }
        //yield return new WaitForSecondsRealtime(1.0f);
        _LookingArm.SetActive(false);
        _HelpHolder.SetActive(false);
        yield return null;
    }
}
