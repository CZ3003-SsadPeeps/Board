using Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardUi : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TextPrefab;
    public Transform content;

    internal void SetLeaderboardDetails()
    {
        //Remove existing stock info
        content.DetachChildren();

        GameObject textObject;
        Text headerText;

        // Headers
        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Name";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "ID";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Date";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Credit";

        PlayerRecordDAO playerRecordDAO = new PlayerRecordDAO();

        //add testdata
        //PlayerRecord[] testplayers = new PlayerRecord[30];
        //string testplayername = "xiaoming";
        //for (int i = 0; i <30; i++)
        //{
        //    testplayers[i] = new PlayerRecord(i, testplayername + i, i + 100, i);
        //}
        //playerRecordDAO.StorePlayerRecords(testplayers);
        

        
        List<PlayerRecord> playerRecords = playerRecordDAO.RetrieveTop30PlayerRecords();
        foreach (PlayerRecord playerRecord in playerRecords)
        {
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = playerRecord.Name;

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = playerRecord.PlayerID.ToString();

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = playerRecord.DateAchieved.ToString();

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = playerRecord.CreditEarned.ToString();


        }

        
    }

    public void backTopPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    void Start()
    {
        //PlayerRecordDAO dao = new PlayerRecordDAO();
        //PlayerRecord pr = dao.retrievePlayerRecords();
        SetLeaderboardDetails();
    }

    // Update is called once per frame
    void Update()
    {
        //scrollbar.SetActive(true);
    }
}
