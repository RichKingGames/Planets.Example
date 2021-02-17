using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUpAbility : Ability
{
    public Animator _animator;
    public void Initialize(float duration, float cooldownDuration, GameObject imageCooldown, Text textCooldown,
        Button button,GameController controller)
    {
        base.Init(duration, cooldownDuration, imageCooldown, textCooldown, button, controller);
    }
    public override void AnimationDown()
    {
        _animator.Play("ShieldDown");
    }
    public override void AnimationUp()
    {
        Controller.gameManager.GetComponent<PlanetSoundsController>().ShieldUpSound();
        _animator.Play("ShieldUp");
    }
}
