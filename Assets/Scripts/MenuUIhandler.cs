using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIhandler : MonoBehaviour
{

    public int record;
    public Text RecordText;
    public string recordman;


    public InputField playername;

    void Start()
    {
        LoadRecord();
        RecordText.text = $"Record: {recordman}, {record}";
    }



        public void StartNew()
    {
        MainManager.playernamestr = playername.text;
        SceneManager.LoadScene(1);

    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int record;
        public string recordman;
    }


    public void LoadRecord()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            record = data.record;
            recordman = data.recordman;

        }
    }

    public void Deleterecord()
    {
        record = 0;
        recordman = "nobody";
        
            
        SaveData data = new SaveData();
        data.record = record;
        data.recordman = recordman;


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        LoadRecord();
        RecordText.text = $"Record: {recordman}, {record}";
    }



}
