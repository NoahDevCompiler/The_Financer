namespace FinanceTool.Components.Helpers
{
    public class FormatHelper
    {
        public static string FormatCurrency(decimal? value) {
            return value.HasValue ? string.Format("{0:C}", value.Value) : "-";
        }
    }
}
