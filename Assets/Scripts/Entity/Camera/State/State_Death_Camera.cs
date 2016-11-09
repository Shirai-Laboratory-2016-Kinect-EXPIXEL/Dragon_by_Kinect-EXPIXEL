﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Death_Camera
//------------------------------------------------------------------------
//	死亡視点カメラの状態クラス。
//========================================================================

public class State_Death_Camera : State_Base_Camera {
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

		target_position = ai.player.position;
		target_position.y =
			Mathf.Max(ai.player.position.y + 0.5f, ai.sea.position.y);
		var delta = ai.transform.position - target_position;

		ai.transform.position = target_position + delta.normalized * 3;
		ai.transform.LookAt(target_position);

		// 障害物補正
		correct_obstacle(target_position);
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	override public void finalize() {
		base.finalize();	// 基盤クラスを終了
	}
}