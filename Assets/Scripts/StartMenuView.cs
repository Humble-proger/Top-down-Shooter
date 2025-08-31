using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenuView : MonoBehaviour 
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Dropdown _dropdown;

    public event Action StartGame;
    public Difficulty ChoiseDifficulty;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(ClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickButton);
    }

    public void SetDifficulty(Difficulty difficulty) => _dropdown.value = (int) difficulty - 1;

    private void ClickButton() {
        ChoiseDifficulty = (Difficulty) Enum.ToObject(typeof(Difficulty), _dropdown.value + 1);
        StartGame?.Invoke(); 
    }

    public void Active() => gameObject.SetActive(true);
    public void Disactive() => gameObject.SetActive(false);
}