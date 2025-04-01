using Dapper;
using SV21T1020578.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020578.DataLayers
{
    public class CustomerUserAccountDAL : BaseDAL
    {
        public CustomerUserAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Kiểm tra thông tin đăng nhập của nhân viên đúng hay không.
        /// Trả về null nếu thông tin đăng nhập không đúng
        /// </summary>
        /// <param name="username">Tên đăng nhập (email của nhân viên)</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount? Authentiate(string username, string password)
        {
            UserAccount? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select CustomerID as UserId,
		                    Email as UserName,
		                    CustomerName as DisplayName,
		                    N'' as Photo,
		                    N'' as RoleNames
                    from Customers
                    where Email = @Email and Password = @Password";
                var parameters = new
                {
                    Email = username,
                    Password = password
                };
                data = connection.QueryFirstOrDefault<UserAccount>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            ;
            return data;
        }

        public bool ChangePassword(string username, string oldpassword, string newpassword)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"
            UPDATE Customers
            SET Password = @NewPassword
            WHERE Email = @Username AND Password = @OldPassword";

                var parameters = new
                {
                    Username = username,
                    OldPassword = oldpassword,
                    NewPassword = newpassword
                };

                int rowsAffected = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                return rowsAffected > 0;
            }
        }
    }
}

