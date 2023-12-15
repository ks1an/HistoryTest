using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameScript : MonoBehaviour
{
    public static event Action ActionGameStarted;
    public static event Action ActionGameEnded;

    #region UI
    [SerializeField] private QuestionList[] _questions;
    [SerializeField] private Text _qText;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] _answerBttns = new Button[4];
    [SerializeField] private Text[] _answersText;
    [SerializeField] private Sprite _defaultAnswerBttnSprite;
    [SerializeField] private Sprite _trueAnswerBttnSprite;
    [SerializeField] private Sprite _falseAnswerBttnSprite;
    #endregion

    private List<object> _qList;
    private QuestionList _curQ;
    private int _randQ;
    private int _trueAnswerIndex;
    private int _falseAnswerIndex;

    public void OnClickPlay()
    {
        _qList = new List<object>(_questions);

        QuestionGenerate();
        ActionGameStarted?.Invoke();
    }

    private void QuestionGenerate()
    {
        if(_qList.Count > 0)
        {
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
        else
        {
            Debug.Log("Вопросы закончились");
            GameEnd();
        }
    }

    private IEnumerator AnimBttns()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].interactable = false;

        int a = 0;
        while(a < _answerBttns.Length)
        {
            if (!_answerBttns[a].gameObject.activeSelf)
            {
                _answerBttns[a].gameObject.SetActive(true);
                _answerBttns[a].gameObject.GetComponent<Animator>().SetTrigger("start");
            }
            else
                _answerBttns[a].gameObject.GetComponent<Animator>().SetTrigger("start");

            a++;
            yield return new WaitForSeconds(0.25f);
        }

        for (int i = 0; i < _answerBttns.Length; i++)
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
            _answerBttns[_falseAnswerIndex].image.sprite = _falseAnswerBttnSprite;
            _answersText[_falseAnswerIndex].color = Color.red;
            _answerBttns[_falseAnswerIndex].gameObject.GetComponent<Animator>().SetTrigger("wrong");
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }

        _qList.RemoveAt(_randQ);

        for (int i = 0; i < _answerBttns.Length; i++)
            _answerBttns[i].gameObject.SetActive(false);

        QuestionGenerate();
    }

    public void GameEnd()
    {
        ActionGameEnded?.Invoke();
    }
}

[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[4];
}
