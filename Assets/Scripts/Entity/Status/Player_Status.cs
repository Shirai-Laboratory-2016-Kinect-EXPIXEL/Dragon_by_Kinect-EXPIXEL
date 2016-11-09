//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
//========================================================================
// ■ Player_Status
//------------------------------------------------------------------------
//	プレイヤーのステータスクラス。
//========================================================================

public class Player_Status : Entity_Status {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public int max_bullets = 100;	// 残弾数
	[HideInInspector] public int bullets;
	[HideInInspector] public float strength_rate;	// 体力比率
	[HideInInspector] public float stamina_rate;	// 持久力比率
	[HideInInspector] public bool is_out_world;
	[HideInInspector] public bool is_play_dead;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
		
		if (strength > 0) {
			strength += 1 * Time.deltaTime;
			strength = Mathf.Clamp(strength, 0, max_strength);
		}

		strength_rate = strength / max_strength;
		stamina_rate = stamina / max_stamina;
	}
	//--------------------------------------------------------------------
	// ● 回復
	//--------------------------------------------------------------------
	public override void recovery() {
		base.recovery();

		bullets = max_bullets;
	}
	//--------------------------------------------------------------------
	// ● ダメージ処理
	//--------------------------------------------------------------------
	public override void damage(float impulsive_force) {
		base.damage(impulsive_force);
		
//		( (Player)ai ).set_ragdoll(true);
	}
}