using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
   [Serializable]
    public struct userRecord
    {
        public string userName;
        public int level;
    }

    public List<userRecord> _userRecors;

    public Text userName;
    public GameController gameController;

    public GameObject userNameInput;

    public GameObject leaderBoardListObject;
    public GameObject leaderBoardElementPrefab;

    private void Awake()
    {
        if (retrieveLeaderboardData() == false)
            _userRecors = new List<userRecord>();

        updateBoardList();


    }

    public void openUserNameInput()
    {
        userNameInput.SetActive(true);
    }

    public void closeUserInput()
    {
        userNameInput.SetActive(false);
    }

    public void saveRecord()
    {
        addUserRecord(userName.text, gameController.level);

    }

    void addUserRecord(string userName,int level)
    {
        var record = new userRecord();
        record.level = level;
        record.userName = userName;


        _userRecors.Add(record);

        saveLeaderboardData();

        updateBoardList();
    }

    public void updateBoardList()
    {        
        foreach (Transform child in leaderBoardListObject.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(var score in _userRecors)
        {
            var element = Instantiate<GameObject>(leaderBoardElementPrefab);
            element.transform.SetParent(leaderBoardListObject.transform);

            var elementText = element.GetComponent<Text>();
            elementText.text = score.userName + "  ---  " + score.level.ToString();

        }

    }

    void saveLeaderboardData()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, _userRecors);
        file.Close();

        

    }

    bool retrieveLeaderboardData()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return false;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<userRecord> data = (List<userRecord>)bf.Deserialize(file);
        file.Close();

        _userRecors = data;

        return true;
    }

    
  
}
