using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

public class DataManipulation
{
    private DataTable data;
    private DataView view;

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
            string selectedCategory = comboFilter.SelectedItem.ToString().Replace("'", "''");
            filters.Add($"RoleName = '{selectedCategory}'");
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
            filters.Add($"Category = '{selectedCategory}'");
        }

        string currentSearch = view.RowFilter;
        if (!string.IsNullOrEmpty(currentSearch))
        {
            filters.Add(currentSearch);
        }

        view.RowFilter = string.Join(" AND ", filters);
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
