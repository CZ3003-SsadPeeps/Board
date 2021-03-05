interface IPlayerRecordDAO
{
    bool StorePlayerRecords(PlayerRecord[] playerRecords);

    PlayerRecord[] RetrievePlayerRecords();
}
