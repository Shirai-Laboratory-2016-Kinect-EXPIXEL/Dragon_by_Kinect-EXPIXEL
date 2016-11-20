//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Controll_Dragon
//------------------------------------------------------------------------
//	ドラゴンの操作状態クラス。
//========================================================================

public class State_Controll_Dragon : State_Base_Dragon {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public override void initialize() {
		base.initialize();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public override void update() {
		base.update();


		var move_direction = new Vector3(
			Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") );
		if (move_direction.magnitude > 1)	move_direction.Normalize();
		move_direction *= Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

		ai.animator.SetFloat("Move_X", move_direction.x);
		ai.animator.SetFloat("Move_Z", move_direction.z);
		ai.animator.SetFloat("Attack_X", move_direction.x);
		ai.animator.SetFloat("Attack_Z", move_direction.z);
		ai.animator.SetBool("Is_Move", Input.GetKey(KeyCode.Z));
		ai.animator.SetBool("Is_Swim", Input.GetKey(KeyCode.X));
		ai.animator.SetBool("Is_Attack", Input.GetKey(KeyCode.C));
		ai.animator.SetBool("Is_Sleep", Input.GetKey(KeyCode.V));
		ai.animator.SetBool("Is_Death", Input.GetKey(KeyCode.B));

//		update_move();		// 移動
	}
	//--------------------------------------------------------------------
	// ● 更新（移動）
	//--------------------------------------------------------------------
	void update_move() {
		var input_vector = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		);
		if (input_vector.magnitude > 1)
			input_vector = input_vector.normalized;

		
		// 方向転換
		var target_rotation = ai.transform.rotation * Quaternion.Euler(
			0,
			Input.GetAxis("Mouse X") * ai.rotate_speed * Time.deltaTime,
			0
		);
		ai.transform.rotation = Quaternion.Lerp(
			ai.transform.rotation,
			target_rotation,
			10 * Time.deltaTime
		);
		
		// 入力されている場合
		if (input_vector != Vector3.zero) {
			// 移動力を設定
			ai.force_direction += ai.transform.rotation * input_vector;
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
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}