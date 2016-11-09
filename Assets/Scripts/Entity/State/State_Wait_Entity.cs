//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Wait_Entity
//------------------------------------------------------------------------
//	実体の待機状態クラス。
//========================================================================

public class State_Wait_Entity : State_Base_Entity {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	float wait_second = 3;//100000;
	float forcing_wait_second;
	State_Base_Entity next_state;
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public State_Wait_Entity(float wait_second = 3,
								float forcing_wait_second = 0,
								State_Base_Entity next_state = null) {
		this.wait_second = wait_second;
		this.forcing_wait_second = forcing_wait_second;
		this.next_state =
			next_state != null ? next_state : new State_Base_Entity();
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		wait_second += Time.time;
		forcing_wait_second += Time.time;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

//		stop();

		if (forcing_wait_second > Time.time)	return;
		
		if (wait_second < Time.time)
			ai.fsm.change(next_state);
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