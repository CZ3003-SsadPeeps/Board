using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using System;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // PlayerRecord player1 = new PlayerRecord(5,"abc",123,123);
        // PlayerRecord player2 = new PlayerRecord(6,"abc",123,123);
        PlayerRecordDAO a =  new PlayerRecordDAO();
        //a.deleteAllData();
        //EventRecord event1 = new EventRecord(13, "add money", 100);
        //EventRecordDAO eventRecordDAO = new EventRecordDAO();
        //eventRecordDAO.addData(new EventRecord(14, "deduct money", -100));
        //List<EventRecord> eventrecords=eventRecordDAO.RetrieveEventRecords();
        //foreach (EventRecord e in eventrecords)
        //{
        //    Debug.Log(e.EventID);
        //    Debug.Log(e.Amount);
        //    Debug.Log(e.Content);
        //}
        //eventRecordDAO.addData(event1);

        //List<QuestionOptionRecord> questionOptions = new List<QuestionOptionRecord>();
        //QuestionOptionRecord q1 = new QuestionOptionRecord("correct answer", true);
        //QuestionOptionRecord q2 = new QuestionOptionRecord("wrong answer", false);
        //questionOptions.Add(q1);
        //questionOptions.Add(q2);
        //QuizRecord quiz1 = new QuizRecord(99, "why bla bla", 100, "normal", questionOptions);
        //QuizRecord quiz2 = new QuizRecord(98, "why bla bla2", 1000, "hard", questionOptions);
        //QuizRecordDAO quizRecordDAO = new QuizRecordDAO();
        //quizRecordDAO.addData(quiz1);
        //quizRecordDAO.addData(quiz2);

        //List<QuizRecord> quizRecords = quizRecordDAO.RetrieveQuizRecords();
        //foreach (QuizRecord q in quizRecords)
        //{
        //    Debug.Log(q.QuizID);
        //    Debug.Log(q.Difficulty);
        //    Debug.Log(q.Reward + 20);
        //    foreach (QuestionOptionRecord qo in q.QuestionOptions)
        //    {
        //        Debug.Log(qo.Content);
        //        Debug.Log(qo.isCorrect);
        //        if (qo.isCorrect)
        //        {
        //            Debug.Log("correct!");
        //        }
        //    }
        //}

        //string[] answers = { "a", "b", "c", "d" };

        //QuestionsNAnswers qna1 = new QuestionsNAnswers(1, "why earth is round?", answers,2, 100,"hard");
        //QuestionsNAnswers qna2 = new QuestionsNAnswers(2, "why earth is flat?", answers, 2, 1000, "easy");
        //QuestionsNAnswersDAO questionsNAnswersDAO = new QuestionsNAnswersDAO();
        //questionsNAnswersDAO.addData(qna1);
        //questionsNAnswersDAO.addData(qna2);

        //List<QuestionsNAnswers> questionsNAnswers = questionsNAnswersDAO.RetrieveQuestionsNAnswers();
        //foreach (QuestionsNAnswers qq in questionsNAnswers)
        //{
        //    Debug.Log(qq.Question);
        //    Debug.Log(qq.Credit+0.67);
        //    Debug.Log(qq.CorrectAnswer);
        //    foreach (string ss in qq.AnswerSelections)
        //    {
        //        Debug.Log(ss);
        //        Debug.Log(Array.IndexOf(qq.AnswerSelections, ss).Equals(qq.CorrectAnswer));
        //    }
        //}

        // a.doNothing();
        //a.addData(player1);
        //a.addData(player2);
        //List<PlayerRecord> players = a.RetrievePlayerRecords();
        // //a.deleteAllData();
        // //EventRecord event1 = new EventRecord(13, "add money",100);
        // //EventRecordDAO eventRecordDAO = new EventRecordDAO();
        // //eventRecordDAO.addData(event1);

        // // a.doNothing();
        // a.addData(player1);
        // a.addData(player2);
        // List<PlayerRecord> players = a.RetrievePlayerRecords();
        List<PlayerRecord> players = a.getDataByString("5");
        foreach(PlayerRecord player in players)
        {
            Debug.Log(player.DateAchieved);
        }

        // // Delete  all data
        // a.deleteAllData();
        // a.deleteDataByString("1");

        // StockDAO b = new StockDAO();
        // b.deleteAllData();

        // b.StoreStock(stocks);

        // Stock stock1 = new Stock("Tesla",1000,200,1,(float) 5.50);
        // Stock stock2 = new Stock("Tesla",1000,200,2,(float) 5.50);
        // Stock stock3 = new Stock("GME",1000,200,1,(float) 5.50);
        // Stock[] stocks = new Stock[3];
        // stocks[0]=stock1;
        // stocks[1]=stock2;
        // stocks[2]=stock3;
        // b.StoreStock(stocks);

        // List<Stock> res = b.RetrieveStocks();
        // List<Stock> res = b.getDataByString("Tesla");
        // foreach(Stock stock in res)
        // {
        //     Debug.Log(stock.companyName);
        // }
        // b.doNothing();
        // b.deleteDataByString("Tesla");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
