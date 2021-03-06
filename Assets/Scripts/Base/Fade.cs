﻿//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ Fade
//------------------------------------------------------------------------
//	フェードのクラス。
//========================================================================

public class Fade : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	public enum State {
		IN,
		OUT
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	State state;
	Image image;
	float rate;
	float speed;
	//--------------------------------------------------------------------
	// ● 状態を変更
	//--------------------------------------------------------------------
	public void change_state(State state) {
		this.state = state;
		speed = state == State.IN ? -1 : 1;
	}
	//--------------------------------------------------------------------
	// ● 終了したか？
	//--------------------------------------------------------------------
	public bool is_finish(State state) {
		return (
			this.state == state &&
			( (state == State.IN && rate == 0) ||
				(state == State.OUT && rate == 1) )
		);
	}
	//--------------------------------------------------------------------
	// ● 初期化（早）
	//--------------------------------------------------------------------
	void Awake() {
		var go = Instantiate( Resources.Load<GameObject>("UI_Fade") );
		DontDestroyOnLoad(go);
		image = go.GetComponentInChildren<Image>();

		rate = 1;
		change_state(State.OUT);
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		rate += speed * Time.deltaTime;
		rate = Mathf.Clamp01(rate);

		var c = image.color;
		c.a = rate;
		image.color = c;

		image.raycastTarget = rate > 0;
	}
}