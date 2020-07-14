using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreezer : MonoBehaviour
{
    // when this bool is true entire game will freeze
    // when u check inventory etc
    private bool _gameIsFreezed;
    // when this bool is true player cant use items or picks
    // it prevents from taking action when clicking on slots in
    // inventory part
    private bool _plrActionIsFreezed;

    public  bool GameIsFreezed
    {
        get { return _gameIsFreezed; }
        private set { ; }
    }
    public bool PlayerActionIsFreezed
    { 
      get { return _plrActionIsFreezed; } 
      private set { ; } 
    }

   

  

    public void FreezePlayerAction()
    {
        _plrActionIsFreezed = true;
    }
    public void UnfreezePlayerAction()
    {
        _plrActionIsFreezed = false;
    }
    public void FreezeGame()
    {
        _gameIsFreezed = true;
    }
    public void UnfreezeGame()
    {
        _gameIsFreezed = false;
    }
  

    
}
