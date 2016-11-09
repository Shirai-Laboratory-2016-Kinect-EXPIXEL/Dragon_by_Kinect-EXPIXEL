﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Collision_Mouse_Data
//------------------------------------------------------------------------
//	マウス衝突判定の情報クラス。
//========================================================================

public class Collision_Mouse_Data {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	public enum Type {
		MOUSE_DOWN,
		MOUSE_UP
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Transform self;
	public Type type;
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public Collision_Mouse_Data(Transform self, Type type) {
		this.self = self;
		this.type = type;
	}
}