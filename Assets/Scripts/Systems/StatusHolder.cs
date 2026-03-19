using System.Collections.Generic;
using UnityEngine;

public class StatusHolder : MonoBehaviour
{
    private Dictionary<StatusEffect, float> _timerByEffect = new();
	private List<StatusEffect> _removeEffects = new();

	private bool _hasStarted = false;


    void Update()
    {
        if (_timerByEffect.Count < 0 )
        {
            if (_hasStarted)
                Destroy(this, 0);
            
            return;
        }

        foreach(StatusEffect status in _timerByEffect.Keys)
        {
            if (Time.time > _timerByEffect[status])
            {
                _removeEffects.Add(status);
                status.ResetStatusEffect(this.transform);
            }

        }

        for (int i = 0; i < _removeEffects.Count; i++) 
        { 
            _timerByEffect.Remove(_removeEffects[i]);
        }

        if (_removeEffects.Count > 0) 
        {
			_removeEffects.Clear();
		}
        
	}

    public void AddEffect(StatusEffect effect)
    {
        if (effect == null) return;

        if (!_timerByEffect.ContainsKey(effect))
			_timerByEffect.Add(effect, Time.time + effect.Duration);
        else
			_timerByEffect[effect] = Time.time + effect.Duration;

        effect.SetStatusEffect(this.transform);

        _hasStarted = true;

	}
}
