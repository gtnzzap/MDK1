using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MDK1;


public class DatabaseHelper
{
    private string connectionString = "Server=localhost;Database=MDK_DB;Trusted_Connection=True;";

    public List<Partner> GetPartnersWithSales()
    {
        List<Partner> partners = new List<Partner>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Запрос, который объединяет таблицы Partner_Products_import и Partners_import
            string query = @"
                SELECT
p.PartnerType,
    p.PartnerName,
    p.Phone,
    p.Rating,
    ISNULL(SUM(pp.QuantitySold), 0) AS TotalQuantitySold
FROM 
    dbo.Partners p
LEFT JOIN 
    dbo.Partner_Products pp ON p.PartnerID = pp.PartnerID
GROUP BY 
    p.PartnerName, p.PartnerType, p.Phone, p.Rating;
";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Partner partner = new Partner
                {
                    PartnerType = reader["PartnerType"].ToString(),
                    Name = reader["PartnerName"].ToString(),
                    TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"]),
                    Phone = reader["Phone"].ToString(),
                    Rating = reader["Rating"].ToString()

                };

                // Расчет скидки на основе объема продаж
                partner.Discount = CalculateDiscount(partner.TotalQuantitySold);

                partners.Add(partner);
            }
        }

        return partners;
    }

    private decimal CalculateDiscount(int totalQuantitySold)
    {
        if (totalQuantitySold <= 10000)
            return 0;
        else if (totalQuantitySold <= 50000)
            return 5;
        else if (totalQuantitySold <= 300000)
            return 10;
        else
            return 15;
    }
}