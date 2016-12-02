//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;
//========================================================================
// ■ Item
//------------------------------------------------------------------------
//	アイテムのデータクラス。
//========================================================================

public class Item {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public GameObject game_object;	// ゲームオブジェクト
	public string title;			// タイトル
	public string company;			// 会社名
	public string info;				// 説明文
	public string icon;				// アイコン名
	public string qr_play;			// PlayStoreのQR画像名
	public string qr_app;			// AppStoreのQR画像名
	public bool is_rare;			// 珍しいか？
	public int id;					// ID 番号
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public Item(int id, ArrayList array_list) {
		load(ref id, ref array_list);
	}
	//--------------------------------------------------------------------
	// ● 読み込み
	//--------------------------------------------------------------------
	public void load(ref int id, ref ArrayList array_list) {
		this.id	= id;

		var t = "";
		foreach (var a in array_list)
			t += a + ", ";
		Debug.Log(t);

		title	= (string)array_list[0];
		company	= (string)array_list[1];
		info	= (string)array_list[2];
		icon	= "Icon/" + (string)array_list[3];
		qr_play	= "QR/" + (string)array_list[4];
		qr_app	= "QR/" + (string)array_list[5];
		is_rare = (string)array_list[6] == "" ?
			false : Convert.ToBoolean( (string)array_list[6] );
	}
	//--------------------------------------------------------------------
	// ● デバッグ表示
	//--------------------------------------------------------------------
	public void print() {
		Debug.Log(id);
		Debug.Log(title);
		Debug.Log(company);
		Debug.Log(info);
		Debug.Log(icon);
		Debug.Log(qr_play);
		Debug.Log(qr_app);
	}
}