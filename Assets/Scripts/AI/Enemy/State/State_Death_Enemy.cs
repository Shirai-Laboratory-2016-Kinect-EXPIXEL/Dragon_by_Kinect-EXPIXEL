﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Death_Enemy
//------------------------------------------------------------------------
//	敵の死亡状態クラス。
//========================================================================

public class State_Death_Enemy : State_Base_Enemy {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float destroy_second = 10;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();
		
		stop();
		ai.rigidbody.useGravity = true;
		ai.rigidbody.freezeRotation = false;

		var go = Object.Instantiate(ai.explosion);
		go.transform.position = ai.transform.position + Vector3.up * 0.5f;
		go.transform.rotation = ai.transform.rotation;
		

		var rs = ai.GetComponentsInChildren<Renderer>();
		foreach (var r in rs) {
			r.material.SetColor("_Color", Color.white * 0.1f);
		}

		destroy_second += Time.time;

//		ai.ses["Engine"].Stop();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

		if (destroy_second < Time.time)
			Object.Destroy(ai.gameObject);
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	public override void late_update() {
		base.late_update();
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	public override void finalize() {
		base.finalize();

//		ai.ses["Engine"].Play();
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}