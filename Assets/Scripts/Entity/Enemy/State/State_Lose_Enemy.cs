﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Lose_Enemy
//------------------------------------------------------------------------
//	敵の見失い状態クラス。
//========================================================================

public class State_Lose_Enemy : State_Base_Enemy {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float next_state_second = 3;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		next_state_second += Time.time;
		stop();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();
		

		// 発見している場合、接近状態に遷移
		Vector3 position;
		if ( is_sighting( search_enemies(), out position) ) {
			target_position = position;
			ai.fsm.change( new State_Chase_Enemy() );
		}


		if (next_state_second < Time.time)
			ai.fsm.change( new State_Loitering_Enemy() );
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
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}