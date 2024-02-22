using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;
using Unity.Services.Analytics;
using YG;

public class GameScript : MonoBehaviour
{
    public static bool CanExit = true;
    public static event Action ActionGameStarted;
    public static event Action ActionGameEnded;

    public CategoryList[] Category => _category;


    [SerializeField] private CategoryList[] _category = new CategoryList[2];
    [SerializeField] private TextMeshProUGUI _qCategoryText; 
    [SerializeField] private TextMeshProUGUI _qText;
    [SerializeField] private InterestFact _interestFactButton;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] _answerBttns = new Button[4];
    [SerializeField] private TextMeshProUGUI[] _answersText;
    [SerializeField] private Sprite _defaultAnswerBttnSprite;
    [SerializeField] private Sprite _trueAnswerBttnSprite;
    [SerializeField] private Sprite _falseAnswerBttnSprite;

    [SerializeField] private QProgressBar _qProgressBar;

    [SerializeField] private GameStats _stats;

    private List<object> _qList;
    private List<object> _qNotCorrectList;
    private QuestionList _curQ;
    private int _randQ;

    private int _trueAnswerIndex;
    private int _falseAnswerIndex;

    private int _selectCategory = 0;

    private int _qLimit = 10;
    private int _qCounter = 0;

    private int _correctAnswers = 0;

    private bool _isCorrectionQ;

    public void SelectCategory(int index)
    {
        if (index < 0)
            _selectCategory = Random.Range(0, _category.Length);
        else
            _selectCategory = index;
    }

    public void OnClickPlay(int qLimit)
    {
        _qList = new List<object>(_category[_selectCategory].questions);
        _qNotCorrectList = new List<object>();

        if (qLimit == 0)
            _qLimit = _category[_selectCategory].questions.Length;
        else
            _qLimit = qLimit;

        _qCounter = 0;
        _correctAnswers = 0;
        CanExit = true;

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].gameObject.SetActive(false);

        _qProgressBar.SetMaxValue(_qLimit);
        QuestionGenerate();
        ActionGameStarted?.Invoke();

        CustomEvent roundStartEvent = new CustomEvent("IsRoundStarted")
        {
             { "gameCategory", _category[_selectCategory].nameOfCategory },
        };
        AnalyticsService.Instance.RecordEvent(roundStartEvent);
    }

    private void QuestionGenerate()
    {
        if(_qCounter == 0)
            _qCategoryText.text = _category[_selectCategory].nameOfCategory;

        if(!_isCorrectionQ)
            ++_qCounter;

        if (_qList.Count > 0 && _qCounter <= _qLimit)
        {
            _isCorrectionQ = false;
            _trueAnswerIndex = -2;
            _falseAnswerIndex = -2;

            _randQ = Random.Range(0, _qList.Count);

            _curQ = _qList[_randQ] as QuestionList;
            _qText.text = _curQ.question;

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
        else if(_qNotCorrectList.Count > 0)
        {
            _isCorrectionQ = true;
            _trueAnswerIndex = -2;
            _falseAnswerIndex = -2;

            _randQ = Random.Range(0, _qNotCorrectList.Count);

            _curQ = _qNotCorrectList[_randQ] as QuestionList;
            _qText.text = _curQ.question;

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
            _qCounter--;
            QuestionGenerate();
            yield break;
        }
        if (_falseAnswerIndex == -2)
        {
            Debug.LogError("the index of the button pressed incorrectly was not received");
            _qCounter--;
            QuestionGenerate();
            yield break;
        }

        if (_qCounter > 0)
            CanExit = false;

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].interactable = false;

        _answersText[_trueAnswerIndex].color = Color.green;
        _answerBttns[_trueAnswerIndex].image.sprite = _trueAnswerBttnSprite;

        float a = 0;
        if (!isTrue)
        {
            _answersText[_falseAnswerIndex].color = Color.red;
            _answerBttns[_falseAnswerIndex].image.sprite = _falseAnswerBttnSprite;
            _answerBttns[_falseAnswerIndex].gameObject.GetComponent<Animator>().SetTrigger("wrong");

            if (!_isCorrectionQ)
            {
                _qProgressBar.IncrementLack();
                _qNotCorrectList.Add(_qList[_randQ]);
            }
            a += 2f;
        }
        else
        {
            _qProgressBar.IncrementProgress();

            if(_isCorrectionQ)
                _qNotCorrectList.RemoveAt(_randQ);
            else
                _correctAnswers++;
            a += 1f;
        }

        if(!_isCorrectionQ)
            _qList?.RemoveAt(_randQ);

        if (_curQ.qFactsList.nameOfFact != "")
        {
            _interestFactButton.gameObject.SetActive(true);
            _interestFactButton.Enable();
            a += 1f;
        }

        yield return new WaitForSeconds(a);

        while (GameTimer.stop)
            yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].gameObject.SetActive(false);

        if(_interestFactButton.isActiveAndEnabled)
            _interestFactButton?.OutAnimation();

        #region Analytics
        CustomEvent parametersAllAnswered = new CustomEvent("questionAnswered")
            {
                { "isCorrectAnswer", isTrue },
                {"nameQ",  _curQ.question}
            };
        AnalyticsService.Instance.RecordEvent(parametersAllAnswered);

        if (!_isCorrectionQ)
        {
            CustomEvent parametersFirstPartAnswer = new CustomEvent("questionAnswered")
            {
                { "isFirstPartCorrectAnswer", isTrue}
            };
            AnalyticsService.Instance.RecordEvent(parametersFirstPartAnswer);
        }
        else
        {
            CustomEvent parametersSecondPartAnswer = new CustomEvent("questionAnswered")
            {
                { "isSecondPartCorrectAnswer", isTrue}
            };
            AnalyticsService.Instance.RecordEvent(parametersSecondPartAnswer);
        }

        #endregion

        QuestionGenerate();
    }

    public void GameEnd()
    {
        YandexGame.FullscreenShow();

        CanExit = true;
        _stats.GetStatsOnGameEnd(_qCounter - 1, _correctAnswers);
        ActionGameEnded?.Invoke();

        CustomEvent roundEndedEvent = new CustomEvent("IsRoundEnded")
        {
             { "gameCategory", _category[_selectCategory].nameOfCategory },
             { "gameTimeEnd", GameStats.timeRound},
        };
        AnalyticsService.Instance.RecordEvent(roundEndedEvent);
    }

    public void ShowInterestFact()
    {
        ModalWindowController.instance.ShowInterestFact(_curQ.qFactsList.nameOfFact, _curQ.qFactsList.image, _curQ.qFactsList.textMessage);
    }
}

[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[4];
    public InterestFactList qFactsList;
}

[System.Serializable]
public class InterestFactList
{
    public string nameOfFact;
    public Sprite image;
    public string textMessage;
}

[System.Serializable]
public class CategoryList
{
    public string nameOfCategory;
    public QuestionList[] questions; 
}
