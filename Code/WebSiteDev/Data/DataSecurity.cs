using System;
using System.Collections.Generic;
using System.Data;

namespace WebSiteDev
{
    public class DataSecurity
    {
        private Dictionary<int, string> originalLogins = new Dictionary<int, string>();
        private Dictionary<int, string> originalPhones = new Dictionary<int, string>();
        private Dictionary<int, string> originalFirstNames = new Dictionary<int, string>();
        private Dictionary<int, string> originalMiddleNames = new Dictionary<int, string>();
        private Dictionary<int, string> originalClientNames = new Dictionary<int, string>();
        private Dictionary<int, string> originalUserNames = new Dictionary<int, string>();

        public void LoadOriginalData(DataTable dt, string loginColumn = "UserLogin", string phoneColumn = "PhoneNumber", string firstNameColumn = "FirstName", string middleNameColumn = "MiddleName")
        {
            originalLogins.Clear();
            originalPhones.Clear();
            originalFirstNames.Clear();
            originalMiddleNames.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = -1;

                if (dt.Columns.Contains("ClientID"))
                {
                    id = Convert.ToInt32(dt.Rows[i]["ClientID"]);
                }
                else if (dt.Columns.Contains("UserID"))
                {
                    id = Convert.ToInt32(dt.Rows[i]["UserID"]);
                }
                else
                {
                    id = i;
                }

                if (dt.Columns.Contains(loginColumn))
                {
                    originalLogins[id] = dt.Rows[i][loginColumn].ToString();
                }
                if (dt.Columns.Contains(phoneColumn))
                {
                    originalPhones[id] = dt.Rows[i][phoneColumn].ToString();
                }
                if (dt.Columns.Contains(firstNameColumn))
                {
                    originalFirstNames[id] = dt.Rows[i][firstNameColumn].ToString();
                }
                if (dt.Columns.Contains(middleNameColumn))
                {
                    originalMiddleNames[id] = dt.Rows[i][middleNameColumn].ToString();
                }
            }
        }

        public void LoadOriginalClientNames(DataTable dt, string clientNameColumn = "ClientName")
        {
            originalClientNames.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int clientID = Convert.ToInt32(dt.Rows[i]["OrderID"]);

                if (dt.Columns.Contains(clientNameColumn))
                {
                    originalClientNames[clientID] = dt.Rows[i][clientNameColumn].ToString();
                }
            }
        }

        public void LoadOriginalUserNames(DataTable dt, string userNameColumn = "UserName")
        {
            originalUserNames.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int orderID = Convert.ToInt32(dt.Rows[i]["OrderID"]);

                if (dt.Columns.Contains(userNameColumn))
                {
                    originalUserNames[orderID] = dt.Rows[i][userNameColumn].ToString();
                }
            }
        }

        public void UpdateOriginalData(int clientID, DataRow row)
        {
            if (row.Table.Columns.Contains("PhoneNumber"))
            {
                originalPhones[clientID] = row["PhoneNumber"].ToString();
            }
            if (row.Table.Columns.Contains("FirstName"))
            {
                originalFirstNames[clientID] = row["FirstName"].ToString();
            }
            if (row.Table.Columns.Contains("MiddleName"))
            {
                originalMiddleNames[clientID] = row["MiddleName"].ToString();
            }
        }

        public string GetOriginalLogin(int id)
        {
            if (originalLogins.ContainsKey(id))
            {
                return originalLogins[id];
            }
            return null;
        }

        public string GetOriginalPhone(int id)
        {
            if (originalPhones.ContainsKey(id))
            {
                return originalPhones[id];
            }
            return null;
        }

        public string GetOriginalFirstName(int id)
        {
            if (originalFirstNames.ContainsKey(id))
            {
                return originalFirstNames[id];
            }
            return null;
        }

        public string GetOriginalMiddleName(int id)
        {
            if (originalMiddleNames.ContainsKey(id))
            {
                return originalMiddleNames[id];
            }
            return null;
        }

        public string GetOriginalClientName(int id)
        {
            if (originalClientNames.ContainsKey(id))
            {
                return originalClientNames[id];
            }
            return null;
        }

        public string GetOriginalUserName(int id)
        {
            if (originalUserNames.ContainsKey(id))
            {
                return originalUserNames[id];
            }
            return null;
        }

        public static string MaskLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return login;
            }
            return new string('•', login.Length);
        }

        public static string MaskName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            if (name.Length == 1)
            {
                return name;
            }
            return name[0] + new string('•', name.Length - 1);
        }

        public static string MaskPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length < 4)
            {
                return phone;
            }

            if (phone.Length >= 5)
            {
                string visible = phone.Substring(0, phone.Length - 5);
                string masked = "••-••";
                return visible + masked;
            }

            string visiblePart = phone.Substring(0, phone.Length - 4);
            string maskedPart = new string('•', 4);
            return visiblePart + maskedPart;
        }

        public static string MaskClientName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return fullName;
            }

            string[] parts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return fullName;
            }

            string surname = parts[0];
            string firstInitial = "";
            string middleInitial = "";

            if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
            {
                firstInitial = parts[1][0] + ".";
            }

            if (parts.Length > 2 && !string.IsNullOrEmpty(parts[2]))
            {
                middleInitial = parts[2][0] + ".";
            }

            return surname + " " + firstInitial + middleInitial;
        }

        public static string MaskUserName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return fullName;
            }

            string[] parts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return fullName;
            }

            string surname = parts[0];
            string firstInitial = "";
            string middleInitial = "";

            if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
            {
                firstInitial = parts[1][0] + ".";
            }

            if (parts.Length > 2 && !string.IsNullOrEmpty(parts[2]))
            {
                middleInitial = parts[2][0] + ".";
            }

            return surname + " " + firstInitial + middleInitial;
        }

        public void Clear()
        {
            originalLogins.Clear();
            originalPhones.Clear();
            originalFirstNames.Clear();
            originalMiddleNames.Clear();
            originalClientNames.Clear();
            originalUserNames.Clear();
        }
    }
}
