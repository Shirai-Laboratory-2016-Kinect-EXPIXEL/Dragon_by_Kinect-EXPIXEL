//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ Text_Manager
//------------------------------------------------------------------------
//	文章管理クラス。
//	テキスト（主に CSV）を読み込み、区切り文字ごとに動的配列に保存し、
//	それを行ごとにハッシュに保存する。
//========================================================================

public class Text_Manager {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	protected const string TOP_PATH = "Data/";	// 全体の階層
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	protected Dictionary<int, ArrayList> datas;	// データキャッシュ
	protected string path;						// フォルダ階層
	string text = "";							// 分割文字列
	protected int start_index = 1;				// 開始要素位置
	int index;									// 要素位置
	bool double_quotation;						// ダブルクォーテーションフラグ
	bool comment;								// コメントフラグ
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected void initialize() {
		// 文章キャッシュを作成
		datas = new Dictionary<int, ArrayList>();
		index = 2 - start_index;
		path = TOP_PATH;
/*
		load("Braun_Tube");
		initialize_test();
*/
	}
	//--------------------------------------------------------------------
	// ● 初期化（テスト）
	//--------------------------------------------------------------------
	protected void initialize_test() {
		// 文章を表示
		foreach (KeyValuePair<int, ArrayList> pair in datas) {
			string line_text = pair.Key + " : ";
			foreach (string text in pair.Value)
				line_text += text + ", ";

			line_text.Replace("\r\n", "\\n").Replace("\r", "\\n")
						.Replace("\n", "\\n");
			Debug.Log(line_text);
		}
	}
	//--------------------------------------------------------------------
	// ● 文章の読み込み
	//--------------------------------------------------------------------
	virtual public void load(string name = null) {
		// リソースを読み込み
		// 名前が設定されている場合、該当ファイルのみを読み込み、
		// 名前が未設定の場合、フォルダ内ファイルを全て読み込む
		TextAsset[] text_assets =
			name == null ? Resources.LoadAll<TextAsset>(path) :
			new TextAsset[] { Resources.Load<TextAsset>(path + name) };

		string texts = "";	// 全体文字列を作成

		// リソース全体を走査し、全体文字列に挿入
		foreach (TextAsset text_asset in text_assets) {
			texts += "file_name," + text_asset.name + "\n";
			texts += text_asset.text.Replace("\r\n", "\n").Replace("\r", "\n")
						+ "\n";
			Resources.UnloadAsset(text_asset);	// リソースを解放
		}


		// 全文字を走査
		foreach (char c in texts) {
			// コメントの場合、コメントフラグを設定
			if ( !comment && text.IndexOf("//") >= 0 ) {
				add();
				comment = true;
			
			// コメント中で、改行の場合、コメントフラグを初期化
			} else if (comment && c == '\n')
				comment = false;


			// コメント中で無い場合
			if (!comment) {
				// 文字がダブルクォーテーションの場合
				if (c == '"')
					double_quotation = !double_quotation;

				// ダブルクォーテーションの中の場合
				else if (double_quotation)
						text += c;
			
				// ダブルクォーテーションで無い場合
				else {
					switch (c) {
						// 改行の場合
						case '\n':
							add();
							// 要素がある場合か、開始要素位置より小さい場合
							if (datas.ContainsKey(index) || index < 1)
								index++;	// 次の要素へ
							break;

						// カンマの場合
						case ',':
							add();
							break;

						// 通常文字の場合
						default:
							text += c;
							break;
					}
				}
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 文章の追加
	//--------------------------------------------------------------------
	void add() {
		// コメント文字列で分割
		string[] texts = text.Split(
			new string[] {"//"}, StringSplitOptions.None);
		text = texts[0];

		int length = text.Split(new string[] {"\n", "\r", " ", "\""},
			StringSplitOptions.RemoveEmptyEntries).Length;
		

		// ダブルクォーテーションの外、通常文字列が存在し、
		// 開始要素位置以上の場合
		if (!double_quotation && (datas.ContainsKey(index) || length > 0) && index > 0) {
			// 要素が無い場合、動的配列を作成
			if ( !datas.ContainsKey(index) )
				datas[index] = new ArrayList();
			datas[index].Add(text);
		}
		text = "";
	}
	//--------------------------------------------------------------------
	// ● 消去
	//--------------------------------------------------------------------
	virtual public void clear() {
		datas.Clear();
	}
}