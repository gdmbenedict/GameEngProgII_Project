using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public Camera playercam;
    public LayerMask layerMask;

    private Ray ray;
    private Material targetMaterial;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = playercam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug Drawline for requirements
        //Debug.DrawRay(ray.origin, ray.direction, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            //debug Log for project requirements
            //Debug.Log("Object detected!\nID:\t" + hit.collider.gameObject.GetInstanceID() + "\nName:\t" + hit.collider.gameObject.name);

            if (hit.collider.GetComponent<MeshRenderer>())
            {
                targetMaterial = hit.collider.GetComponent<MeshRenderer>().material;

                if (targetMaterial.color != Color.red)
                {
                    originalColor = targetMaterial.color;
                }

                targetMaterial.color = Color.red;
            }


        }
        else
        {
            if (targetMaterial != null)
            {
                targetMaterial.color = originalColor;
            }      
        }


    }
}
