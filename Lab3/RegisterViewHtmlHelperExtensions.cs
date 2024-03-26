using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Lab3
{
    public static class RegisterViewHtmlHelperExtensions
    {
        public static IHtmlContent CustomTextBoxFor<TModel, TValue>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression)
        {
            var label = htmlHelper.LabelFor(expression, new { @class = "control-label" });
            var input = htmlHelper.TextBoxFor(expression, new { @class = "form-control" });
            var validationMessage = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" });

            return new HtmlContentBuilder()
                .AppendHtml(label)
                .AppendHtml(input)
                .AppendHtml(validationMessage);
        }

        public static IHtmlContent CustomPasswordFor<TModel, TValue>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression)
        {
            var label = htmlHelper.LabelFor(expression, new { @class = "control-label" });
            var input = htmlHelper.PasswordFor(expression, new { @class = "form-control" });
            var validationMessage = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" });

            return new HtmlContentBuilder()
                .AppendHtml(label)
                .AppendHtml(input)
                .AppendHtml(validationMessage);
        }

        // Додайте інші методи для інших типів полів, таких як радіокнопки, числа, і т.д.

        // Приклад для radio кнопок
        public static IHtmlContent CustomRadioButtonsFor<TModel, TValue>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string value,
            string label)
        {
            var radioButton = htmlHelper.RadioButtonFor(expression, value, new { id = value });
            var span = new TagBuilder("span");
            span.InnerHtml.Append(label);

            return new HtmlContentBuilder()
                .AppendHtml(radioButton)
                .AppendHtml(span);
        }
    }
}
