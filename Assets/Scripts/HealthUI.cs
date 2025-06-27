using System;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Transform barRedTransform;

    private HealthSystem playerHealthSystem;

    private void Start()
    {
        playerHealthSystem = GameManager.Instance.GetPlayerTransform().GetComponent<HealthSystem>();

        playerHealthSystem.OnHealthAmountChanged += (object sender, EventArgs e) =>
        {
            barRedTransform.localScale = new Vector3(playerHealthSystem.GetHealthAmountNormalized(), 1f, 1f);
        };
    }
}
