using System;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class LeaveManager : MonoBehaviour
{
    public static LeaveManager Instance { get; private set; }

    public event EventHandler OnLeaveMap;
    public event EventHandler OnEnterMap;

    [SerializeField] private float maxDistance;

    private Transform playerTransform;

    private bool inMap;

    private float leaveTimerMax = 5f;
    private float leaveTimer;

    private void Awake()
    {
        Instance = this;
        inMap = true;

        playerTransform = GameManager.Instance.GetPlayerTransform();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (playerTransform.position.magnitude > maxDistance && inMap)
        {
            inMap = false;
            leaveTimer = leaveTimerMax;
            OnLeaveMap?.Invoke(this, EventArgs.Empty);
            SoundManager.Instance.PlayStoppableSound(SoundManager.Sound.Warning);
        }

        if (playerTransform.position.magnitude <= maxDistance && !inMap)
        {
            inMap = true;
            OnEnterMap?.Invoke(this, EventArgs.Empty);
            SoundManager.Instance.StopStoppableSound(SoundManager.Sound.Warning);
        }

        if (inMap) return;

        if (leaveTimer <= 0f) return;

        leaveTimer -= Time.deltaTime;

        if (leaveTimer <= 0f)
        {
            playerTransform.GetComponent<HealthSystem>().Damage(10000);
        }
    }

    public float GetLeaveTimer()
    {
        return leaveTimer;
    }
}
