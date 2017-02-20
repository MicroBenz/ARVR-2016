using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Leap;

using PDollarGestureRecognizer;

public class Demo : MonoBehaviour
{

    public Transform gestureOnScreenPrefab;
    public Transform cameraTrans;
    public Transform indexR3;
    public Transform indexR1;
    public Transform middleR3;
    public Transform middleR1;
    public Transform middleL3;
    public Transform plamL;
    private bool pulser;
    private bool rigidPulser;
    public Rigidbody bullet;
    public Rigidbody bomb;
    public Camera camera;
    float rotateDegree = 0;

    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect drawArea;

    private RuntimePlatform platform;
    private int vertexCount = 0;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    private double minX = 10000;
    private double minY = 10000;
    private double maxX = -10000;
    private double maxY = -10000;

    //GUI
    private string message;
    private bool recognized;
    private string newGestureName = "";

    void Start()
    {
        pulser = false;
        rigidPulser = false;

        platform = Application.platform;
        drawArea = new Rect(0, 0, Screen.width, Screen.height);

        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    void Update()
    {
        //print (/*Mathf.Cos( (float)Math.PI */ camera.transform.forward);



        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            //if (Input.GetMouseButton(0)) {
            if (pointClick())
            {
                //virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

                //virtualKeyPosition = new Vector3(Mathf.Cos(cameraTrans.rotation.y)*indexR3.position.x*700+300, indexR3.position.y*700+300);
                virtualKeyPosition = new Vector3(camera.WorldToScreenPoint(indexR3.position).x, camera.WorldToScreenPoint(indexR3.position).y);
            }
        }

        if (drawArea.Contains(virtualKeyPosition))
        {

            //if (Input.GetMouseButtonDown(0)) {
            if (pointClick() && !pulser)
            {
                pulser = true;
                if (recognized)
                {

                    recognized = false;
                    strokeId = -1;

                    points.Clear();

                    foreach (LineRenderer lineRenderer in gestureLinesRenderer)
                    {

                        lineRenderer.SetVertexCount(0);
                        Destroy(lineRenderer.gameObject);
                    }

                    gestureLinesRenderer.Clear();
                }
                
                ++strokeId;

                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                vertexCount = 0;
            }
            else if (!pointClick())
                pulser = false;

            //if (Input.GetMouseButton(0)) {
            if (pointClick())
            {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
                if (virtualKeyPosition.x > maxX)
                {
                    maxX = virtualKeyPosition.x;
                }
                if (virtualKeyPosition.x < minX)
                {
                    minX = virtualKeyPosition.x;
                }
                if (virtualKeyPosition.y > maxY)
                {
                    maxY = virtualKeyPosition.y;
                }
                if (virtualKeyPosition.y < minY)
                {
                    minY = virtualKeyPosition.y;
                }
                currentGestureLineRenderer.SetVertexCount(++vertexCount);
                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }
        }
    }

    private bool pointClick()
    {
        Vector3 indexR = indexR3.position - indexR1.position;
        Vector3 middleR = middleR3.position - middleR1.position;
        return (Vector3.Dot(indexR, middleR) <= 0);
    }

    void OnGUI()
    {

        //GUI.Box(drawArea, "Draw Area");

        GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);

        //if (GUI.Button(new Rect(Screen.width - 100, 10, 100, 30), "Recognize") || !pointClick()) {
        if (!pointClick())
        {
            recognized = true;

            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            message = gestureResult.GestureClass + " " + gestureResult.Score;

            string result = gestureResult.GestureClass;

            Transform shootedObject = cameraTrans;
            shootedObject.position = new Vector3(shootedObject.position.x, shootedObject.position.y, shootedObject.position.z);

            if (!rigidPulser)
            {
                Vector3 screen_location = new Vector3((float)((maxX + minX)/2), (float)((maxY + minY)/2),10);
                Vector3 spawn_location = camera.ScreenToWorldPoint(screen_location);
                Debug.Log(spawn_location);
                Debug.Log("minX: " + minX + " maxX: " + maxX);
                Debug.Log("minY: " + minY + " maxY: " + maxY);
                Rigidbody Instance;
                if (result == "D" || result == "P")
                {
                    Instance =
                        //Instantiate(cube, indexR3.position, shootedObject.rotation) as Rigidbody;
                        //Instantiate(cube, gestureLinesRenderer[0].gameObject.transform.position, shootedObject.rotation) as Rigidbody;
                    
                        Instantiate(bullet, spawn_location, bullet.transform.rotation) as Rigidbody;
                    //cubeInstance.velocity = new Vector3(Mathf.Sin((float)Math.PI*shootedObject.rotation.y),0,Mathf.Cos((float)Math.PI*shootedObject.rotation.y)) * froce;
                }
                /*else if (result == "circle")
                {
                    Instance =
                        Instantiate(sphere, indexR3.forward, shootedObject.rotation) as Rigidbody;
                    //sphereInstance.velocity = new Vector3(Mathf.Sin((float)Math.PI*shootedObject.rotation.y),0,Mathf.Cos((float)Math.PI*shootedObject.rotation.y)) * froce;
                }*/
                else if (result == "five point star")
                {
                    Instance =
                        Instantiate(bomb, spawn_location, bomb.transform.rotation) as Rigidbody;
                    //cylinderInstance.velocity = new Vector3(Mathf.Sin((float)Math.PI*shootedObject.rotation.y),0,Mathf.Cos((float)Math.PI*shootedObject.rotation.y)) * froce;
                }
                else
                    Instance = null;
                //Instance.velocity = camera.transform.forward * froce;
                rigidPulser = true;
                minX = 10000;
                minY = 10000;
                maxX = -10000;
                maxY = -10000;
            }

        }
        else
            rigidPulser = false;
        /*
		GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
		newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);

		if (GUI.Button(new Rect(Screen.width - 50, 150, 50, 30), "Add") && points.Count > 0 && newGestureName != "") {

			string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

			#if !UNITY_WEBPLAYER
				GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
			#endif

			trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

			newGestureName = "";
		}
		*/

    }
}
