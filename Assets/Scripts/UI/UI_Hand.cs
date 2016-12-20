//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ UI_Hand
//------------------------------------------------------------------------
//	ＵＩの手クラス。
//========================================================================

public class UI_Hand : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	new RectTransform transform;
	Dragon dragon;
	Vector3 last_position;
	public GameObject ui_heart;
	float next_heart_second;
	float next_change_second;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		transform = GetComponent<RectTransform>();
		dragon = GameObject.FindWithTag("Dragon").GetComponent<Dragon>();

	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		transform.anchoredPosition = Input_EX.hand_position;
		

		var ray_pos = transform.position;
		ray_pos.z = 0;
		RaycastHit hit_info;
/*
		var ray = RectTransformUtility.ScreenPointToRay(
			Camera.main, transform.anchoredPosition);
		var is_hit = Physics.Raycast(
			ray, out hit_info, 5);
*/
		var is_hit = Physics.Raycast(
			Camera.main.ScreenPointToRay(ray_pos),
			out hit_info,
			5);

		if (is_hit && hit_info.transform.root.tag == "Dragon") {
			var delta = transform.position - last_position;
			var distance = delta.magnitude;
			if (distance > 10) {
				var mood_add = distance / 400;//1000;
				dragon.status.mood += mood_add;

				if (next_change_second < Time.time) {
					next_change_second = Time.time + 1;
					dragon.change_idle_type();
				}
				if (next_heart_second < Time.time) {
					next_heart_second = Time.time + 0.1f;
					var go = Instantiate(ui_heart);
					go.transform.SetParent(transform.parent, false);
					go.transform.position = transform.position;
					go.transform.rotation = transform.rotation;

					var se = mood_add > 0.05 ? "SE_Cute_2" : "SE_Cute_1";
					dragon.ses[se].PlayOneShot(dragon.ses[se].clip);
				}
			}
		}


		last_position = transform.position;
	}
}