using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInputController : MonoBehaviour
{
    public GameController _gameController;

    public void Press()
    {
        _gameController.StartGame();
    }
}
