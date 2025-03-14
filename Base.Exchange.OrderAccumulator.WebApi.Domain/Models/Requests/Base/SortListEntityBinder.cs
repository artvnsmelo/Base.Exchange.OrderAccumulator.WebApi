namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;

public class SortListEntityBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;

        // Try to fetch the value of the argument by name
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var sortList = new List<SortItem?>();

        if (valueProviderResult.FirstValue != null && valueProviderResult.FirstValue.TrimStart().StartsWith('{'))
        {
            foreach (var item in valueProviderResult)
            {
                var sortItem = JsonSerializer.Deserialize<SortItem>(item,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                sortList.Add(sortItem);
            }
        }
        else
        {
            foreach (var item in valueProviderResult)
            {
                if (string.IsNullOrEmpty(item))
                {
                    throw new ArgumentNullException(nameof(SortListEntityBinder));
                }

                var sortValues = item.Split(',');

                if (sortValues == null || sortValues.Length != 2
                    || string.IsNullOrEmpty(sortValues[0]) || string.IsNullOrEmpty(sortValues[1]))
                {
                    throw new ArgumentNullException(nameof(SortListEntityBinder));
                }

                var sortItem = new SortItem { Sort = sortValues[0], SortDirection = sortValues[1] };
                sortList.Add(sortItem);
            }
        }

        bindingContext.Result = ModelBindingResult.Success(sortList);

        return Task.CompletedTask;
    }
}
