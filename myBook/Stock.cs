
using System;

namespace myBook
{
	/// <summary>
	/// Description of Stock.
	/// </summary>
	public class Stock
	{
		public string Symbol { get; set; }
        public string Name { get; set; }
        public string NowPrice { get; set; }
        public string LastTradePrice { get; set; }
        public string OpenPrice { get; set; }
        public string cjl { get; set; }
        public string neipan { get; set; }
        public string waipan { get; set; }

        public string[] bids { get; set; }
        
        public string TradeDetails { get; set; }
        
        public string zde { get; set; }
        public string zdf { get; set; }
        public string DayHighPrice { get; set; }
        public string DayLowPrice { get; set; }
        public string Highest { get; set; }
        public string Lowest { get; set; }
        public string SumInfo { get; set; }
        public string TurnOver { get; set; }
        
        public string TradeTime { get; set; }
	}

    public class Details
    {
        public string time { get; set; }
        public string price { get; set; }
        public string mount { get; set; }
        public string sign { get; set; }
    }

}
