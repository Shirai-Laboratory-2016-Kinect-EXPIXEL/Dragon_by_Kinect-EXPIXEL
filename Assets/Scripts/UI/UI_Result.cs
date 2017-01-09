//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ UI_Result
//------------------------------------------------------------------------
//	ＵＩの結果クラス。
//========================================================================

public class UI_Result : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	public enum Fade {
		IN = 1,
		OUT = -1
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Image icon;
	public Text title;
	public Text company;
	public Text info;
	public Image qr;
	Image back_ground;
	CanvasGroup group;
	float rate;
	[HideInInspector] public Fade fade = Fade.OUT;
	Dragon dragon;
	Item last_item;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		back_ground = GetComponent<Image>();
		group = GetComponent<CanvasGroup>();
		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		if (dragon.status.item != null &&
			last_item != dragon.status.item)
		{
			var item = dragon.status.item;
			title.text = item.title;
			company.text = item.company;
			info.text = item.info;
			icon.sprite = Resources.Load<Sprite>(item.icon);
			qr.sprite =
				item.qr != "" ? Resources.Load<Sprite>(item.qr)
				: null;
			back_ground.color = Color.white * 0.8f;
		}
		last_item = dragon.status.item;

		fade = dragon.fsm.state() is State_Result_Dragon ?
			Fade.IN : Fade.OUT;


		rate += (int)fade * Time.deltaTime;
		rate = Mathf.Clamp01(rate);

		group.alpha = rate;
	}
}