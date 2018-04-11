using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PortraitLoader : MonoBehaviour
{
    [SerializeField] Material portraitMaterial;

    const string folderPath = "/Portraits/";

    List<GameObject> portraits = new List<GameObject>();

	// Update is called once per frame
	void Start ()
    {
        LoadPortraits();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            LoadPortraits();
    }

    private void OnDisable()
    {
        DestroyPortraits();
    }

    void LoadPortraits()
    {
        DestroyPortraits();

        string path = Application.dataPath + folderPath;

        print("Loading models from folder path " + path);

        if(Directory.Exists(path))
        {
            foreach(string directory in Directory.GetDirectories(path))
            {
                foreach (string objPath in Directory.GetFiles(directory, "*.obj"))               
                {
                    if (objPath.Contains(".meta"))
                        continue;

                    print("loading model " + objPath);

                    Mesh mesh = FastObjImporter.Instance.ImportFile(objPath);

                    GameObject portrait = new GameObject(objPath);
                    MeshRenderer mr = portrait.AddComponent<MeshRenderer>();
                    MeshFilter mf = portrait.AddComponent<MeshFilter>();
                    
                    mf.sharedMesh = mesh;
                    
                    Material mat = new Material(portraitMaterial);
                    mr.material = mat;

                    portrait.transform.parent = transform;

                    portraits.Add(portrait);
                }

                break;
            }
        }
    }

    void DestroyPortraits()
    {
        foreach (GameObject p in portraits)
            Destroy(p);

        portraits.Clear();
    }
}
