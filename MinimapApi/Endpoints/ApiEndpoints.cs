namespace MinimapApi.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Orders
    {
        private const string Base = $"{ApiBase}/orders";

        public const string Place = Base;
        public const string GetAll = Base;
        public const string GetById = $"{Base}/{{id:guid}}";
    }

    public static class ErrorOrs
    {
        public const string ErrorOr = $"{ApiBase}/errorors";
        public const string LangExt = $"{ApiBase}/langext";
    }
}