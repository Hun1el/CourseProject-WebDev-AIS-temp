using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WebSiteDev;

public class DataManipulation
{
    private DataTable data;
    public DataView view;

    public DataManipulation(DataTable table)
    {
        data = table;
        view = data.DefaultView;
    }

    // Методы для применения всех функций
    public void ApplyAllUser(ComboBox comboSort, ComboBox comboFilter, TextBox textSearch)
    {
        ApplySearchUser(textSearch);
        ApplyFilterUser(comboFilter);
        ApplySortUser(comboSort);
    }

    public void ApplyAllCategory(ComboBox comboSort, TextBox textSearch)
    {
        ApplySearchCategory(textSearch);
        ApplySortCategory(comboSort);
    }

    public void ApplyAllProduct(ComboBox comboSort, ComboBox comboFilter, TextBox textSearch)
    {
        ApplySearchProduct(textSearch);
        ApplyFilterProduct(comboFilter);
        ApplySortProduct(comboSort);
    }

    public void ApplyAllOrder(ComboBox comboSort, ComboBox comboFilter, TextBox textSearch)
    {
        ApplySearchOrder(textSearch);
        ApplyFilterOrder(comboFilter);
        ApplySortOrder(comboSort);
    }

    public void ApplyAllClient(ComboBox comboSort, TextBox textSearch)
    {
        ApplySearchClient(textSearch);
        ApplySortClient(comboSort);
    }

    public void ApplyAllDirector(ComboBox comboSort, ComboBox comboFilter, TextBox textSearch)
    {
        ApplySearchDirector(textSearch);
        ApplyFilterDirector(comboFilter);
        ApplySortDirector(comboSort);
    }

