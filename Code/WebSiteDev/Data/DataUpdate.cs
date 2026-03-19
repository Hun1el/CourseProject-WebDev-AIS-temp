using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSiteDev
{
    public static class DataUpdate
    {
        public static bool UpdateCategory(int categoryID, string newName)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand CheckQuery = new MySqlCommand("SELECT COUNT(*) FROM Category WHERE CategoryName = '" + newName + "' AND CategoryID != " + categoryID, con);
                    if (Convert.ToInt32(CheckQuery.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Категория с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    MySqlCommand UpdateQuery = new MySqlCommand("UPDATE Category SET CategoryName = '" + newName + "' WHERE CategoryID = " + categoryID, con);

                    return UpdateQuery.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateStatus(int statusID, string newName)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand CheckQuery = new MySqlCommand("SELECT COUNT(*) FROM Status WHERE StatusName = '" + newName + "' AND StatusID != " + statusID, con);
                    if (Convert.ToInt32(CheckQuery.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Статус с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    MySqlCommand UpdateQuery = new MySqlCommand("UPDATE Status SET StatusName = '" + newName + "' WHERE StatusID = " + statusID, con);

                    return UpdateQuery.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateRole(int roleID, string newName)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand CheckQuery = new MySqlCommand("SELECT COUNT(*) FROM Role WHERE RoleName = '" + newName + "' AND RoleID != " + roleID, con);
                    if (Convert.ToInt32(CheckQuery.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Роль с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    MySqlCommand UpdateQuery = new MySqlCommand("UPDATE Role SET RoleName = '" + newName + "' WHERE RoleID = " + roleID, con);

                    return UpdateQuery.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateUser(int userID, string surname, string firstName, string middleName, string login, string password, int roleID, string phone)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand checkLogin = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE UserLogin = '" + login + "' AND UserID != " + userID, con);
                    if (Convert.ToInt32(checkLogin.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Логин уже используется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string checkPhoneQuery = "SELECT COUNT(*) FROM `Users` WHERE PhoneNumber = '" + phone + "' AND UserID != " + userID;
                    using (MySqlCommand checkPhoneCmd = new MySqlCommand(checkPhoneQuery, con))
                    {
                        int phoneCount = Convert.ToInt32(checkPhoneCmd.ExecuteScalar());

                        if (phoneCount > 0)
                        {
                            MessageBox.Show("Пользователь с таким номером телефона уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string query = "UPDATE Users SET Surname = '" + surname + "', FirstName = '" + firstName + "', " +
                        "MiddleName = '" + middleName + "', UserLogin = '" + login + "', " +
                        "RoleID = " + roleID + ", PhoneNumber = '" + phone + "'";

                    if (password != null)
                    {
                        query += ", UserPassword = '" + password + "'";
                    }

                    query += " WHERE UserID = " + userID;

                    MySqlCommand update = new MySqlCommand(query, con);
                    return update.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateProduct(int productID, string productName, string productDescription, int categoryID, decimal basePrice)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand checkName = new MySqlCommand("SELECT COUNT(*) FROM Product WHERE ProductName = '" + productName + "' AND ProductID != " + productID, con);
                    
                    if (Convert.ToInt32(checkName.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Услуга с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    MySqlCommand update = new MySqlCommand(
                        "UPDATE Product SET ProductName = '" + productName + "', ProductDescription = '" + productDescription +
                        "', CategoryID = " + categoryID + ", BasePrice = " + basePrice.ToString().Replace(",", ".") +
                        " WHERE ProductID = " + productID, con);

                    return update.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateClient(int clientID, string surname, string firstName, string middleName, string phone, string email)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    string checkEmailQuery = "SELECT COUNT(*) FROM Clients WHERE Email = '" + email + "' AND ClientID != " + clientID;
                    using (MySqlCommand checkEmailCmd = new MySqlCommand(checkEmailQuery, con))
                    {
                        int emailCount = Convert.ToInt32(checkEmailCmd.ExecuteScalar());
                        if (emailCount > 0)
                        {
                            MessageBox.Show("Клиент с таким email уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string checkPhoneQuery = "SELECT COUNT(*) FROM Clients WHERE PhoneNumber = '" + phone + "' AND ClientID != " + clientID;
                    using (MySqlCommand checkPhoneCmd = new MySqlCommand(checkPhoneQuery, con))
                    {
                        int phoneCount = Convert.ToInt32(checkPhoneCmd.ExecuteScalar());
                        if (phoneCount > 0)
                        {
                            MessageBox.Show("Клиент с таким номером телефона уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string query = "UPDATE Clients SET Surname = '" + surname + "', FirstName = '" + firstName + "', " +
                        "MiddleName = '" + middleName + "', PhoneNumber = '" + phone + "', Email = '" + email + "'" +
                        " WHERE ClientID = " + clientID;

                    MySqlCommand update = new MySqlCommand(query, con);
                    return update.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool UpdateOrderStatusAndDate(int orderID, string status, DateTime compDate)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand cmdStatus = new MySqlCommand("SELECT StatusID FROM Status WHERE StatusName = '" + status.Replace("'", "''") + "'", con);
                    int statusID = Convert.ToInt32(cmdStatus.ExecuteScalar());

                    string dateStr = compDate.ToString("yyyy-MM-dd");

                    string query = "UPDATE `Order` SET StatusID = " + statusID + ", OrderCompDate = '" + dateStr + "' WHERE OrderID = " + orderID;

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
