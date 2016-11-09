﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
//========================================================================
// ■ Player_Tool
//------------------------------------------------------------------------
//	プレイヤーの道具クラス。
//========================================================================

public class Player_Tool {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	// 状態
	public enum State {
		GUN,
		PLAY_DEAD,
		HAND,
		MAX
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Player ai;
	public State state = State.GUN;
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public Player_Tool(Player ai) {
		this.ai = ai;
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	public void initialize() {
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public void update() {
		switch (state) {
			case State.GUN:			update_gun();		break;
			case State.PLAY_DEAD:	update_play_dead();	break;
			case State.HAND:		update_hand();		break;
		}

		var axis = Input.GetAxis("Mouse ScrollWheel");
		axis = axis < 0 ? -1 : axis > 0 ? 1 : 0;
		
		var s = Mathf.Repeat( (int)state + axis, (int)State.MAX );
		state = (State)s;
	}
	//--------------------------------------------------------------------
	// ● 更新（銃）
	//--------------------------------------------------------------------
	void update_gun() {
	}
	//--------------------------------------------------------------------
	// ● 更新（死んだ振り）
	//--------------------------------------------------------------------
	void update_play_dead() {
		if (Input.GetMouseButtonDown(1) &&
			ai.fsm.state() is State_Controll_Player)
		{
			ai.set_ragdoll(!ai.is_ragdoll);
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（手）
	//--------------------------------------------------------------------
	void update_hand() {
	}
}