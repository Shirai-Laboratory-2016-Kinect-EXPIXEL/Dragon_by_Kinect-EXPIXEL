//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//========================================================================
// ■ UI_Result_Exit
//------------------------------------------------------------------------
//	ＵＩの結果終了クラス。
//========================================================================

public class UI_Result_Exit : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public RectTransform hand;
	Image back_ground;
	Text text;
	bool is_collision;
	float collision_second;
	Dragon dragon;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		back_ground = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		var is_next =
			is_collision && dragon.fsm.state() is State_Result_Dragon;

		collision_second =
			is_next ? collision_second + Time.deltaTime : 0;
		if (collision_second > 5)
			dragon.fsm.change( new State_Contact_Dragon() );
		
		back_ground.color = is_next ? new Color(0, 0, 1, 0.8f) :
			new Color(1, 1, 1, 0.8f);
		text.text = is_next ? (5 - (int)collision_second).ToString() :
			"次へ";
	}
	//--------------------------------------------------------------------
	// ● 範囲衝突中のコールバック関数
	//--------------------------------------------------------------------
	void OnTriggerStay(Collider collider) {
		if (collider.transform.parent.name == "UI_Hand")
			is_collision = true;
	}
	//--------------------------------------------------------------------
	// ● 範囲衝突から離れたコールバック関数
	//--------------------------------------------------------------------
	void OnTriggerExit(Collider collider) {
		if (collider.transform.parent.name == "UI_Hand")
			is_collision = false;
	}
}