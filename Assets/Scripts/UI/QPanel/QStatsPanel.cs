using UnityEngine;
using TMPro;
using System;

public class QStatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _accuracyText;
    [SerializeField] private TextMeshProUGUI _resultTimeText;

    private float _currentAccuracyAmount;

    private void OnEnable()
    {
        _currentAccuracyAmount = 0;
        _resultTimeText.text = GameStats.timeRound;
    }

    private void Update()
    {
        if(_currentAccuracyAmount < GameStats.accuracyRound)
        {
            _currentAccuracyAmount += 100 * Time.deltaTime;
            _accuracyText.text = Convert.ToInt16(_currentAccuracyAmount).ToString() + "%";
        }
    }
}
