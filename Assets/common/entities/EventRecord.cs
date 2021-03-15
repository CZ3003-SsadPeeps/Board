using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRecord
{
    public int EventID;
    public string Content;
    public int Amount;

    public EventRecord(int EventID, string Content, int Amount)
    {
        this.EventID = EventID;
        this.Content = Content;
        this.Amount = Amount;
    }
}
