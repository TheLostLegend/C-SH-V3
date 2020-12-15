using AdventureWorks.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AdventureWorks.DataAccess
{
    public class ArrayPartXML
    {
        private string connectionString;
        private SqlConnection sqlConnection;
        public ArrayPartXML(string connection)
        {
            connectionString = connection;
            sqlConnection = new SqlConnection(connectionString);
        }
        public SearchResult<SalesOrder> ReadOne(int id)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("ReadOne", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SalesOrderId", id);
                var outparam = new SqlParameter("@CustomerId", SqlDbType.Int);
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@TerritoryId", SqlDbType.Int);
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@CreditCardId", SqlDbType.Int);
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@ShipToAddressId", SqlDbType.Int);
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@AccountNumber", SqlDbType.Char);
                outparam.Size = 10;
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@Name", SqlDbType.Char);
                outparam.Size = 50;
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@CardType", SqlDbType.Char);
                outparam.Size = 50;
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);
                outparam = new SqlParameter("@City", SqlDbType.Char);
                outparam.Size = 30;
                outparam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outparam);

                command.ExecuteNonQuery();

                SearchResult<SalesOrder> searchResult = new SearchResult<SalesOrder>();

                searchResult.Entities = new SalesOrder();

                searchResult.Entities.SalesOrderId = id;
                searchResult.Entities.CustomerId = Convert.ToInt32(command.Parameters["@CustomerId"].Value);
                searchResult.Entities.TerritoryId = Convert.ToInt32(command.Parameters["@TerritoryId"].Value);
                searchResult.Entities.CreditCardId = Convert.ToInt32(command.Parameters["@CreditCardId"].Value);
                searchResult.Entities.ShipToAddressId = Convert.ToInt32(command.Parameters["@ShipToAddressId"].Value);
                searchResult.Entities.AccountNumber = (string)command.Parameters["@AccountNumber"].Value;
                searchResult.Entities.TerritoryName = (string)command.Parameters["@Name"].Value;
                searchResult.Entities.CardType = (string)command.Parameters["@CardType"].Value;
                searchResult.Entities.City = (string)command.Parameters["@City"].Value;

                return searchResult;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
