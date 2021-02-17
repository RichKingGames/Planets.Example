using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeUpAbility : Ability
{
    private float _upDownDuration;
    public Animator _animator;

    public void Initialize(float duration, float upDownDuration, float cooldownDuration,
        GameObject imageCooldown, Text textCooldown, Button button,GameController controller)
    {
        base.Init(duration, cooldownDuration, imageCooldown, textCooldown, button, controller);
        _upDownDuration = upDownDuration;

    }
    public float GetSizeUpRatio()
    {
        if (_timer < _upDownDuration)
        {
            return _timer / _upDownDuration;
        }
        if (_timer < (_duration - _upDownDuration))
        {
            return 1.0f;
        }
        if (_timer < _duration)
        {
            return (_duration - _timer) / _upDownDuration;
        }
        return 0.0f;
    }

    public override void AnimationUp()
    {
        Controller.gameManager.GetComponent<PlanetSoundsController>().SizeUpSound();
        _animator.Play("SizeUp");
        
        
    }

    public override void AnimationDown()
    {
        _animator.Play("SizeDown");

    }
}

