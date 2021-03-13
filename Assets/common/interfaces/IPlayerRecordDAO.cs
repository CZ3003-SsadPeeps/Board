using System.Collections.Generic;

interface IPlayerRecordDAO
{
    bool StorePlayerRecords(PlayerRecord[] playerRecords);

    List<PlayerRecord> RetrievePlayerRecords();
}
