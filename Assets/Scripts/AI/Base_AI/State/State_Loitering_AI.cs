//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ State_Loitering_AI
//------------------------------------------------------------------------
//	人工知能の徘徊状態クラス。
//========================================================================

public class State_Loitering_AI : State_Base_AI {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	enum State {
		LOITERING,
		DETOUR,
		MAX
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	State state = State.LOITERING;
	int index;
	protected float next_wait_second = 10;
	protected State_Base_AI next_state;
	Vector3 last_position;
	float stop_second;
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public State_Loitering_AI(State_Base_AI next_state = null) {
		this.next_state = next_state;
		if (next_state == null)
			next_wait_second = float.PositiveInfinity;
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();

		next_wait_second += Time.time;

		ai.max_move_speed *= 0.5f * ai.status.speed_rate;


		var pos = choice_near_position(ai.points.points);
		for (var i = 0; i < ai.points.points.Count; i++) {
			if (ai.points.points[i] == pos) {
				index = i;
				break;
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();

		switch (state) {
			case State.LOITERING:	update_loitering();	break;
			case State.DETOUR:		update_detour();	break;
		}
		tracking_target();


		var delta = ai.transform.position - last_position;
		stop_second =
			delta.magnitude < 0.1f ? stop_second + Time.deltaTime : 0;

		if (stop_second > 5) {
			state = (State)( ( (int)state + 1 ) % (int)State.MAX );
		}

//		Debug.Log(target_position);
//		Debug_EX.add("" + stop_second);

		last_position = ai.transform.position;
		check_next_state();
	}
	//--------------------------------------------------------------------
	// ● 更新（徘徊）
	//--------------------------------------------------------------------
	void update_loitering() {
		target_position = ai.points.points[index];
		
		var delta = target_position - ai.transform.position;
		if (delta.magnitude < 1) {
			index = (int)Mathf.Repeat(
				(float)index + 1, ai.points.points.Count);
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（迂回）
	//--------------------------------------------------------------------
	void update_detour() {
		return;

		var delta = ai.transform.position - ai.points.transform.position;
		target_position = delta.normalized * 100;
	}
	//--------------------------------------------------------------------
	// ● 次の状態を判定
	//--------------------------------------------------------------------
	public override void check_next_state() {
		base.check_next_state();
		
		if (next_wait_second < Time.time)
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

		ai.max_move_speed *= 2 * ai.status.speed_rate;
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}