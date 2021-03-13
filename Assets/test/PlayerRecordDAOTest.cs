using System.Collections.Generic;

class PlayerRecordDAOTest : IPlayerRecordDAO
{
    public bool StorePlayerRecords(PlayerRecord[] playerRecords)
    {
        return true;
    }

    public List<PlayerRecord> RetrievePlayerRecords()
    {
        return new List<PlayerRecord>();
    }
}
