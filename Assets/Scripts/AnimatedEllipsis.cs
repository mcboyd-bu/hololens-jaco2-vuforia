using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEllipsis : MonoBehaviour {

    private TextMesh _textMesh;
    private string _text;
    private int _counter;
    private int _frame;

	// Use this for initialization
	void Start () {
		_textMesh = GameObject.Find("LoadingArm").GetComponent<TextMesh>();
        _text = _textMesh.text;
        _counter = 0;
        _frame = 1;
    }
	
	// Update is called once per frame
	void Update () {
		if (_frame < 119)
        {
            if (_frame%30 == 0)
                _textMesh.text += ".";
            _frame += 1;
        }
        else
        {
            _textMesh.text = _text;
            _frame = 1;
        }
	}
}
