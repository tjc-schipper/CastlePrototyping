using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarsUI : MonoBehaviour
{

	[SerializeField]
	HealthBar prefab_HealthBar;

	private const float Y_OFFSET = 16f;

	private Dictionary<Damageable, HealthBar> damageables;

	private static HealthBarsUI _instance;
	private static HealthBarsUI instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectsOfType<HealthBarsUI>()[0];

			return _instance;
		}
	}


	void Awake()
	{
		this.damageables = new Dictionary<Damageable, HealthBar>();
	}

	void Update()
	{
		foreach (KeyValuePair<Damageable, HealthBar> pair in instance.damageables)
		{
            pair.Value.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(pair.Key.transform.position) - new Vector3(0f, Y_OFFSET, 0f);

		}
	}

	public static void RegisterDamageable(Damageable d)
	{
		if (!instance.damageables.ContainsKey(d))
		{
			HealthBar newHealthBar = Instantiate<HealthBar>(instance.prefab_HealthBar, instance.transform);
			newHealthBar.Init(d);
			d.Destroyed += instance.HandleDamageableDestroyed;
			instance.damageables.Add(d, newHealthBar);
		}
	}
	
	private void HandleDamageableDestroyed(Damageable d)
	{
		if (this.damageables.ContainsKey(d))
		{
			// Remove from list, unsubscribe event, destroy UI prefab
			d.Destroyed -= HandleDamageableDestroyed;

			HealthBar bar = null;
			if (this.damageables.TryGetValue(d, out bar))
			{
				Destroy(bar.gameObject);
			}

			this.damageables.Remove(d);
		}
	}
}
