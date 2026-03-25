using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerDamageView : MonoBehaviour
{
	[SerializeField] private Volume playerVolume;
	[SerializeField] private float timeDamageView;
	[SerializeField] private AnimationCurve vignetteCurveAnimation;

	private Coroutine e_flashView;

	public void TriggerDamageView()
	{
		if (e_flashView != null)
			StopCoroutine(e_flashView);

		e_flashView = StartCoroutine(EDamageFlash());
	}

	private IEnumerator EDamageFlash()
	{
		float timeElapsed = 0;

		while(timeElapsed < timeDamageView)
		{
			if (playerVolume.profile.TryGet(out Vignette vignette))
			{
				if (timeDamageView <= 0)
					timeDamageView = 0.1f;

				vignette.intensity.value = vignetteCurveAnimation.Evaluate(timeElapsed/timeDamageView);
			}
			else
				yield break;

			timeElapsed += Time.deltaTime;

			yield return null;
		}
	}

}
