using System;
using UnityEngine;

public class CodeStyle : MonoBehaviour {
	// Constants: UpperCase SnakeCase
	public const int MAX_HEALTH = 100;

	// Properties: PascalCase
	public static CodeStyle Instance {
		get; private set;
	}

	// Events: PascalCase
	public  event EventHandler OnHealthChanged;

	// Fields: camelCase
	private int health;

	// Function Names: PascalCase
	public void SetHealth(int health) {
		this.health = health;
		OnHealthChanged?.Invoke(this, EventArgs.Empty);
	}

	// Function Params: camelCase
	public void Damage(int damage) {
		health -= damage;
		OnHealthChanged?.Invoke(this, EventArgs.Empty);
	}

}
