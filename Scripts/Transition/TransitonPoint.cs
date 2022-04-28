using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitonPoint : MonoBehaviour
{
   public enum TransitionType
   {
       SameScene,DifferentScene
   }
    
    [Header("Transition Info")]
    public string sceneName;
    public TransitionType transitionType;
    public TransitonDestination.DestinationTag destinationTag;
    private bool canTrans;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canTrans)
        {
            //TODO:SceneController 传送
            SceneContoller.Instance.TransitonDestination(this);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            canTrans = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canTrans = false;
    }

}
