﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ State_Look_Camera
//------------------------------------------------------------------------
//	対象方向視点カメラの状態クラス。
//========================================================================

public class State_Look_Camera : State_Base_Camera {
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

		// 対象を向く
		if (ai.look_target != null) {
			ai.transform.position = ai.player.position;
			ai.transform.LookAt(ai.look_target);
			ai.transform.position -= ai.transform.forward * 3;

		// ＴＰＳ視点に変更
		} else {
			ai.fsm.change( new State_TPS_Camera() );
		}
	}
	//--------------------------------------------------------------------
	// ● 終了
	//--------------------------------------------------------------------
	override public void finalize() {
		base.finalize();	// 基盤クラスを終了
	}
}