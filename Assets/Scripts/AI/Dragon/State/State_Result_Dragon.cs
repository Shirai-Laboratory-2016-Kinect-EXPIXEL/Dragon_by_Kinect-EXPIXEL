﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Result_Dragon
//------------------------------------------------------------------------
//	ドラゴンの結果状態クラス。
//========================================================================

public class State_Result_Dragon : State_Base_Dragon {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float next_state_second;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		ai.animator.SetBool("Is_Idle", true);
		for (var i = 0; i < Random.Range(0, 4); i++)
			ai.change_idle_type();

		next_state_second = (Debug.isDebugBuild ? 10 : 120) + Time.time;

		var go = GameObject.FindWithTag("Smartphone");
		if (go != null)	go.GetComponent<Smartphone>().relinquish();

		Audio_Manager.me.set_volume(0.5f);
		Audio_Manager.me.play("Fanfare");
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

		if (next_state_second < Time.time)
			ai.fsm.change( new State_Contact_Dragon() );
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

		ai.animator.SetBool("Is_Idle", false);
		ai.status.item = null;
		
		Audio_Manager.me.play("");
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}