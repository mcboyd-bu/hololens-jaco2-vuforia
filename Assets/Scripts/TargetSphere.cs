using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetSphere : MonoBehaviour {

    public static TargetSphere instance;

    public Transform spherePrefab;

    public Transform labelPrefab;

    public GameObject cursor;

    private Transform _arm;

    private Vector3 _relativePos;

    [HideInInspector]
    public Transform lastSpherePlaced;

    [HideInInspector]
    public Transform lastSphereLabelPlaced;

    [HideInInspector]
    public TextMesh lastSphereLabelPlacedText;

    private VuMarkManager vuMarkManager;

    public void Awake()
    {
        instance = this;
    }

    public void CreateSphere()
    {
        Vector3[] spherePositions = new[] { new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.2f, 0.3f, 0.1f), new Vector3(0.1f, 0.2f, 0.6f), new Vector3(-0.2f, 0.5f, 0.2f), new Vector3(0.4f, 0.2f, 0.0f), new Vector3(0.1f, 0.6f, -0.2f) };
        Quaternion sphereRotation = Quaternion.identity;
        Vector3 vectPtoS;
        //_arm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ArmPlane>().lastPlanePlaced;
        _arm = GameObject.Find("ArmPlane").transform;
        Vector3 armV = _arm.position;
        Quaternion sphereRotation2 = _arm.rotation;
        //ArmPlane.instance.CreatePlanePR(armV, sphereRotation2);
        
        // string displayText;
        for (int i = 0; i < 6; i++)
        {
            lastSpherePlaced = Instantiate(spherePrefab, armV + spherePositions[i], sphereRotation2);
            
            _relativePos = _arm.InverseTransformPoint(lastSpherePlaced.position);
            _relativePos = _relativePos * 0.0049f;

            string tagS = "Sphere" + i.ToString();
            string tagSL = "SphereLabel" + i.ToString();
            lastSpherePlaced.name = tagS;
            lastSphereLabelPlaced = Instantiate(labelPrefab, lastSpherePlaced.transform.position + (new Vector3(0, -0.05f, -0.05f)), transform.rotation);
            lastSphereLabelPlaced.name = tagSL;
            lastSphereLabelPlacedText = lastSphereLabelPlaced.GetComponent<TextMesh>();
            vectPtoS = lastSpherePlaced.transform.position - armV;
            lastSpherePlaced.GetComponent<SphereXYZ>().X = _relativePos.x; // vectPtoS.x;
            lastSpherePlaced.GetComponent<SphereXYZ>().Y = _relativePos.y; // vectPtoS.y;
            lastSpherePlaced.GetComponent<SphereXYZ>().Z = _relativePos.z; // vectPtoS.z;
            lastSphereLabelPlacedText.text = _relativePos.ToString();
        }
        // Disables Vuforia...only used for testing/video recording
        //VuforiaBehaviour.Instance.enabled = false;
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
