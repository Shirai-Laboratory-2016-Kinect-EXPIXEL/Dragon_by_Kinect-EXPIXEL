﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
//========================================================================
// ■ Data_Manager
//------------------------------------------------------------------------
//	データの総合管理クラス。
//========================================================================

public class Data_Manager : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public static Item_Manager item;			// アイテムデータ
	//--------------------------------------------------------------------
	// ● 初期化（早）
	//--------------------------------------------------------------------
	void Awake() {
		item = new Item_Manager();
	}
	//--------------------------------------------------------------------
	// ● 全消去
	//--------------------------------------------------------------------
	public void all_clear() {
		item.clear();
	}
}