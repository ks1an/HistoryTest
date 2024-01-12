using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;
using Unity.Services.Analytics;

public class GameScript : MonoBehaviour
{
    public static bool CanExit = true;

    #region events
    public static event Action ActionGameStarted;
    public static event Action ActionGameEnded;
    #endregion

    [SerializeField] private CategoryList[] _category = new CategoryList[2];
    [SerializeField] private TextMeshProUGUI _qCategoryText; 
    [SerializeField] private Text _qText;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] _answerBttns = new Button[4];
    [SerializeField] private TextMeshProUGUI[] _answersText;
    [SerializeField] private Sprite _defaultAnswerBttnSprite;
    [SerializeField] private Sprite _trueAnswerBttnSprite;
    [SerializeField] private Sprite _falseAnswerBttnSprite;

    [SerializeField] private QProgressBar _qProgressBar;

    [SerializeField] private GameStats _stats;

    private List<object> _qList;
    private QuestionList _curQ;
    private int _randQ;

    private int _trueAnswerIndex;
    private int _falseAnswerIndex;

    private int _selectCategory = 0;

    private int _qLimit = 10;
    private int _qCounter = 0;

    private int _correctAnswers = 0;

    public void OnClickPlay(int qLimit)
    {
        _qList = new List<object>(_category[_selectCategory].questions);
        _qLimit = qLimit;
        _qCounter = 0;
        _correctAnswers = 0;

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].gameObject.SetActive(false);

        QuestionGenerate();
        ActionGameStarted?.Invoke();
    }

    public void SelectCategory(int index)
    {
        if (index < 0)
            _selectCategory = Random.Range(0, _category.Length);
        else
            _selectCategory = index;
    }

    private void QuestionGenerate()
    {
        if (_qCounter > 0)
            CanExit = false;

        _qCounter++;

        if (_qList.Count > 0 && _qCounter <= _qLimit)
        {
            _trueAnswerIndex = -2;
            _falseAnswerIndex = -2;

            _randQ = Random.Range(0, _qList.Count);
            _curQ = _qList[_randQ] as QuestionList;
            _qText.text = _curQ.question;
            _qCategoryText.text = _category[_selectCategory].nameOfCategory;

            List<string> _answers = new List<string>(_curQ.answers);

            for (int i = 0; i < _curQ.answers.Length; i++)
            {
                int rand = Random.Range(0, _answers.Count);
                _answersText[i].text = _answers[rand];

                if (_answersText[i].text.ToString() == _curQ.answers[0])
                    _trueAnswerIndex = i;

                _answerBttns[i].image.sprite = _defaultAnswerBttnSprite;
                _answersText[i].color = Color.black;

                _answers.RemoveAt(rand);
            }

            StartCoroutine(AnimBttns());
        }
        else
        {
            /*Debug.Log("Questions have ended or the limit has been reached! \n" 
                + "qLimit: " + _qLimit + " qCounter: " + _qCounter + " qList.Count: " + _qList.Count);*/

            GameEnd();
        }
    }

    private IEnumerator AnimBttns()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].interactable = false;

        int a = 0;
        while(a < _curQ.answers.Length)
        {
            _answerBttns[a].gameObject.SetActive(true);
            _answerBttns[a].gameObject.GetComponent<Animator>().SetTrigger("start");

            a++;
            yield return new WaitForSeconds(0.25f);
        }

        for (int i = 0; i < _curQ.answers.Length; i++)
            _answerBttns[i].interactable = true;

        yield break;
    }

    public void OnClickAnswerBttn(int indexAnswer)
    {
        if (_answersText[indexAnswer].text.ToString() == _curQ.answers[0])
        {
            _falseAnswerIndex = -1;
            StartCoroutine(TrueOrFalseAnswer(true));
        }
        else
        {
            _falseAnswerIndex = indexAnswer;
            StartCoroutine(TrueOrFalseAnswer(false));
        }
    }

    private IEnumerator TrueOrFalseAnswer(bool isTrue)
    {
        if (_trueAnswerIndex == -2)
        {
            Debug.LogError("the index of the correct answer button was not received");
            yield break;
        }
        if (_falseAnswerIndex == -2)
        {
            Debug.LogError("the index of the button pressed incorrectly was not received");
            yield break;
        }

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].interactable = false;

        _answersText[_trueAnswerIndex].color = Color.green;
        _answerBttns[_trueAnswerIndex].image.sprite = _trueAnswerBttnSprite;

        if (!isTrue)
        {
            _answersText[_falseAnswerIndex].color = Color.red;
            _answerBttns[_falseAnswerIndex].image.sprite = _falseAnswerBttnSprite;
            _answerBttns[_falseAnswerIndex].gameObject.GetComponent<Animator>().SetTrigger("wrong");

            _qProgressBar.IncrementLack((float)_qLimit/100);

            yield return new WaitForSeconds(2);
        }
        else
        {
            _correctAnswers++;
            _qProgressBar.IncrementProgress((float)_qLimit / 100);

            yield return new WaitForSeconds(1);
        }

        _qList.RemoveAt(_randQ);

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].gameObject.SetActive(false);

        QuestionGenerate();

        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "isCorrectAnswer", isTrue}
            };
        AnalyticsService.Instance.CustomData("questionAnswered", parameters);
    }

    public void GameEnd()
    {
        CanExit = true;
        _stats.GetStatsOnGameEnd(--_qCounter, _correctAnswers);
        ActionGameEnded?.Invoke();
    }
}

[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[4];
}

[System.Serializable]
public class CategoryList
{
    public string nameOfCategory;
    public QuestionList[] questions; 
}
