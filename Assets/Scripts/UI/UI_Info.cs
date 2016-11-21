//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ UI_Info
//------------------------------------------------------------------------
//	ＵＩの説明クラス。
//========================================================================

public class UI_Info : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public List<GameObject> ui_gos;
	int index;
	float next_change_second;
	Dragon dragon;
	CanvasGroup group;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();
		group = GetComponent<CanvasGroup>();

		foreach (var go in ui_gos)
			go.SetActive(false);
		group.alpha = 0;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		group.alpha = dragon.fsm.state() is State_Sleep_Dragon ? 0 : 1;
		

		if (next_change_second < Time.time) {
			next_change_second = Time.time + 0.5f;
			ui_gos[index].SetActive(false);
			index = (int)Mathf.Repeat(index + 1, ui_gos.Count);
			ui_gos[index].SetActive(true);
		}
	}
}