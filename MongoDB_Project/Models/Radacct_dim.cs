using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_Project
{
    [BsonIgnoreExtraElements]
    public class Radacct_dim
    {
        [BsonElement("radacctId")]
        public ObjectId radacctId { get; set; }

         [BsonElement("username")]
        public string userName { get; set; }

        [BsonElement("acctstarttime")]
        public DateTime acctStartTime { get; set; }

        [BsonElement("acctstoptime")]
        public DateTime acctStopTime { get; set; }

        [BsonElement("acctinputoctets")]
        public int acctInputOctets { get; set; }
        
    }

}
