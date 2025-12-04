using System.Collections;
using UnityEngine;

public class EntityGlowAnim : MonoBehaviour
{
    private const string FRESNEL_POWER = "_fernel_amount";
	private const string FRESNEL_TRIGGER = "_fresnel_trigger";

    [SerializeField] private float _timeGlowAnim;
	[SerializeField] private float _fresnelAmount;

	private Material _entityMat;
	private Coroutine e_glowAnimation;

	private void Awake()
	{
		_entityMat = GetComponent<MeshRenderer>().material;
	}

	[ContextMenu("asdfasdf")]
    public void TriggerGlow() 
    {
		if (e_glowAnimation != null)
            StopCoroutine(e_glowAnimation);

        e_glowAnimation = StartCoroutine(EGlowAnimation());

		_entityMat.SetInt(FRESNEL_TRIGGER, 1);

	}

    private IEnumerator EGlowAnimation() 
    {
        float timeElapsed = 0;
		
        _entityMat.SetFloat(FRESNEL_POWER, 0.3f);
        
        timeElapsed = 0;

		while (timeElapsed <= _timeGlowAnim)
		{
			float t = timeElapsed / _timeGlowAnim;

			timeElapsed += Time.deltaTime;

			_entityMat.SetFloat(FRESNEL_POWER, Mathf.Lerp(0.3f, 5, t));

			yield return null;
		}

		_entityMat.SetInt(FRESNEL_TRIGGER, 0);
	}



}
