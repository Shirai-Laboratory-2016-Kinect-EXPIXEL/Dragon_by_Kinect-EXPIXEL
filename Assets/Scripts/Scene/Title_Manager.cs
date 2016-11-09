﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
//========================================================================
// ■ Title_Manager
//------------------------------------------------------------------------
//	タイトル管理クラス。
//========================================================================

public class Title_Manager : Scene_Manager {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Save_Data save;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();

		save = Base_Scripts.get_instance()
			.GetComponent<Save_Data>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
	}
	//--------------------------------------------------------------------
	// ● ボタンが押されたコールバック関数
	//--------------------------------------------------------------------
	public override void on_click(GameObject go) {
		base.on_click(go);

//		Audio_Manager.se.play("");

		switch (go.name) {
			case "Button_Start":	next_scene_name = "Game";	break;
			case "Button_Exit":		state = State.FINALIZE;		break;
		}
	}
}