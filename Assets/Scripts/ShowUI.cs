using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
   public GameObject _showBaseButton;

   private void Start()
   {
      _showBaseButton.SetActive(false);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.name == "Player")
      {
         _showBaseButton.SetActive(true);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.name == "Player")
      {
         _showBaseButton.SetActive(false);
      }
   }
   
   
}
