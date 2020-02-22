using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MessageController _messageController;
    public HealthController _healthController;
    public GameState _state;

    // Start is called before the first frame update
    void Start()
    {
        _healthController.UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        _state = GameState.Active;
        StartCoroutine("Gameplay");
    }

    IEnumerator Gameplay()
    {
        while (_state == GameState.Active )
        {
            _healthController.Decrement();
            CheckEndConditions();
            yield return new WaitForSeconds(1f);
        }
    }

    private void CheckEndConditions()
    {
        if (_healthController.Health <= 0) {
            _state = GameState.Lose;
        }
        else if (_healthController.Health >= 100) {
            _state = GameState.Win;
        }
    }
}
