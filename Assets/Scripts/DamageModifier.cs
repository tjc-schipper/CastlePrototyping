using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageModifier {

	int GetPriority();

	int Apply(int damage, Attack.DamageTypes dmgType);
}
