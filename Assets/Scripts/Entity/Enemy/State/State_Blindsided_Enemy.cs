//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Blindsided_Enemy
//------------------------------------------------------------------------
//	敵の不意打ち状態クラス。
//========================================================================

public class State_Blindsided_Enemy : State_Base_Enemy {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float next_state_second = 5;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		next_state_second += Time.time;
		ai.max_move_speed *= 0.5f * ai.status.speed_rate;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

		target_position = ai.player.position;
		tracking_target();
		
		Vector3 position;
		if ( is_sighting( search_enemies(), out position) )
			ai.fsm.change( new State_Chase_Enemy() );
		else if (next_state_second < Time.time)
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

		ai.max_move_speed *= 2 * ai.status.speed_rate;
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}