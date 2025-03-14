namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Consts
{
    public static class CacheConsts
    {
        public const long PAGE = 1;
        public const long LIMIT = 100;
        public const string DEFAULT_CACHE_KEY_PREFIX = "customer:core:{0}";
        public static readonly TimeSpan DEFAULT_CACHE_EXPIRATION_TIME = TimeSpan.FromMinutes(10);
        public static readonly string ADVISOR_ID = string.Format(DEFAULT_CACHE_KEY_PREFIX, "advisorid:{0}");
        public static readonly string PARTNER_ID = string.Format(DEFAULT_CACHE_KEY_PREFIX, "partnerid:{0}");
        public const string FIXED_INCOME_CACHE_KEY_PREFIX = "fixed-income:core:{0}";
        public const string DIRECT_TREASURY_CACHE_KEY_PREFIX = "direct-treasury:core:{0}";
        public const string EQUITY_CACHE_KEY_PREFIX = "equity:core:{0}";
        public const string FUNDS_CACHE_KEY_PREFIX = "funds:core:{0}";
        public const string PRIVATE_PENSION_CACHE_KEY_PREFIX = "private-pension:core:{0}";
        public const string CLUBS_CACHE_KEY_PREFIX = "clubs:core:{0}";
        public const string COE_CACHE_KEY_PREFIX = "coe:core:{0}";
        public const string TRANSIT_CACHE_KEY_PREFIX = "transit:core:{0}";

        public readonly struct ConsolidatedPositionFilter
        {
            public static readonly string POSITION_DATE = "position-date:{0}";
            public static readonly string FIXED_INCOME_CACHE_ACCOUNT_NUMBER = string.Format(FIXED_INCOME_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string DIRECT_TREASURY_CACHE_ACCOUNT_NUMBER = string.Format(DIRECT_TREASURY_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string EQUITY_CACHE_ACCOUNT_NUMBER = string.Format(EQUITY_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string FUNDS_CACHE_ACCOUNT_NUMBER = string.Format(FUNDS_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string PRIVATE_PENSION_CACHE_ACCOUNT_NUMBER = string.Format(PRIVATE_PENSION_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string CLUBS_CACHE_ACCOUNT_NUMBER = string.Format(CLUBS_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string COE_CACHE_ACCOUNT_NUMBER = string.Format(COE_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string TRANSIT_CACHE_ACCOUNT_NUMBER = string.Format(TRANSIT_CACHE_KEY_PREFIX, "consolidated:accountnumber:{0}");
            public static readonly string CATEGORIES_CHECKING_ACCOUNT_NUMBER = string.Format(DEFAULT_CACHE_KEY_PREFIX, "categories:consolidated:accountnumber:{0}");
        }

        public readonly struct Statement
        {

            public static readonly string START_DATE = "start-date:{0}";
            public static readonly string END_DATE = "end-date:{0}";
            public static readonly string CURRENT_ACCOUNT = "current-account";
            public static readonly string PENSION = "pension";
            public static readonly string FUND = "fund";
            public static readonly string FIXEDINCOME = "fixed-income";
            public static readonly string DIRECT_TREASURY = "direct-treasury";
            public static readonly string CLUB = "club";
        }

        public readonly struct Movement
        {
            public static readonly string PRODUCT_TYPE = "product-type:{0}";
            public static readonly string START_DATE = "start-date:{0}";
            public static readonly string END_DATE = "end-date:{0}";
            public static readonly string CURRENT_ACCOUNT = "current-account:{0}";
        }
    }
}
