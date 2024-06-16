namespace Api.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class V1
    {
        private const string VersionBase = $"{ApiBase}/v1";
        
        public static class Orders
        {
            private const string Base = $"{VersionBase}/orders";
		
            public const string Create = Base;
            public const string GetAll = Base;
            public const string GetById = $"{Base}/{{id:guid}}";

            // Inner resources
            public const string Rate = $"{Base}/{{id:guid}}/ratings";
            public const string Delete = $"{Base}/{{id:guid}}/ratings";
        }

        public static class Ratings
        {
            private const string Base = $"{VersionBase}/ratings";

            public const string GetUserRatings = $"{Base}/me"; // or just Base 
        }
    }
}