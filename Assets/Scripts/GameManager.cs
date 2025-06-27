using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Paused,
        Playing,
        GameOver
    }

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;

    [SerializeField] private Transform playerTransform;

    private GameState gameState;

    private void Awake()
    {
        Instance = this;

        playerTransform.GetComponent<HealthSystem>().OnDie += (object sender, EventArgs e) =>
        {
            gameState = GameState.GameOver;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameState = gameState != GameState.Paused ? GameState.Paused : GameState.Playing;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayGame()
    {
        gameState = GameState.Playing;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public GameState GetCurrentState()
    {
        return gameState;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
