using System;
using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Models;
using AdventureWorks.DataAccess;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdventureWorks.ServiceLayer
{
    public class ArrayDataXML
    {
        private string connectionString;
        public ArrayDataXML(string connection)
        {
            connectionString = connection;
        }
        public SearchResult<SalesOrder>[] GetOrders(XMLCriteria searchCriteria)
        {

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SearchResult<SalesOrder>[] searchResults = new SearchResult<SalesOrder>[searchCriteria.Count];
            ArrayPartXML storedProcedure = new ArrayPartXML(connectionString);
            int currentId = searchCriteria.StartId;
            int lastId = searchCriteria.StartId + searchCriteria.Count;

            for (int i = 0; currentId < lastId; i++, currentId++)
            {
                searchResults[i] = storedProcedure.ReadOne(currentId);
            }
            return searchResults;
        }
    }
}