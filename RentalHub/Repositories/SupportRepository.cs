using Oracle.ManagedDataAccess.Client;
using RentalHub.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHub.Repositories
{
    public class SupportRepository : RepositoryBase
    {
        public static SupportRepository Instance = new SupportRepository();

        public bool InsertSupportTicket(TicketModel ticketModel)
        {
            string query = @"INSERT INTO SUPPORT (TICKETID, USERID, TICKETTITLE, TICKETMESSAGE, CREATEDATE)
                            VALUES(support_id_seq.nextval, :USERID, :MESSAGETITLE, :MESSAGEBODY, SYSTIMESTAMP)";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("USERID", ticketModel.UserId),
                new OracleParameter("MESSAGETITLE", ticketModel.TicketTitle),
                new OracleParameter("MESSAGEBODY", ticketModel.TicketMessage)
            };

            try
            {
                ExecuteNonQuery(query, parameters);
            }
            catch (OracleException ex)
            {
                // Handle Oracle specific exceptions
                Console.WriteLine($"Oracle Exception: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate
            }

            return true;
        }
    }
}
