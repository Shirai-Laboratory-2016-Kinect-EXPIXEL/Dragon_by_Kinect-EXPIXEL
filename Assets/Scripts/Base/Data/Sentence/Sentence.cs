//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Sentence
//------------------------------------------------------------------------
//	文章のデータクラス。
//========================================================================

public class Sentence {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	// タグ
	public enum Tag {
		NONE,				// 命令無し
		NAME,				// 名前
		IMAGE,				// 画像名
		COLOR,				// 色
		WAIT,				// 待機命令
		RANDOM_SCENE,		// ランダムシーン遷移命令
		NEXT_SCENE,			// シーン遷移命令
		RANDOM,				// ランダム命令の場合
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Tag tag;					// タグ
	public string[] sentences;		// 複数文章
	string last_choice_sentence;	// 最後に選択した文章
	int last_choice_index;			// 最後に選択した要素番号
	//--------------------------------------------------------------------
	// ● ゲッター
	//--------------------------------------------------------------------
	// 最後に選択した要素番号を取得
	public int get_last_choice_index() {
		return last_choice_index;
	}
	// ランダムに文章を取得
	public string get_random_sentence() {
		// 要素が１つしか無い場合、そのまま返す
		if (sentences.Length == 1)	return sentences[0];

		// 前回ランダム選択した要素と、違う要素を戻す
		string temp = last_choice_sentence;
		// 違う要素になるまで、繰り返しランダム選択
		while (last_choice_sentence == temp) {
			last_choice_index = Random.Range(0, sentences.Length);
			temp = sentences[last_choice_index];
		}
		last_choice_sentence = temp;	// 選択要素を保存

		return temp;
	}
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	// デフォルト
	public Sentence() {
		tag = Tag.NONE;
		sentences = new string[] { "" };
	}
	// 配列
	public Sentence(ArrayList array_list) {
		load(ref array_list);
	}
	//--------------------------------------------------------------------
	// ● 読み込み
	//--------------------------------------------------------------------
	public void load(ref ArrayList array_list) {
		// 先頭タグ文字で分岐
		switch ( (string)array_list[0] ) {

			// 名前の場合
			case "name":				tag = Tag.NAME;				break;
			// 画像名の場合
			case "image":				tag = Tag.IMAGE;			break;
			// 色の場合
			case "color":				tag = Tag.COLOR;			break;
			// 待機命令の場合
			case "wait":				tag = Tag.WAIT;				break;
			// ランダムシーン遷移命令の場合
			case "random_scene":		tag = Tag.RANDOM_SCENE;		break;
			// シーン遷移命令の場合
			case "next_scene":			tag = Tag.NEXT_SCENE;		break;
			// ランダム命令の場合
			case "random":				tag = Tag.RANDOM;			break;
			// それ以外の場合
			default:					tag = Tag.NONE;				break;
		}

		// タグが設定されている場合、タグ文字を消去
		if (tag != Tag.NONE)	array_list.RemoveAt(0);

		// 複数文章を設定
		sentences = (string[])array_list.ToArray( typeof(string) );
	}
	//--------------------------------------------------------------------
	// ● デバッグ表示
	//--------------------------------------------------------------------
	public string to_string() {
		// 読み込み文章を表示
		string s = tag + " : ";
		foreach (string text in sentences)
			s += text + ", ";
		s = s.Replace("\r\n", "\\n").Replace("\r", "\\n")
				.Replace("\n", "\\n");
		return s;
	}
	//--------------------------------------------------------------------
	// ● デバッグ表示
	//--------------------------------------------------------------------
	public void print() {
		Debug.Log( to_string() );
	}
}