//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//========================================================================
// ■ Sentence_Manager
//------------------------------------------------------------------------
//	文章の総合データクラス。
//========================================================================

public class Sentence_Manager : Text_Manager {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	// 文章データ
	protected Dictionary< string, Dictionary<string, ArrayList> > sentences;
	//--------------------------------------------------------------------
	// ● ゲッター
	//--------------------------------------------------------------------
	// データ取得
	public ArrayList get(string file_name, string scene_name) {
		return is_scene(file_name, scene_name) ?
			sentences[file_name][scene_name] : new ArrayList();
	}
	// データ取得
	public Dictionary<string, ArrayList> get(string file_name) {
		return is_file(file_name) ? sentences[file_name] :
			new Dictionary<string, ArrayList>();
	}
	// サブフォルダ階層を取得
	virtual protected string get_sub_path() {
		return "Sentence/";
	}
	//--------------------------------------------------------------------
	// ● ファイルが存在するか？
	//--------------------------------------------------------------------
	public bool is_file(string name) {
		return sentences.ContainsKey(name);
	}
	//--------------------------------------------------------------------
	// ● シーンが存在するか？
	//--------------------------------------------------------------------
	public bool is_scene(string file_name, string scene_name) {
		return is_file(file_name) &&
			sentences[file_name].ContainsKey(scene_name);
	}
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public Sentence_Manager() {
		base.initialize();

		// 文章データを作成
		sentences = new Dictionary< string, Dictionary<string, ArrayList> >();

		path += get_sub_path();	// フォルダ階層を設定

		load();
//		initialize_test();
	}
	//--------------------------------------------------------------------
	// ● 初期化（テスト）
	//--------------------------------------------------------------------
	protected void initialize_test() {
//		base.start_test();

		// 文章を表示
		foreach (KeyValuePair< string, Dictionary<string, ArrayList> > pair
			in sentences) {
			foreach (KeyValuePair<string, ArrayList> p in pair.Value) {
				foreach (Sentence sentence in p.Value)
					sentence.print();
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 文章の読み込み
	//--------------------------------------------------------------------
	override public void load(string name = null) {
		base.load(name);

		string file_name = "";
		string scene = "";

		foreach (KeyValuePair<int, ArrayList> pair in datas) {
			switch ( (string)pair.Value[0] ) {
				case "file_name":
					file_name = (string)pair.Value[1];
					// 要素が無い場合、動的配列を作成
					if ( !sentences.ContainsKey(file_name) )
						sentences[file_name] =
							new Dictionary<string, ArrayList>();
					break;

				case "scene":
					scene = (string)pair.Value[1];
					// 要素が無い場合、動的配列を作成
					if ( !sentences[file_name].ContainsKey(scene) )
						sentences[file_name][scene] = new ArrayList();
					break;

				default:
					add_sentence(file_name, scene, pair.Value);
					break;
			}
		}
		base.clear();
	}
	//--------------------------------------------------------------------
	// ● 文章クラスを追加
	//--------------------------------------------------------------------
	virtual protected void add_sentence(string file_name, string scene,
										ArrayList array_list) {
		sentences[file_name][scene].Add( new Sentence(array_list) );
	}
	//--------------------------------------------------------------------
	// ● 消去
	//--------------------------------------------------------------------
	override public void clear() {
		base.clear();
		sentences.Clear();
	}
}