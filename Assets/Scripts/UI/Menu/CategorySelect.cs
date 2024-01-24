using UnityEngine;

public class CategorySelect : MonoBehaviour
{
    [SerializeField] private GameScript _gameScript;

    private int _categoryCounter;

    public void StartSelectCategory()
    {
        _categoryCounter = 0;
        SelectCategory();
    }

    public void NextCategory()
    {
        if (_categoryCounter >= _gameScript.category.Length - 1)
            _categoryCounter = 0;
        else
            _categoryCounter++;

        SelectCategory();
    }
    public void PreviousCategory()
    {
        if (_categoryCounter <= 0)
            _categoryCounter = _gameScript.category.Length - 1;
        else
            _categoryCounter--;

        SelectCategory();
    }
    private void SelectCategory()
    {
        ModalWindowController.instance.ShowSelectCategoryPanel($"({_categoryCounter + 1} из {_gameScript.category.Length})", _gameScript.category[_categoryCounter].nameOfCategory, "", ConfirmSelectCategory, NextCategory, PreviousCategory);
    }

    private void ConfirmSelectCategory()
    {
        _gameScript.SelectCategory(_categoryCounter);
        _gameScript.OnClickPlay(0);
        ModalWindowController.instance.Close();
    }
}
