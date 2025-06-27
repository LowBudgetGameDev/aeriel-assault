using System;
using TMPro;
using UnityEngine;

public class LeaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaveText;

    private bool isOutsideMap;

    private void Start()
    {
        LeaveManager.Instance.OnLeaveMap += (object sender, EventArgs e) =>
        {
            Show();
        };

        LeaveManager.Instance.OnEnterMap += (object sender, EventArgs e) =>
        {
            Hide();
        };

        Hide();
    }

    private void Update()
    {
        if (!isOutsideMap) return;

        SetLeaveText(LeaveManager.Instance.GetLeaveTimer());
    }

    private void Show()
    {
        gameObject.SetActive(true);
        SetLeaveText(LeaveManager.Instance.GetLeaveTimer());
        isOutsideMap = true;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        isOutsideMap = false;
    }

    private void SetLeaveText(float time)
    {
        leaveText.text = "Return to Play\n" + time.ToString("F1");
    }
}
