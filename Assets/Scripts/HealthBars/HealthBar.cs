using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

	[SerializeField]
	UnityEngine.UI.Image container;

	[SerializeField]
	RectTransform mask;

	private readonly Color HEALTHY_COLOR = new Color(0f, 1f, 0f);
	private readonly Color DAMAGED_COLOR = new Color(1f, 0f, 0f);

	private Damageable damageable;


	public void Init(Damageable d)
	{
		this.damageable = d;
		float percentage = (float)d.HP / (float)d.initialHP;
		AdjustMask(percentage);
		d.TookDamage += HandleTookDamage;
	}

	private void AdjustMask(float percentage)
	{
		if (percentage.Equals(1f))
		{
			this.mask.gameObject.SetActive(false);
			this.container.gameObject.SetActive(false);
		}
		else
		{
			this.mask.gameObject.SetActive(true);
			this.container.gameObject.SetActive(true);
			this.mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.container.rectTransform.rect.width * percentage);
			this.mask.GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(DAMAGED_COLOR, HEALTHY_COLOR, percentage);
		}

	}

	private void HandleTookDamage(Damageable d, int damage, int remainingHP)
	{
		float percentage = (float)remainingHP / (float)d.initialHP;
		AdjustMask(percentage);
	}
}
