//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Controll_Player
//------------------------------------------------------------------------
//	プレイヤーの操作状態クラス。
//========================================================================

public class State_Controll_Player : State_Base_Player {
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


//		if (ai.game_manager.game_state == Game_Manager.Game_State.START)
//			return;
		
		ai.tool.update();
		
		update_use_item();


//		update_move();		// 移動
	}
	//--------------------------------------------------------------------
	// ● 更新（アイテム使用）
	//--------------------------------------------------------------------
	void update_use_item() {
		var camera_up = ai.camera.transform.forward;
		camera_up.x = 0;
		camera_up.z = 0;

		RaycastHit hit_info;
		var is_hit = Physics.Raycast(
			ai.center.position,
			(ai.center.forward + camera_up).normalized,
//			ai.camera.transform.position,
//			ai.camera.transform.forward,
			out hit_info,
			2
//			3
		);
		if (is_hit) {
			var region = hit_info.transform.GetComponent<Region>();
			var item = (region != null ? region.owner : hit_info.transform)
				.GetComponent<Base_Item>();
			if (item == null)
				item = hit_info.transform.root.GetComponent<Base_Item>();
			
			if (item != null) {
				item.visible_info();
				if ( Input.GetMouseButtonDown(0) )
				{
					item.get();
				}
			}
		}

		if (Debug.isDebugBuild) {
			Debug.DrawRay(
				ai.center.position,
				(ai.center.forward + camera_up).normalized * 2,
				Color.red);
		}
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

		ai.set_ragdoll(false);
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void receive_collision_data(ref Collision_Data data) {
		base.receive_collision_data(ref data);
	}
}