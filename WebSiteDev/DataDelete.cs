using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSiteDev
{
    public static class DataDelete
    {
        public static bool DeleteCategory(int categoryID)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand Links = new MySqlCommand("SELECT COUNT(*) FROM Product WHERE CategoryID = " + categoryID, con);

                    if (Convert.ToInt32(Links.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Категория используется в услгах! Удаление невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand DeleteQuery = new MySqlCommand("DELETE FROM Category WHERE CategoryID = " + categoryID, con);

                    return DeleteQuery.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool DeleteRole(int roleID)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand Links = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE RoleID = " + roleID, con);

                    if (Convert.ToInt32(Links.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Данная роль используется! Удаление невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand DeleteQuery = new MySqlCommand("DELETE FROM Role WHERE RoleID = " + roleID, con);

                    return DeleteQuery.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool DeleteStatus(int statusID)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand Links = new MySqlCommand("SELECT COUNT(*) FROM `Order` WHERE StatusID = " + statusID, con);

                    if (Convert.ToInt32(Links.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Данный статус используется! Удаление невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand DeleteQuery = new MySqlCommand("DELETE FROM Status WHERE StatusID = " + statusID, con);

                    return DeleteQuery.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool DeleteProduct(int productID)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand CheckOrderProduct = new MySqlCommand("SELECT COUNT(*) FROM orderproduct WHERE ProductID = " + productID, con);

                    if (Convert.ToInt32(CheckOrderProduct.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Услуга используется в заказах! Удаление невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Product WHERE ProductID = " + productID, con);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool DeleteUser(int userID, int currentUserID)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    if (userID == currentUserID)
                    {
                        MessageBox.Show("Невозможно удалить пользователя под которым совершён вход!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand checkOrders = new MySqlCommand("SELECT COUNT(*) FROM `Order` WHERE UserID = " + userID, con);

                    if (Convert.ToInt32(checkOrders.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Пользователь имеет заказы! Удаление невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Users WHERE UserID = " + userID, con);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool DeleteClient(int clientID)
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();

                    MySqlCommand CheckOrders = new MySqlCommand("SELECT COUNT(*) FROM `Order` WHERE ClientID = " + clientID, con);

                    if (Convert.ToInt32(CheckOrders.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Клиент имеет заказы! Удаление невозможно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Clients WHERE ClientID = " + clientID, con);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        //public static bool DeleteOrder(int orderID)
        //{
        //    using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        //    {
        //        try
        //        {
        //            con.Open();

        //            MySqlCommand GetStatus = new MySqlCommand("SELECT s.StatusName FROM `Order` o " +
        //                "JOIN Status s ON o.StatusID = s.StatusID " +
        //                "WHERE o.OrderID = " + orderID, con);

        //            object StatusObject = GetStatus.ExecuteScalar();
        //            string Status = "";

        //            if (StatusObject != null)
        //            {
        //                Status = StatusObject.ToString();
        //            }

        //            if (Status == "Завершён")
        //            {
        //                MessageBox.Show("Нельзя удалить завершённый заказ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return false;
        //            }

        //            if (Status == "Отменён")
        //            {
        //                MessageBox.Show("Нельзя удалить отменённый заказ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return false;
        //            }

        //            MySqlCommand DeleteOrderProducts = new MySqlCommand("DELETE FROM orderproduct WHERE OrderID = " + orderID, con);
        //            DeleteOrderProducts.ExecuteNonQuery();

        //            MySqlCommand cmd = new MySqlCommand("DELETE FROM `Order` WHERE OrderID = " + orderID, con);

        //            return cmd.ExecuteNonQuery() > 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //}
    }
}
