//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Contact_Dragon
//------------------------------------------------------------------------
//	ドラゴンの接触状態クラス。
//========================================================================

public class State_Contact_Dragon : State_Base_Dragon {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float change_fishing_mood;
	float change_fishing_second;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();
		
		ai.status.mood = 0;
		change_fishing_mood = ai.status.mood + 10;
		change_fishing_second = 30 + Time.time;

		ai.animator.SetBool("Is_Idle", true);
		Audio_Manager.bgm.set_volume(0.5f);
		Audio_Manager.bgm.play("hureai");
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();
		

		if ( ai.animator.GetCurrentAnimatorStateInfo(0).IsName("Wait") )
			ai.animator.SetFloat("Idle_Type", ai.idle_type);


		if (!Input_EX.is_human_exists) {
			ai.fsm.change( new State_Sleep_Dragon() );

		} else if (Input_EX.is_start_fishing ||
			change_fishing_mood < ai.status.mood ||
			change_fishing_second < Time.time)
		{
			ai.fsm.change( new State_Fishing_Dragon() );

		}
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
		Audio_Manager.bgm.play("");
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}