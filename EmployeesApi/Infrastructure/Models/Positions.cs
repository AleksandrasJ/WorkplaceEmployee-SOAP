namespace EmployeeApi.Infrastructure.Models
{
    public enum Positions
    {
        Ceo,
        Boss,
        ProductManager,
        InformationAnalist,
        QA,
        SoftwareDeveloper,
        HR,
        NotDefinedYet
    }

    public static class PositionsExtensions
    {
        public static Positions MapStringToPosition(this string position)
        {
            return position switch
            {
                "Ceo" => Positions.Ceo,
                "Boss" => Positions.Boss,
                "Product Manager" => Positions.ProductManager,
                "Information Analist" => Positions.InformationAnalist,
                "QA" => Positions.QA,
                "Software developer" or "Developer" => Positions.SoftwareDeveloper,
                "HR" => Positions.HR,
                _ => Positions.NotDefinedYet,
            };
        }
    }
}