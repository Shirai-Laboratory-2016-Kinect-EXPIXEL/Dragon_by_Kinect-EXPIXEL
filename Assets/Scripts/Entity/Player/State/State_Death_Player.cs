//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Death_Player
//------------------------------------------------------------------------
//	プレイヤーの死亡状態クラス。
//========================================================================

public class State_Death_Player : State_Base_Player {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	bool is_max_death;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();
		
		stop();

		ai.set_ragdoll(true);
		
		ai.camera.status.death();

		ai.death_count++;
		is_max_death = ai.death_count > 3;
		if (is_max_death)
			ai.game_manager.game_state = Game_Manager.Game_State.OVER;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();
		
		if ( !is_max_death && Input.GetMouseButtonDown(0) ) {
			ai.status.strength = ai.status.max_strength / 10;
			ai.fsm.change( new State_Controll_Player() );
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

		ai.set_ragdoll(false);
		
		ai.camera.fsm.change( new State_TPS_Camera() );
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}