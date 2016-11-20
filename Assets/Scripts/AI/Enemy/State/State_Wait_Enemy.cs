﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Wait_Enemy
//------------------------------------------------------------------------
//	敵の待機状態クラス。
//========================================================================

public class State_Wait_Enemy : State_Wait_AI {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	protected new Enemy ai;	// ＡＩ本体
	Vector3 wait_position;
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public State_Wait_Enemy(float wait_second = 3,
							float forcing_wait_second = 0,
							State_Base_AI next_state = null) :
		base(wait_second, forcing_wait_second, next_state)
	{
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		ai = (Enemy)base.ai;

		wait_position = ai.transform.position;
		stop();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();
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