﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Patreon.Net.Models
{
    /// <summary>
    /// The record of a pledging action taken by the user, or that action's failure.
    /// </summary>
    
    [PatreonResource("pledge-event")]
    public class PledgeEvent : PatreonResource<PledgeEventRelationships>
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PaymentStatusValue
        {
            [EnumMember(Value = "Paid")]
            Paid,
            [EnumMember(Value = "Declined")]
            Declined,
            [EnumMember(Value = "Deleted")]
            Deleted,
            [EnumMember(Value = "Pending")]
            Pending,
            [EnumMember(Value = "Refunded")]
            Refunded,
            [EnumMember(Value = "Fraud")]
            Fraud,
            [EnumMember(Value = "Other")]
            Other
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum PledgeEventType
        {
            [EnumMember(Value = "pledge-event")]
            PledgeEvent,
            [EnumMember(Value = "pledge_start")]
            PledgeStart,
            [EnumMember(Value = "pledge_upgrade")]
            PledgeUpgrade,
            [EnumMember(Value = "pledge_downgrade")]
            PledgeDowngrade,
            [EnumMember(Value = "pledge_delete")]
            PledgeDelete,
            [EnumMember(Value = "subscription")]
            Subscription
        }

        /// <summary>
        /// Amount (in the currency in which the patron paid) of the underlying event.
        /// </summary>
        [JsonProperty("amount_cents")]
        public int AmountCents { get; set; }
        /// <summary>
        /// ISO code of the currency of the event.
        /// </summary>
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        /// <summary>
        /// The date which this event occurred.
        /// </summary>
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
        /// <summary>
        /// Status of underlying payment.
        /// </summary>
        [JsonProperty("payment_status")]
        public PaymentStatusValue PaymentStatus { get; set; }
        /// <summary>
        /// Id of the tier associated with the pledge.
        /// </summary>
        [JsonProperty("tier_id")]
        public string TierId { get; set; }
        /// <summary>
        /// Title of the reward tier associated with the pledge.
        /// </summary>
        [JsonProperty("tier_title")]
        public string TierTitle { get; set; }
        /// <summary>
        /// Event type.
        /// </summary>
        // [JsonProperty("event_type")]
        [JsonIgnore]
        public PledgeEventType EventType  =>  eventTypeMap[Id.Split(':').First()];
        private static Dictionary<string, PledgeEventType> eventTypeMap;
        static PledgeEvent()
        {
            eventTypeMap = new Dictionary<string, PledgeEventType>();
            eventTypeMap.Add("pledge-event", PledgeEventType.PledgeEvent);
            eventTypeMap.Add("pledge_start", PledgeEventType.PledgeStart);
            eventTypeMap.Add("pledge_upgrade", PledgeEventType.PledgeUpgrade);
            eventTypeMap.Add("pledge_downgrade", PledgeEventType.PledgeDowngrade);
            eventTypeMap.Add("pledge_delete", PledgeEventType.PledgeDelete);
            eventTypeMap.Add("subscription", PledgeEventType.Subscription);
        } 
    }

    public class PledgeEventRelationships
    {
        /// <summary>
        /// The campaign being pledged to.
        /// </summary>
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
        /// <summary>
        /// The pledging user
        /// </summary>
        [JsonProperty("patron")]
        public User Patron { get; set; }
        /// <summary>
        /// The tier associated with this pledge event.
        /// </summary>
        [JsonProperty("tier")]
        public Tier Tier { get; set; }
    }
}
