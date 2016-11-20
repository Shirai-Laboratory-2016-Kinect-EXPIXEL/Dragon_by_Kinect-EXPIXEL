﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ UI_Heart
//------------------------------------------------------------------------
//	ＵＩのタイトルクラス。
//========================================================================

public class UI_Heart : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Image image;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		image = GetComponent<Image>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		var c = image.color;
		c.a -= 1 * Time.deltaTime;
		c.a = Mathf.Clamp01(c.a);
		image.color = c;
		
		transform.position += Vector3.up * 100 * Time.deltaTime;

		if (c.a <= 0)	Destroy(gameObject);
	}
}