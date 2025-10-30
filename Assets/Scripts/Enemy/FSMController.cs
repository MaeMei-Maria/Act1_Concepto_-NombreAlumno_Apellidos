using System;
using UnityEngine;

public class FSMController : MonoBehaviour
{
   private States currentState;

   private void Update()
   {
      if (currentState)
      {
         currentState.OnUpdate();
      }
   }

   public void SetState(States newState) //Comprueba si hay un estado anterior y lo actualiza.
   {
      if (currentState)
      {
         currentState.OnExit();
      }
      
      currentState.OnEnter();
      currentState = newState;
   }
}
