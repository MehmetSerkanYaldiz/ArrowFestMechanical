using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//using Unity.EditorCoroutines.Editor;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif


public class Arrow : MonoBehaviour

{
    public float minX, maxX;
    public LayerMask layerMask;
    public float mesafe;

    public List<GameObject> arrows = new List<GameObject>();
    public GameObject ArrowObject;
    public Transform parent;
    TextMeshPro tm;
    [Range(0, 150)] public int arrowCount;
    int sayi;

    private bool isDecrease = false;










    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {


            GetRay();


        }

       
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "Gate")
    //    {

    //        if (other.gameObject.name == "Gate") {

    //            Debug.Log(other.gameObject.name);

    //            GameObject x = other.gameObject;
    //            x.GetComponent<MeshRenderer>().enabled = false;


    //            tm = x.GetComponentInChildren<TextMeshPro>();
    //            Debug.Log(tm.text);
    //            sayi = int.Parse(tm.text);

             

    //        }


    //    }
    //}

    IEnumerator DestroyObject(GameObject g)
    {

        yield return new WaitForEndOfFrame();
        DestroyImmediate(g);

    }

    void CreateArrow()
    {

        for (int i = arrows.Count; i < arrowCount; i++)
        {

            GameObject g = Instantiate(ArrowObject, parent);
            arrows.Add(g);
            g.transform.localPosition = Vector3.zero;

        }
        Diz();



    }
    void DestroyArrow()
    {


        for (int i = arrows.Count - 1; i >= arrowCount; i--)
        {
            GameObject g = arrows[arrows.Count - 1];
            arrows.RemoveAt(arrows.Count - 1);
           //EditorCoroutineUtility.StartCoroutine(DestroyObject(g),this) ;
          DestroyObject(g);





        }
        isDecrease = false;
        Diz();

    }

    private void OnValidate()
    {
        if (arrowCount > arrows.Count && !isDecrease)
        {

            CreateArrow();


        }
        else if (arrows.Count > arrowCount)
        {

            isDecrease = true;
            DestroyArrow();


        }
        else
        {

            Diz();
        }
    }
    void MoveObjects(Transform objTransform, float degree)
    {

        Vector3 pos = Vector3.zero;
        pos.x = Mathf.Cos(degree * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(degree * Mathf.Deg2Rad);
        objTransform.localPosition = pos * mesafe;



    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Gate" )
    //    {

    //        Debug.Log(collision.gameObject.name);

    //        GameObject x = collision.gameObject;
    //        x.GetComponent<MeshRenderer>().enabled = true;

    //    }
    //}
    void Diz()
    {
        float angle = 1f;
        float arrowCount = arrows.Count;
        angle = 360 / arrowCount;

        for (int i = 0; i < arrowCount; i++)
        {

            MoveObjects(arrows[i].transform, i * angle);
        }


    }



    void GetRay()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {

            Vector3 mouse = hit.point;
            mouse.x = Mathf.Clamp(mouse.x, minX, maxX);
            mesafe = mouse.x;




            Diz();

        }






    }


}
