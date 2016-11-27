//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ UI_Info
//------------------------------------------------------------------------
//	ＵＩの説明クラス。
//========================================================================

public class UI_Info : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Dragon dragon;
	CanvasGroup group;
	Text text;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();
		group = GetComponent<CanvasGroup>();
		text = GetComponentInChildren<Text>();

		group.alpha = 0;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		group.alpha = dragon.fsm.state() is State_Sleep_Dragon ? 0 : 1;

		if (dragon.fsm.state() is State_Contact_Dragon)
			text.text = "右手を動かして、ドラゴンを撫でよう！";
		else if (dragon.fsm.state() is State_Fishing_Dragon)
			text.text = "ドラゴンが何かを狩っている・・・！？";
		else if (dragon.fsm.state() is State_Result_Dragon)
			text.text = "オススメゲームを採ってきてくれた！";
	}
}