namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

[ExcludeFromCodeCoverage]
public sealed class ErrorResourceManager
{
    private const string ResourceFileError = "Errors.ResponseErrors";

    private static readonly Lazy<ErrorResourceManager> Instance = new(() => new ErrorResourceManager());

    public static ErrorResourceManager Read => Instance.Value;

    private ErrorResourceManager() { }

    public DefaultErrorResponse? GetObject(string errorCode, CultureInfo errorLanguage)
    {
        var value = GetString(ResourceFileError, errorCode, errorLanguage);

        return !IsJson(value)
            ? default
            : JsonSerializer.Deserialize<DefaultErrorResponse>
            (
                value,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                    IgnoreReadOnlyProperties = true,
                    IgnoreReadOnlyFields = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new JsonStringEnumConverter() },
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                    WriteIndented = false
                }
            );
    }

    private static string GetString(string resourceFileFullName, string keyName, CultureInfo? cultureInfo = null)
    {
        if (!File.Exists(resourceFileFullName))
            return string.Empty;
        var definedCulture = cultureInfo ?? CultureInfo.CurrentCulture;
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyNameSpace = assembly.GetName().Name;
        var resourceName = $"{assemblyNameSpace}.Resources.{resourceFileFullName}";
        var resourceManager = new ResourceManager(resourceName, assembly);
        var resourceSet = resourceManager?.GetResourceSet(definedCulture, true, true);

        return resourceSet?.Cast<DictionaryEntry>()?.Any(it => it.Key.Equals(keyName)) ?? false
            ? resourceManager?.GetString(keyName, definedCulture) ?? string.Empty
            : string.Empty;
    }

    public static bool IsJson(string? source)
    {
        if (source is null || string.IsNullOrWhiteSpace(source))
            return false;

        try
        {
            JsonDocument.Parse(source, new JsonDocumentOptions { AllowTrailingCommas = true });
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
