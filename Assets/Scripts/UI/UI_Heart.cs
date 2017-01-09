//------------------------------------------------------------------------
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
	float radian;
	float up_speed;
	Quaternion rotation;
	float alpha_sub_speed;
	new RectTransform transform;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		image = GetComponent<Image>();
		transform = GetComponent<RectTransform>();

		transform.anchoredPosition += new Vector2(
			Random.Range(-30, 30),
			Random.Range(-30, 30) );
		radian = Random.Range(0, Mathf.PI * 2);
		up_speed = Random.Range(0.7f, 1.3f);
		
		if (Random.value > 0.8) {
			var c = image.color;
			var a = c.a;
			c = Color.red;
			c.a = a;
			image.color = c;
		}

		rotation = Quaternion.Euler( 0, 0, Random.Range(-10, 10) );
		transform.rotation = rotation;

		alpha_sub_speed = Random.Range(0.7f, 1.3f);

		transform.localScale *= Random.Range(0.7f, 1.3f);
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		var c = image.color;
		c.a -= alpha_sub_speed * Time.deltaTime;
		c.a = Mathf.Clamp01(c.a);
		image.color = c;
		
		radian += Mathf.PI * 2 * Time.deltaTime;
		radian = Mathf.Repeat(radian, Mathf.PI * 2);

		var hoge =
			rotation *
			new Vector2(Mathf.Sin(radian), up_speed) *
			100 * Time.deltaTime;
		transform.anchoredPosition += new Vector2(hoge.x, hoge.y);

		if (c.a <= 0)	Destroy(gameObject);
	}
}