using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelModule : MonoBehaviour
{

    public int numberOfRow=1;
    public int numberOfColumn=1;
    public int numberOfDepth=1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScale() {
        transform.localScale = new Vector3(numberOfColumn, numberOfDepth, numberOfRow);
    }
}


[CustomEditor(typeof(LevelModule))]
public class LevelModuleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelModule myScript = (LevelModule)target;
        if (GUILayout.Button("Update Scale"))
        {
            myScript.updateScale();
        }
    }
}