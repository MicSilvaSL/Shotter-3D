using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserView : MonoBehaviour
{
    private LineRenderer _lineRender;

	private void Awake()
	{
		_lineRender = GetComponent<LineRenderer>();
		this.gameObject.SetActive(false);
	}
	public void SetLinePosition(Vector3 orignPoint, Vector3 endPoint) 
	{
		if (_lineRender == null)
			_lineRender = GetComponent<LineRenderer>();


		_lineRender.SetPosition(0, orignPoint);
		_lineRender.SetPosition(1, endPoint);

		if (!this.gameObject.activeSelf)
			this.gameObject.SetActive(true);

	}
}
