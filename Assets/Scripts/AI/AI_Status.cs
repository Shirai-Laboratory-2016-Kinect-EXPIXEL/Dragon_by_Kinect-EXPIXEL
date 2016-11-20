//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
//========================================================================
// ■ AI_Status
//------------------------------------------------------------------------
//	人工知能のステータスクラス。
//========================================================================

public class AI_Status : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public float max_strength = 100;			// 最大体力
	public float max_stamina = 100;				// 最大持久力

	[HideInInspector] public float strength;	// 体力
	[HideInInspector] public float stamina;		// 持久力
	[HideInInspector] public float speed_rate = 1;
	[HideInInspector] public float mood;
	[HideInInspector] public Item item;
	
	protected AI ai;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected virtual void Start() {
		recovery();

		ai = GetComponent<AI>();
		var save = Base_Scripts.get_instance()
			.GetComponent<Save_Data>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected virtual void Update() {
		mood -= 0.1f * Time.deltaTime;
		mood = Mathf.Clamp(mood, 0, 100);

//		update_test();
	}
	//--------------------------------------------------------------------
	// ● 回復
	//--------------------------------------------------------------------
	public virtual void recovery() {
		strength = max_strength;
		stamina = max_stamina;
	}
	//--------------------------------------------------------------------
	// ● ダメージ処理
	//--------------------------------------------------------------------
	public virtual void damage(float impulsive_force) {
		if (strength > 0) {
			strength -= impulsive_force / ai.rigidbody.mass;
			strength = Mathf.Max(strength, 0);
			if (strength <= 0)	death();
		}
	}
	//--------------------------------------------------------------------
	// ● 死亡処理
	//--------------------------------------------------------------------
	public virtual void death() {
		strength = 0;
		ai.fsm.change_death();
	}
	//--------------------------------------------------------------------
	// ● 更新（テスト）
	//--------------------------------------------------------------------
	protected virtual void update_test() {
		Debug_EX.add(this + " HP : " + strength);
		Debug_EX.add(this + " Mood : " + mood);
	}
}