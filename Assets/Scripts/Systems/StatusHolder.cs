using System.Collections.Generic;
using UnityEngine;

public class StatusHolder : MonoBehaviour
{
    private Dictionary<StatusEffect, StatusData> _timerByEffect = new();
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
            if (Time.time > _timerByEffect[status].timeToEnd)
            {
                _removeEffects.Add(status);
                status.ResetStatusEffect(this.transform);
            }

        }

        for (int i = 0; i < _removeEffects.Count; i++) 
        {
            Destroy(_timerByEffect[_removeEffects[i]].feedback);
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
        {
            GameObject feedbackInstance = effect.FeedbackPrefab != null ? Instantiate(effect.FeedbackPrefab, this.transform.position, Quaternion.identity) : null;
            feedbackInstance.transform.SetParent(this.transform);

			_timerByEffect.Add(effect, new StatusData { timeToEnd = Time.time + effect.Duration, feedback = feedbackInstance });
		}
        else
			_timerByEffect[effect].timeToEnd = Time.time + effect.Duration;

        effect.SetStatusEffect(this.transform);

        _hasStarted = true;

	}

    private class StatusData
    {
        public float timeToEnd = 0;
        public GameObject feedback;
    }
}
