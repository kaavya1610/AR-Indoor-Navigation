using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SNT : MonoBehaviour
{ 
     // Start is called before the first frame update
     [SerializeField]
     private TMP_Dropdown navigationTargetDropDown;
     [SerializeField]
     private List<Target> navigationTargetObjects = new List<Target>();
     private NavMeshPath path;// current calculated path
     private LineRenderer line;//linerenderer to display path
     private Vector3 targetPosition = Vector3.zero;// current target position

     private bool lineToggle = false;

     private void Start()
     {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
     }

     // Update is called once per frame
     private void Update()
     {
         if (lineToggle && targetPosition != Vector3.zero)
         {
             NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
             line.positionCount = path.corners.Length;
             line.SetPositions(path.corners);
          }
      }
      public void SetCurrentNavigationTarget(int selectedValue)
      {
      targetPosition = vector3.zero;
      string selectedText = navigationTargetDropDown.options[selectedValue].text;
      Target currentTarget = navigationTargetPbjects.Finf(x => x.Name.Equals(selectedText));
      if (currentTargget !=null)
        {
         targetPosition = currentTarget.PositionObject.transform.position;
        }
      }
      public void ToggleVisibility()
      {
      lineToggle = !lineToggle;
      line.enabled = lineToggle;
      }
 }     

