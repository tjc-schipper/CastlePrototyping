using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildDialogItem : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
{

	public event System.Action<Buildable> Clicked;

	[SerializeField]
	UnityEngine.UI.Image icon;
	[SerializeField]
	UnityEngine.UI.Text nameLabel;
	[SerializeField]
	UnityEngine.UI.Text costLabel;

	public const float FRAME_WIDTH = 128f;
	public const float FRAME_HEIGHT = 200f;

	private PlayerResources playerResources;
	private Buildable buildable;

	[Zenject.Inject]
	public void Construct(PlayerResources playerResources)
	{
		this.playerResources = playerResources;
	}

	public void Init(Buildable buildable)
	{
		this.buildable = buildable;
		//bool canAfford = Root.PlayerResources.CanAfford(buildable.Cost);
		bool canAfford = this.playerResources.CanAfford(buildable.Cost);

		this.nameLabel.text = buildable.DisplayName;

		this.icon.sprite = buildable.BuildMenuIcon;
		this.icon.color = canAfford ? Color.white : Color.grey;

		this.costLabel.color = canAfford ? Color.white : Color.red;
		this.costLabel.text = buildable.Cost.materials.ToString();  //TODO: Swap with a resource visualizer! (stacked vertical)
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		if (Clicked != null)
			Clicked.Invoke(this.buildable);
	}




	public class Factory : Zenject.PlaceholderFactory<BuildDialogItem> { }
}
