using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpAbility : Ability
{
    public TrailRenderer _trail;
    public float TrailTime = 1;
    public float TrailWidth = 1;

    public void Initialize(float duration, float cooldownDuration, GameObject imageCooldown, Text textCooldown,
       Button button, GameController controller)
    {
        base.Init(duration, cooldownDuration, imageCooldown, textCooldown, button, controller);
    }
    public override void AnimationDown()
    {
        StartCoroutine(TrailEnd());
    }
    public override void AnimationUp()
    {
        Controller.gameManager.GetComponent<PlanetSoundsController>().SpeedUpSound();
        _trail.time = TrailTime;
        _trail.startWidth = TrailWidth;
        _trail.gameObject.SetActive(true);
    }
    public IEnumerator TrailEnd()
    {
        while (_trail.time > 0)
        {
            _trail.time -= 0.1f;
            yield return new WaitForSeconds(0.001f);
        }
        _trail.gameObject.SetActive(false);
        _trail.time = TrailTime;
    }
    
}
