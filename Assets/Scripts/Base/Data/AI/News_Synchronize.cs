//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Tag = News_Sentence.Tag;
//========================================================================
// ■ News_Synchronize
//------------------------------------------------------------------------
//	ニュースの同期クラス。
//========================================================================

public class News_Synchronize : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	int index;										// 文章配列の参照位置
	public string file_name;						// ファイル名
	string scene_name;								// シーン名
	string present_text;							// 現在表示文章
	News_Sentence present_news_sentence;			// 現在のニュース文章クラス
	float wait_second;								// 待機時間（秒）
	static int max_id = 0;							// 最大のオブジェクト番号（デバッグ用）
	int id;											// オブジェクト番号（デバッグ用）
	static public ArrayList all = new ArrayList();
	Image ui_background;
	Text ui_text;
	public SpriteRenderer tv_sprite;
	//--------------------------------------------------------------------
	// ● ゲッター
	//--------------------------------------------------------------------
	// シーン名を取得
	public string get_scene_name()	{ return scene_name; }
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		ui_background = GetComponent<Image>();
		ui_text = GetComponentInChildren<Text>();

		// オブジェクト番号を設定
		id = ++max_id;
		
		clear();

		all.Add( gameObject.GetComponent<News_Synchronize>() );

		start_scene("Start");
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	public void Update() {
		// 待機時間が設定されている場合、待機
		if (wait_second > 0) {
			wait_second -= Time.deltaTime;
		
		// 文章が存在する場合
		} else if ( !is_finish() ) {
			// 最後まで再生した場合、削除
			if (index >= Data_Manager.news.get(file_name, scene_name).Count) {
				var c = ui_background.color;
				c.a = 0;
				ui_background.color = c;
				ui_text.text = "";
				c = ui_text.color;
				c = Color.white;
				ui_text.color = c;
				clear();
				start_scene("Start");

			// それ以外の場合、文章命令を実行
			} else {
				execution_text();
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 文章命令を実行
	//--------------------------------------------------------------------
	void execution_text() {
		if (scene_name == "")	return;
		
		present_text = "";	// 現在文章を初期化


		// AI 文章を取得
		present_news_sentence =
			(News_Sentence)Data_Manager.news.get(file_name, scene_name)[index];
		index++;	// 文章配列位置を更新


		// 文章タグで分岐
		switch (present_news_sentence.tag) {
			// 待機命令の場合、待機時間を設定
			case Tag.WAIT:
				wait_second = float.Parse(present_news_sentence.sentences[0]);
				break;
			
			// シーン変更命令の場合
			case Tag.NEXT_SCENE:
				start_scene( present_news_sentence.sentences[0] );
				break;

			// 色の場合、色を設定
			case Tag.IMAGE:
				tv_sprite.sprite = Resources.Load<Sprite>(
					"News/" + present_news_sentence.sentences[0] );
				execution_text();
				break;

			// 色の場合、色を設定
			case Tag.COLOR:
				var c = present_news_sentence.sentences;
				ui_text.color = new Color(
					Mathf.Clamp01(float.Parse(c[0]) / 255),
					Mathf.Clamp01(float.Parse(c[1]) / 255),
					Mathf.Clamp01(float.Parse(c[2]) / 255) );
				execution_text();
				break;

			// ランダム命令の場合
			case Tag.RANDOM:
				// 現在文章をランダム設定
				play( present_news_sentence.get_random_sentence() );
				break;

			// それ以外の場合
			default:
				// 文章を再生
				play( present_news_sentence.sentences[0] );
				break;
		}
	}
	//--------------------------------------------------------------------
	// ● 文章を再生
	//--------------------------------------------------------------------
	public void play(string text) {
		present_text = text;
		var c = ui_background.color;
		c.a = 0.3f;
		ui_background.color = c;
		ui_text.text = present_text;
		
		text = text.Replace("\r", "").Replace("\n", "");
		wait_second = Mathf.Max(text.Length / 6.0f, 2);
	}
	//--------------------------------------------------------------------
	// ● 文章が終了したか？
	//--------------------------------------------------------------------
	public bool is_finish() {
		return scene_name == "";	// シーン名が未設定か？
	}
	//--------------------------------------------------------------------
	// ● シーンを開始
	//--------------------------------------------------------------------
	public void start_scene(string name) {
		clear();		// 前回分を消去

		// シーン名が存在する場合
		if ( Data_Manager.news.is_scene(file_name, name) ) {
//			voice_path = name + "/";	// 声音フォルダ階層を設定
			scene_name = name;			// シーン名を設定
			execution_text();			// 文章命令を実行
		}
	}
	//--------------------------------------------------------------------
	// ● 消去
	//--------------------------------------------------------------------
	public void clear() {
		// 文章が終了していない場合
		if ( !is_finish() ) {
			index = 0;									// 参照位置を初期化
			scene_name = "";							// シーン名を初期化
			present_text = "";							// 現在表示文章を初期化
			wait_second = 0;							// 待機時間（秒）を初期化
			present_news_sentence = new News_Sentence();	// ニュース文章を初期化
		}
	}
}