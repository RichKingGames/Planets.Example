using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    internal float _timer;
    internal float _duration;
    private float _cooldownDuration;
    private GameObject _imageCooldown;
    private Text _textCooldown;
    private Button _button;
    public GameController Controller;
   
   
    public void Init(float duration, float cooldownDuration, GameObject imageCooldown,
        Text textCooldown, Button button, GameController controller)
    {
        _duration = duration;
        _cooldownDuration = cooldownDuration;
        _timer = duration + cooldownDuration;
        _imageCooldown = imageCooldown;
        _textCooldown = textCooldown;
        _button = button;
        Controller = controller;
        _button.onClick.AddListener(Activate);
    }
    public void Activate()
    {
        if(!IsAvailable)
        {
            return;
        }
        _timer = 0.0f;
        _imageCooldown.SetActive(true);
        TickImpl();
    }
    public bool IsActive => _timer <= _duration;
    public bool IsAvailable => _timer >= _duration + _cooldownDuration;
    private bool _prevIsActive = false;

    public void Tick(float delta)
    {
        _timer += delta;
        if(!IsAvailable)
        {
            TickImpl();
        }
        else if(_imageCooldown.activeSelf)
        {
            _imageCooldown.SetActive(false);
        }
        if (_prevIsActive && !IsActive)
        { 
            AnimationDown(); 
        }
        else if(!_prevIsActive && IsActive)
        {
            AnimationUp();
        }
        _prevIsActive = IsActive;
    }
    private void TickImpl()
    {
        _textCooldown.text = ((int)(_duration + _cooldownDuration - _timer)).ToString();
        _imageCooldown.GetComponent<Image>().fillAmount = 1.0f - _timer / (_duration + _cooldownDuration);
    }
    public abstract void AnimationUp();
    public abstract void AnimationDown();
}

