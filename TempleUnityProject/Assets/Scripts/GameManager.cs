using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject textGameOver;				//ゲームオーバーテキスト
	public GameObject buttons;					//操作ボタン
    public int meteorHP = 2;

    //定数定義
    private const int MAX_ORB = 10;

    //オブジェクト参照
    public GameObject orbPrefab;
    public GameObject canvasGame;
    public GameObject textScore;
	public GameObject TextHighScore;

    //設定値
    public Vector3 createPosition = new Vector3(0f, 800.0f, 0f);





	//高さ変えたい検討
	/*
	float height = GameObject.


	
	var sr = object.GetComponent<SpriteRenderer>();
	var width = sr.bounds.size.x;


	float height = gameObject.GetComponent<Renderer>().bounds.size.y;
	print ("height: " + height);


	float height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
	print ("height: " + height);


	Vector3 tmp = GameObject.Find("hogehoge").transform.position;
	GameObject.Find("hogehoge").transform.position = new Vector3(tmp.x + 100, tmp.y, tmp.z);

	float x = tmp.x;
	float y = tmp.y;
	float z = tmp.z;
	*/



    //メンバ変数
    private int score = 0;
    private int nextScore = 100;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

	// Use this for initialization
	void Start () {
        //初期オーブ生成
        CreateOrb();
        RefreshScoreText();
		RefreshHighScoreText();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // オーブ生成
    public void CreateOrb () {
        GameObject orb = (GameObject)Instantiate(orbPrefab);        
        // HPの設定
        orb.GetComponent<OrbManager>().SetHP(meteorHP);
        //ポジション設定
        orb.transform.localPosition = createPosition;
        meteorHP += 1;
    }

    //オーブ入手
    public void GetOrb (){
        score += 1;
        RefreshScoreText();
    }

	public void LoadTitle() {
		SceneManager.LoadScene ("TitleScene");
	}

	public void GameOver(){
		textGameOver.SetActive (true);
		buttons.SetActive (false);
		Invoke("LoadTitle", 1);
	}

    //スコアてきすと更新
    void RefreshScoreText (){
        textScore.GetComponent<Text>().text =
            score + "/" + nextScore;
    }

	void RefreshHighScoreText(){
		TextHighScore.GetComponent<Text>().text =
			"BEST " + "000";
	}
}
