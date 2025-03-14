namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

[ExcludeFromCodeCoverage]
public abstract class ErrorResponseBase
{
    private const string DefaultErrorTitle = "Error";
    private const string DefaultErrorMessage = "An error has occurred";

    public Guid ErrorId { get; }
    public DateTimeOffset ErrorTimestamp { get; }
    public string ErrorTraceId { get; private set; } = null!;
    public string ErrorCode { get; }
    public string? ErrorTitle { get; set; }
    public string? ErrorDisplay { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorSuggest { get; set; }
    public string? ErrorLanguage { get; set; }

    [JsonIgnore]
    public Exception? ErrorException { get; private set; }

    [JsonIgnore]
    public object[] ErrorArgs { get; set; } = Array.Empty<object>();
    public ErrorResponseBase
    (
        string errorCode,
        params object[] errorArgs
    ) : this(errorCode, null, null, null, null, null, null, errorArgs) { }

    public ErrorResponseBase
    (
        string errorCode,
        string errorMessage,
        params object[] errorArgs
    ) : this(errorCode, errorMessage, null, null, null, null, null, errorArgs) { }

    public ErrorResponseBase
    (
        string errorCode,
        string errorMessage,
        Exception errorException
    ) : this(errorCode, errorMessage, null, null, null, null, errorException) { }

    public ErrorResponseBase
    (
        string errorCode,
        string? errorMessage = null,
        string? errorTitle = null,
        string? errorDisplay = null,
        string? errorSuggest = null,
        string? errorLanguage = null,
        Exception? errorException = null,
        params object[]? errorArgs
    )
    {
        ErrorId = Guid.NewGuid();
        ErrorTimestamp = GetTimestamp();
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorDisplay = errorDisplay;
        ErrorTitle = errorTitle;
        ErrorSuggest = errorSuggest;
        ErrorLanguage = errorLanguage;
        ErrorException = errorException;
        ErrorArgs = errorArgs ?? Array.Empty<object>();
    }

    private static DateTimeOffset GetTimestamp()
    {
        return DateTimeOffset.Now.LocalDateTime;
    }

    public void AutoGenerateFields(HttpContext httpContext)
    {
        var errorLanguage = GetLanguage(httpContext);

        ErrorLanguage = errorLanguage.Name;
        ErrorTraceId = GetErrorTraceId(httpContext);

        var errorResource = GetErrorResource(ErrorCode, errorLanguage);

        ErrorDisplay = SetResourceValue(errorResource?.ErrorDisplay, ErrorDisplay);
        ErrorSuggest = SetResourceValue(errorResource?.ErrorSuggest, ErrorSuggest);
        ErrorTitle = SetResourceValue(errorResource?.ErrorTitle, ErrorTitle, DefaultErrorTitle);
        ErrorMessage = SetResourceValue
        (
            errorResource?.ErrorMessage,
            ErrorMessage,
            DefaultErrorMessage,
            ErrorArgs
        );
    }

    private static string GetErrorTraceId(HttpContext? httpContext)
    {
        return Activity.Current?.Id ?? httpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();
    }

    private static CultureInfo GetLanguage(HttpContext? httpContext)
    {
        var acceptLanguageHeader = httpContext?.Request?.Headers?.FirstOrDefault(it => it.Key.ToLower().Equals("accept-language"));
        var selectedCulture = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .FirstOrDefault
            (
                c => c.Name.Equals
                (
                    acceptLanguageHeader is null || !acceptLanguageHeader.HasValue
                    ? CultureInfo.CurrentCulture.Name
                    : acceptLanguageHeader.Value.ToString(),
                    StringComparison.InvariantCultureIgnoreCase
                )
            );

        return selectedCulture ?? CultureInfo.CurrentCulture;
    }
    private static T? SetResourceValue<T>(T? value, T? newValue, T? defaultValue)
    {
        return value ?? newValue ?? defaultValue;
    }

    private static string? SetResourceValue
    (
        string? value,
        string? newValue,
        string? defaultValue = null,
        object[]? errorArgs = null
    )
    {
        var result = value ?? newValue ?? defaultValue;

        if (result is not null && errorArgs is not null && errorArgs.Any())
        {
            result = string.Format(result, errorArgs);
        }

        return result;
    }

    private static DefaultErrorResponse? GetErrorResource(string errorCode, CultureInfo language)
    {
        return ErrorResourceManager.Read.GetObject(errorCode, language);
    }
}
