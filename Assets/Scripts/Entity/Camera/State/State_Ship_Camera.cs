//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Ship_Camera
//------------------------------------------------------------------------
//	船視点カメラの状態クラス。
//========================================================================

public class State_Ship_Camera : State_Base_Camera {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------

	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	override public void initialize() {
		base.initialize();	// 基盤クラスを初期化
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	override public void late_update() {
		base.late_update();	// 基盤クラスを更新

		// マウス入力ベクトルを作成
		var input_vector = new Vector3(
			Input.GetAxis("Mouse Y"),
			Input.GetAxis("Mouse X"),
			0
		);
		if ( Input.GetMouseButton(0) )	input_vector = Vector3.zero;

		// 回転角度ベクトルを更新
		ai.euler_angles +=
			input_vector * ai.rotation_speed * Time.deltaTime;
		ai.euler_angles.x =
			Mathf.Clamp(ai.euler_angles.x, -60, 60);
		ai.euler_angles.y = Mathf.Repeat(ai.euler_angles.y, 360);


		// 注視点を設定
		target_position = ai.ship.position +
//			ai.ship_offset;
			ai.ship.rotation * ai.ship_offset;

		// 位置を更新
		ai.transform.position = target_position +
			Quaternion.Euler(ai.euler_angles) *
			Vector3.forward * ai.ship_distance;

		// 回転を更新
		var delta = target_position - ai.transform.position;
		ai.transform.rotation = Quaternion.Slerp(
			ai.transform.rotation, Quaternion.LookRotation(delta),
			ai.slerp_speed * Time.deltaTime);

		// 障害物補正
		var gaze_offset = ai.ship_offset;
		gaze_offset.x = 0;
		correct_obstacle(
			ai.ship.position + ai.ship.rotation * gaze_offset);
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	override public void finalize() {
		base.finalize();	// 基盤クラスを終了
	}
}