    public void FillComboBoxWithRoles(ComboBox combo, string firstItem)
    {
        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT RoleID, RoleName FROM Role ORDER BY RoleName", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow dr = dt.NewRow();
            dr["RoleID"] = 0;
            dr["RoleName"] = firstItem;
            dt.Rows.InsertAt(dr, 0);

            combo.DataSource = dt;
            combo.DisplayMember = "RoleName";
            combo.ValueMember = "RoleID";
            combo.SelectedIndex = 0;
        }
    }

    public void FillComboBoxWithCategories(ComboBox combo, string firstItem)
    {
        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CategoryID, CategoryName FROM Category ORDER BY CategoryName", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow dr = dt.NewRow();
            dr["CategoryID"] = 0;
            dr["CategoryName"] = firstItem;
            dt.Rows.InsertAt(dr, 0);

            combo.DataSource = dt;
            combo.DisplayMember = "CategoryName";
            combo.ValueMember = "CategoryID";
            combo.SelectedIndex = 0;
        }
    }

    public void FillComboBoxWithStatuses(ComboBox combo, string firstItem)
    {
        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT StatusID, StatusName FROM Status ORDER BY StatusName", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow dr = dt.NewRow();
            dr["StatusID"] = 0;
            dr["StatusName"] = firstItem;
            dt.Rows.InsertAt(dr, 0);

            combo.DataSource = dt;
            combo.DisplayMember = "StatusName";
            combo.ValueMember = "StatusID";
            combo.SelectedIndex = 0;
        }
    }

    public void FillComboBoxWithProducts(ComboBox combo, string firstItem)
    {
        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ProductID, ProductName FROM Product ORDER BY ProductName", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow dr = dt.NewRow();
            dr["ProductID"] = 0;
            dr["ProductName"] = firstItem;
            dt.Rows.InsertAt(dr, 0);

            combo.DataSource = dt;
            combo.DisplayMember = "ProductName";
            combo.ValueMember = "ProductID";
            combo.SelectedIndex = 0;
        }
    }

    public void FillComboBoxWithUsers(ComboBox combo, string firstItem)
    {
        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT UserID, CONCAT(IFNULL(Surname,''), ' ', IFNULL(FirstName,''), ' ', IFNULL(MiddleName,'')) AS FullName FROM Users ORDER BY FullName", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow dr = dt.NewRow();
            dr["UserID"] = 0;
            dr["FullName"] = firstItem;
            dt.Rows.InsertAt(dr, 0);

            combo.DataSource = dt;
            combo.DisplayMember = "FullName";
            combo.ValueMember = "UserID";
            combo.SelectedIndex = 0;
        }
    }

    public void FillComboBoxWithClients(ComboBox combo, string firstItem)
    {
        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ClientID, CONCAT(IFNULL(Surname,''), ' ', IFNULL(FirstName,''), ' ', IFNULL(MiddleName,'')) AS FullName FROM Clients ORDER BY FullName", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow dr = dt.NewRow();
            dr["ClientID"] = 0;
            dr["FullName"] = firstItem;
            dt.Rows.InsertAt(dr, 0);

            combo.DataSource = dt;
            combo.DisplayMember = "FullName";
            combo.ValueMember = "ClientID";
            combo.SelectedIndex = 0;
        }
    }

    // Методы поиска
    public void ApplySearchUser(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"Surname LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplySearchCategory(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"CategoryName LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplySearchRole(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"RoleName LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplySearchStatus(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"StatusName LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplySearchProduct(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"ProductName LIKE '%{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplySearchOrder(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"Convert(OrderID, 'System.String') LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }

        //if (int.TryParse(textSearch.Text.Trim(), out int orderId))
        //{
        //    view.RowFilter = $"OrderID = {orderId}";
        //}
        //else
        //{
        //    view.RowFilter = "";
        //}
    }

    public void ApplySearchClient(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"Surname LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplySearchDirector(TextBox textSearch)
    {
        string searchText = textSearch.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(searchText))
        {
            view.RowFilter = $"Convert(OrderID, 'System.String') LIKE '{searchText}%'";
        }
        else
        {
            view.RowFilter = "";
        }
    }

    // Методы фильтрации
    public void ApplyFilterUser(ComboBox comboFilter)
    {
        List<string> filters = new List<string>();

        if (comboFilter.SelectedIndex > 0)
        {
            var row = comboFilter.SelectedItem as DataRowView;
            if (row != null)
            {
                string selectedRole = row["RoleName"].ToString().Replace("'", "''");
                filters.Add("RoleName = '" + selectedRole + "'");
            }
        }

        if (!string.IsNullOrEmpty(view.RowFilter))
        {
            filters.Add(view.RowFilter);
        }

        if (filters.Count > 0)
        {
            view.RowFilter = string.Join(" AND ", filters);
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplyFilterProduct(ComboBox comboFilter)
    {
        List<string> filters = new List<string>();

        if (comboFilter.SelectedIndex > 0)
        {
            var row = comboFilter.SelectedItem as DataRowView;
            if (row != null)
            {
                string selectedCategory = row["CategoryName"].ToString().Replace("'", "''");
                filters.Add("Category = '" + selectedCategory + "'");
            }
        }

        if (!string.IsNullOrEmpty(view.RowFilter))
        {
            filters.Add(view.RowFilter);
        }

        if (filters.Count > 0)
        {
            view.RowFilter = string.Join(" AND ", filters);
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplyFilterOrder(ComboBox comboFilter)
    {
        List<string> filters = new List<string>();

        if (comboFilter.SelectedIndex > 0)
        {
            var row = comboFilter.SelectedItem as DataRowView;
            if (row != null)
            {
                string selectedStatus = row["StatusName"].ToString().Replace("'", "''");
                filters.Add("StatusName = '" + selectedStatus + "'");
            }
        }

        if (!string.IsNullOrEmpty(view.RowFilter))
        {
            filters.Add(view.RowFilter);
        }

        if (filters.Count > 0)
        {
            view.RowFilter = string.Join(" AND ", filters);
        }
        else
        {
            view.RowFilter = "";
        }
    }

    public void ApplyFilterDirector(ComboBox comboFilter)
    {
        List<string> filters = new List<string>();

        if (comboFilter.SelectedIndex > 0)
        {
            var row = comboFilter.SelectedItem as DataRowView;
            if (row != null)
            {
                string selectedStatus = row["StatusName"].ToString().Replace("'", "''");
                filters.Add("StatusName = '" + selectedStatus + "'");
            }
        }

        if (!string.IsNullOrEmpty(view.RowFilter))
        {
            filters.Add(view.RowFilter);
        }

        if (filters.Count > 0)
        {
            view.RowFilter = string.Join(" AND ", filters);
        }
        else
        {
            view.RowFilter = "";
        }
    }


    // Методы сортировки
    public void ApplySortUser(ComboBox comboSort)
    {
        if (comboSort.SelectedIndex == 1)
        {
            view.Sort = "Surname ASC";
        }
        else if (comboSort.SelectedIndex == 2)
        {
            view.Sort = "Surname DESC";
        }
        else
        {
            view.Sort = "";
        }
    }

    public void ApplySortCategory(ComboBox comboSort)
    {
        if (comboSort.SelectedIndex == 1)
        {
            view.Sort = "CategoryName ASC";
        }
        else if (comboSort.SelectedIndex == 2)
        {
            view.Sort = "CategoryName DESC";
        }
        else
        {
            view.Sort = "";
        }
    }

    public void ApplySortProduct(ComboBox comboSort)
    {
        if (comboSort.SelectedIndex == 1)
        {
            view.Sort = "BasePrice ASC";
        }
        else if (comboSort.SelectedIndex == 2)
        {
            view.Sort = "BasePrice DESC";
        }
        else
        {
            view.Sort = "";
        }
    }

    public void ApplySortOrder(ComboBox comboSort)
    {
        if (comboSort.SelectedIndex == 1)
        {
            view.Sort = "OrderDate ASC";
        }
        else if (comboSort.SelectedIndex == 2)
        {
            view.Sort = "OrderDate DESC";
        }
        else if (comboSort.SelectedIndex == 3)
        {
            view.Sort = "OrderCompDate ASC";
        }
        else if (comboSort.SelectedIndex == 4)
        {
            view.Sort = "OrderCompDate DESC";
        }
        else
        {
            view.Sort = "";
        }
    }

    public void ApplySortClient(ComboBox comboSort)
    {
        if (comboSort.SelectedIndex == 1)
        {
            view.Sort = "Surname ASC";
        }
        else if (comboSort.SelectedIndex == 2)
        {
            view.Sort = "Surname DESC";
        }
        else
        {
            view.Sort = "";
        }
    }

    public void ApplySortDirector(ComboBox comboSort)
    {
        if (comboSort.SelectedIndex == 1)
        {
            view.Sort = "OrderDate ASC";
        }
        else if (comboSort.SelectedIndex == 2)
        {
            view.Sort = "OrderDate DESC";
        }
        else if (comboSort.SelectedIndex == 3)
        {
            view.Sort = "OrderCompDate ASC";
        }
        else if (comboSort.SelectedIndex == 4)
        {
            view.Sort = "OrderCompDate DESC";
        }
        else
        {
            view.Sort = "";
        }
    }

    // Метод сброса всего
    public void ResetFilters(ComboBox comboSort = null, ComboBox comboFilter = null, TextBox textSearch = null)
    {
        if (comboSort != null)
        {
            comboSort.SelectedIndex = 0;
        }
        if (comboFilter != null)
        {
            comboFilter.SelectedIndex = 0;
        }
        if (textSearch != null)
        {
            textSearch.Clear();
        }

        view.RowFilter = "";
        view.Sort = "";
    }
}
