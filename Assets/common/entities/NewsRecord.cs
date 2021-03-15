using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsRecord
{
    public int NewsID;
    public string CompanyName;
    public string Content;
    public float FluctuationRate;

    public NewsRecord(int NewsID,string CompanyName,string Content,float FluctuationRate)
    {
        this.NewsID = NewsID;
        this.CompanyName = CompanyName;
        this.Content = Content;
        this.FluctuationRate = FluctuationRate;
    }
}
