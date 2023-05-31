using System;
using MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace Training
{
	internal sealed class Account
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

		public string Id { get; set; } = string.Empty;

		[BsonElement("account_id")]
		public string AccountId{ get; set; } = string.Empty;

        [BsonElement("account_holder")]
        public string AccountHolder { get; set; } = string.Empty;

        [BsonElement("account_type")]
        public string AccountType { get; set; } = string.Empty;

		[BsonElement("balance")]
		public decimal Balance { get; set; }

		[BsonElement("transfers_complete")]
		public string[] TransfersComplete { get; set; } = Array.Empty<string>();

    }
}

