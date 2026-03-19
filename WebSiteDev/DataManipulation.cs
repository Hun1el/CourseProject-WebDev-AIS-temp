using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WebSiteDev;

public class DataManipulation
{
    private DataTable data;
    private DataView view;

    public DataManipulation(DataTable table)
    {
        data = table;
        view = data.DefaultView;
    }

    public void FillComboBox(ComboBox combo, string firstItem, List<string> items)
    {
        combo.Items.Clear();
        if (!string.IsNullOrEmpty(firstItem))
        {
            combo.Items.Add(firstItem);
        }

        if (items != null && items.Count > 0)
        {
            items.Sort();
            foreach (var item in items)
            {
                combo.Items.Add(item);
            }
        }

        combo.SelectedIndex = 0;
    }

    public List<string> GetListFromQuery(string query, string columnName)
    {
        List<string> list = new List<string>();

        using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                    {
                        list.Add(reader.GetString(columnName));
                    }
                }
            }
        }

        return list;
    }

    public void FillComboBoxWithRoles(ComboBox combo, string firstItem)
    {
        var roles = GetListFromQuery("SELECT RoleName FROM Role", "RoleName");
        FillComboBox(combo, firstItem, roles);
    }

    public void FillComboBoxWithCategories(ComboBox combo, string firstItem)
    {
        var categories = GetListFromQuery("SELECT CategoryName FROM Category", "CategoryName");
        FillComboBox(combo, firstItem, categories);
    }

    public void FillComboBoxWithStatuses(ComboBox combo, string firstItem)
    {
        var statuses = GetListFromQuery("SELECT StatusName FROM Status", "StatusName");
        FillComboBox(combo, firstItem, statuses);
    }

    public void FillComboBoxWithUsers(ComboBox combo, string firstItem)
    {
        var products = GetListFromQuery("SELECT ProductName FROM Product", "ProductName");
        FillComboBox(combo, firstItem, products);
    }

    public void FillComboBoxWithClients(ComboBox combo, string firstItem)
    {
        var users = GetListFromQuery(@"SELECT CONCAT(IFNULL(Surname,''), ' ', IFNULL(FirstName,''), ' ', IFNULL(MiddleName,'')) AS FullName FROM Users", "FullName");
        FillComboBox(combo, firstItem, users);
    }

    public void FillComboBoxWithProducts(ComboBox combo, string firstItem)
    {
        var clients = GetListFromQuery(@"SELECT CONCAT(IFNULL(Surname,''), ' ', IFNULL(FirstName,''), ' ', IFNULL(MiddleName,'')) AS FullName FROM Clients", "FullName");
        FillComboBox(combo, firstItem, clients);
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
            view.RowFilter = $"ProductName LIKE '{searchText}%'";
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
            string selectedRole = comboFilter.SelectedItem.ToString().Replace("'", "''");
            filters.Add($"RoleName = '{selectedRole}'");
        }

        string currentSearch = view.RowFilter;
        if (!string.IsNullOrEmpty(currentSearch))
        {
            filters.Add(currentSearch);
        }

        view.RowFilter = string.Join(" AND ", filters);
    }

    public void ApplyFilterProduct(ComboBox comboFilter)
    {
        List<string> filters = new List<string>();

        if (comboFilter.SelectedIndex > 0)
        {
            string selectedCategory = comboFilter.SelectedItem.ToString().Replace("'", "''");
            filters.Add("Category = '" + selectedCategory + "'");
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
            string selectedCategory = comboFilter.SelectedItem.ToString().Replace("'", "''");
            filters.Add($"StatusName = '{selectedCategory}'");
        }

        string currentSearch = view.RowFilter;
        if (!string.IsNullOrEmpty(currentSearch))
        {
            filters.Add(currentSearch);
        }

        view.RowFilter = string.Join(" AND ", filters);
    }

    public void ApplyFilterDirector(ComboBox comboFilter)
    {
        List<string> filters = new List<string>();

        if (comboFilter.SelectedIndex > 0)
        {
            string selectedCategory = comboFilter.SelectedItem.ToString().Replace("'", "''");
            filters.Add($"StatusName = '{selectedCategory}'");
        }

        string currentSearch = view.RowFilter;
        if (!string.IsNullOrEmpty(currentSearch))
        {
            filters.Add(currentSearch);
        }

        view.RowFilter = string.Join(" AND ", filters);
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
