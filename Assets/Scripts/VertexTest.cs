using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTest : MonoBehaviour
{
    [SerializeField]
    private Mesh mesh;
    [SerializeField]
    private Vector3[] vertices;
    [SerializeField]
    private Vector3[] normals;   
    [SerializeField]
    private int centerPoint;
    [SerializeField]
    private int verticiesCount;
    [SerializeField]
    private List<GameObject> points;
    [SerializeField]
    private GameObject toBeIstantiated;
    

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
        verticiesCount = vertices.Length;
        for (int i = 0; i < vertices.Length; i++)
        {
            {
                GameObject childObject = Instantiate(toBeIstantiated, gameObject.transform.position + vertices[i],
                                                    Quaternion.LookRotation(normals[i])) as GameObject;
                // childObject.transform.position = gameObject.transform.position + vertices[i];
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
            }
        }
        
        for (int i = 0; i < points.Count; i++)
        {
            //if (i != CenterPoint)
            // {
            // }
            if (i == points.Count - 1)
            {
                points[i].GetComponent<HingeJoint>().connectedBody = points[0].GetComponent<Rigidbody>();
            }
            else
            {
                points[i].GetComponent<HingeJoint>().connectedBody = points[i + 1].GetComponent<Rigidbody>();
            }

        }
    }

    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = points[i].transform.localPosition;
        }
        mesh.vertices = vertices;
        mesh.normals = normals; 
    }

}
