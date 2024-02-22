using UnityEngine;
using UnityEngine.UI;

public class CategorySelect : MonoBehaviour
{
    [SerializeField] private GameScript _gameScript;

    private int _categoryCounter;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_button.gameObject.activeSelf)
                _button.onClick.Invoke();
        }
    }
    public void StartSelectCategory()
    {
        _categoryCounter = 0;
        SelectCategory();
    }

    public void NextCategory()
    {
        if (_categoryCounter >= _gameScript.Category.Length - 1)
            _categoryCounter = 0;
        else
            _categoryCounter++;

        SelectCategory();
    }
    public void PreviousCategory()
    {
        if (_categoryCounter <= 0)
            _categoryCounter = _gameScript.Category.Length - 1;
        else
            _categoryCounter--;

        SelectCategory();
    }
    private void SelectCategory()
    {
        ModalWindowController.instance.ShowSelectCategoryPanel($"({_categoryCounter + 1} из {_gameScript.Category.Length})", _gameScript.Category[_categoryCounter].nameOfCategory, "", ConfirmSelectCategory, NextCategory, PreviousCategory);
    }

    private void ConfirmSelectCategory()
    {
        _gameScript.SelectCategory(_categoryCounter);
        _gameScript.OnClickPlay(0);
        ModalWindowController.instance.Close();
    }
}
