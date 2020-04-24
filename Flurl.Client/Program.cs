using Dapper;
using Flurl.Http;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Flurl.Client
{
    public class Program
    {
        private const string _connectionString = "connectionstring";
        private const string _firstFetchQuery =
            "SELECT TOP 1 UserID as [UserId], Username as [Username], BirthDate as [DateOfBirth] FROM Users WITH(NOLOCK)";
        private const string _secondFetchQuery =
            "SELECT TOP 1 FirstName as [FirstName], LastName as [LastName] FROM Users WITH(NOLOCK)";
        
        public static async Task Main(string[] args)
        {
            await using (var sqlConn = new SqlConnection(_connectionString))
            {
                await sqlConn.OpenAsync();
                
                var user = sqlConn.Query(_firstFetchQuery)
                    .FirstOrDefault();

				//dynamic object number 1 with a response back...
				var firstResponse = await "https://localhost:44373/weatherforecast"
					.PostJsonAsync((object)user)
					.ReceiveJson();

				var person = sqlConn.Query(_secondFetchQuery)
					.FirstOrDefault();

				//dynamic object number 2 with a response back...
				var secondResponse = await "https://localhost:44373/weatherforecast/person"
					.PostJsonAsync((object)person)
					.ReceiveJson();
			}
            
            Console.WriteLine("Hello World!");
        }
    }
}