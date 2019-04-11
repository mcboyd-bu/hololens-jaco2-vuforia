using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using UnityEngine.XR.WSA.WebCam;

public class DistanceCapture : MonoBehaviour {

    private GestureRecognizer recognizer;

    private Transform _sphere;

    public Transform sphereHighlightPrefab;

    private TextMesh _sphereLabel;

    private int tapCount = 1;

    private Color _initColor;

    private Color _hGreen = new Color(0.502f, 1f, 0f, 0.9f);  // Green color for the mesh
    private Color _hYellow = new Color(1f, 0.9f, 0f, 0.9f);  // Yellow color for the mesh
    private Color _hRed = new Color(1f, 0f, 0.01f, 0.9f);  // Red color for the mesh

    // Use this for initialization
    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += TapHandler;
        recognizer.StartCapturingGestures();
		
	}
	
    private void TapHandler(TappedEventArgs obj)
    {
        if (tapCount == 0)
        {
            // Instantiate plane and sphere
            //ArmPlane.instance.CreatePlane();
            //TargetSphere.instance.CreateSphere();
            //Vector3 vectPtoS = ArmPlane.instance.transform.position - TargetSphere.instance.transform.position;
            //string displayText = "Arm Pos: " + ArmPlane.instance.transform.position.ToString() + "\r\nSphere Pos: " + TargetSphere.instance.transform.position.ToString() + "\n\r" + vectPtoS.ToString();
            // TargetSphere.instance.lastSphereLabelPlacedText.text = (new Vector3(-0.5f, 0.5f, 0.5f)).ToString();
            // TargetSphere.instance.lastSphereLabelPlacedText.text = displayText; // (vectPtoS).ToString();
            Renderer rendi = GameObject.Find("Sphere1").GetComponent<Renderer>();
            _initColor = rendi.material.color;
        }
        else if (tapCount > 0)
        {
            //string test = GazeManager.Instance.HitObject.name;
            //_sphereLabel = GameObject.Find("SphereLabel1").GetComponent<TextMesh>();
            //_sphereLabel.text = "YES! " + test;
            //_sphereLabel.color = Color.red;
            // ResultsLabel.instance.CreateLabel();
            GameObject gazedObj = GazeManager.Instance.HitObject;
            string objName = gazedObj.name;
            StartCoroutine(SendMsgs.instance.SendMsg("object clicked: " + objName));
            if (objName.StartsWith("Sphere"))
            {
                GameObject highlightInstance = GameObject.Find("SPH");
                if (highlightInstance != null)
                    Destroy(highlightInstance);
                //string sphereTagNumber = objName.Substring(objName.Length - 1);
                //_sphereLabel = GameObject.Find("SphereLabel1").GetComponent<TextMesh>();
                //_sphereLabel.text = "YES! Tag#: " + sphereTagNumber + "\r\nX: " + GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().X.ToString();
                //_sphereLabel.color = Color.red;

                //Set all other sphere's back to default render look
                for (int i = 0; i < 6; i++)
                {
                    Renderer rendi = GameObject.Find("Sphere"+i.ToString()).GetComponent<Renderer>();
                    rendi.material.color = _initColor; // new Color(1f, 1f, 1f, 0.576f);  // Default white color for all spheres
                }

                //Fetch the Renderer from the GameObject
                Renderer rend = GazeManager.Instance.HitObject.GetComponent<Renderer>();
                rend.material.color = new Color(0.502f, 1f, 0f, 0.9f);  // Green color for the selected sphere

                Transform highlight = Instantiate(sphereHighlightPrefab, gazedObj.transform.position, gazedObj.transform.rotation);
                highlight.name = "SPH";

                float _x = GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().X;
                float _y = GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().Y;
                float _z = GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().Z;
                //Debug.Log(String.Format("About to send data, X: {0}", _x.ToString()));
                StartCoroutine(SendData.instance.SendDataToAPI(_x, _y, _z));
            }
            else if (objName.StartsWith("Cube"))
            {
                // ResultsLabel.instance.CreateLabel();
                StartCoroutine(SendMsgs.instance.SendMsg("object clicked, in Cube"));
                // First check if the "highlight" for the Cube has already been created, and if so, delete it
                //GameObject highlightInstance = GameObject.Find("CubeHighlight");
                //if (highlightInstance != null)
                //    Destroy(highlightInstance);

                //Fetch the Renderer from the GameObject
                Renderer rend = GazeManager.Instance.HitObject.GetComponent<Renderer>();
                int prob = GazeManager.Instance.HitObject.GetComponent<CubeProps>().Probability;
                if (prob < 31) rend.material.color = _hRed;
                else if (prob < 76) rend.material.color = _hYellow;
                else rend.material.color = _hGreen;
                
                StartCoroutine(SendMsgs.instance.SendMsg("in Cube, rend material color changed...."));
                //StartCoroutine(SendProbability.instance.GetProbability("cube", returnValue =>
                //{
                //    prob = returnValue;
                //}));
                StartCoroutine(SendMsgs.instance.SendMsg("returned prob from cubeprops: " + prob));
                //Transform highlight = Instantiate(sphereHighlightPrefab, gazedObj.transform.position, gazedObj.transform.rotation);
                //highlight.name = "CubeHighlight";
            }
            else
            {
                // Nothing really...
            }
        }
        else
        {
            // Nothing really...
        }
        tapCount += 1;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
