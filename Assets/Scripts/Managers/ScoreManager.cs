using UnityEngine;
using Signals;
using Data.ValueObjects;
public class ScoreManager : MonoBehaviour
{
    private ScoreData _scoreData;

    private int _paymentAmount = -10;

    private int _earningAmount = 10;

    private bool isStop { get; set; }

    private void OnGetData()
    {
        _scoreData = InitializeDataSignals.Instance.onLoadGameScore.Invoke();
        SetGameScore();
    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        LevelSignals.Instance.onLevelInitialize += OnGetData;
        CoreGameSignals.Instance.onHasEnoughMoney += OnHasMoney;
        CoreGameSignals.Instance.onHasEnoughGem += OnHasGem;
        CoreGameSignals.Instance.onUpdateGemScore += OnUpdateGemScore;
        CoreGameSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
        CoreGameSignals.Instance.onStartMoneyPayment += OnStartMoneyPayment;
        CoreGameSignals.Instance.onStopMoneyPayment += OnStopMoneyPayment;
    }
    private void UnsubscribeEvents()
    {
        LevelSignals.Instance.onLevelInitialize -= OnGetData;
        CoreGameSignals.Instance.onHasEnoughMoney -= OnHasMoney;
        CoreGameSignals.Instance.onHasEnoughGem -= OnHasGem;
        CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
        CoreGameSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
        CoreGameSignals.Instance.onStartMoneyPayment -= OnStartMoneyPayment;
        CoreGameSignals.Instance.onStopMoneyPayment -= OnStopMoneyPayment;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private bool OnHasMoney() => _scoreData.TotalMoneyScore != 0 || isStop;
    private bool OnHasGem() => _scoreData.TotalGemScore != 0;
    private void SetGameScore()
    {
        UISignals.Instance.onUpdateMoneyScore?.Invoke(_scoreData.TotalMoneyScore);
        UISignals.Instance.onUpdateGemScore?.Invoke(_scoreData.TotalGemScore);
    }

    private void OnStartMoneyPayment()
    {
        UpdateMoneyScore(_paymentAmount);
    }

    private void OnStopMoneyPayment()
    {
        isStop = true;
    }

    private void UpdateGemScore(int _amount)
    {
        OnUpdateGemScore(_amount);
    }

    private void UpdateMoneyScore(int _amount)
    {
        OnUpdateMoneyScore(_amount);
    }

    private void OnUpdateMoneyScore(int _amount)
    {
        _scoreData.TotalMoneyScore += _amount;
        UISignals.Instance.onUpdateMoneyScore?.Invoke(_scoreData.TotalMoneyScore);
        UpdateGameScoreData();
    }

    private void OnUpdateGemScore(int _amount)
    {
        _scoreData.TotalGemScore += _amount;
        UISignals.Instance.onUpdateGemScore?.Invoke(_scoreData.TotalGemScore);
        UpdateGameScoreData();
    }

    private void UpdateGameScoreData() => InitializeDataSignals.Instance.onSaveGameScore?.Invoke(_scoreData);
}
