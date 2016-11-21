﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ UI_Mood
//------------------------------------------------------------------------
//	ＵＩの好感度クラス。
//========================================================================

public class UI_Mood : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Text text;
	Dragon dragon;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		text = GetComponent<Text>();
		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		text.enabled = !(dragon.fsm.state() is State_Sleep_Dragon);

		text.text = "好感度 " + (int)dragon.status.mood + " ％";
	}
}