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

        public void LoadOriginalData(DataTable dt, string loginColumn = "UserLogin",
            string phoneColumn = "PhoneNumber", string firstNameColumn = "FirstName",
            string middleNameColumn = "MiddleName")
        {
            originalLogins.Clear();
            originalPhones.Clear();
            originalFirstNames.Clear();
            originalMiddleNames.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Columns.Contains(loginColumn))
                {
                    originalLogins[i] = dt.Rows[i][loginColumn].ToString();
                }
                if (dt.Columns.Contains(phoneColumn))
                {
                    originalPhones[i] = dt.Rows[i][phoneColumn].ToString();
                }
                if (dt.Columns.Contains(firstNameColumn))
                {
                    originalFirstNames[i] = dt.Rows[i][firstNameColumn].ToString();
                }
                if (dt.Columns.Contains(middleNameColumn))
                {
                    originalMiddleNames[i] = dt.Rows[i][middleNameColumn].ToString();
                }
            }
        }

        public void LoadOriginalClientNames(DataTable dt, string clientNameColumn = "ClientName")
        {
            originalClientNames.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Columns.Contains(clientNameColumn))
                {
                    originalClientNames[i] = dt.Rows[i][clientNameColumn].ToString();
                }
            }
        }

        public void LoadOriginalUserNames(DataTable dt, string userNameColumn = "UserName")
        {
            originalUserNames.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Columns.Contains(userNameColumn))
                {
                    originalUserNames[i] = dt.Rows[i][userNameColumn].ToString();
                }
            }
        }

        public string GetOriginalLogin(int rowIndex)
        {
            if (originalLogins.ContainsKey(rowIndex))
            {
                return originalLogins[rowIndex];
            }
            return null;
        }

        public string GetOriginalPhone(int rowIndex)
        {
            if (originalPhones.ContainsKey(rowIndex))
            {
                return originalPhones[rowIndex];
            }
            return null;
        }

        public string GetOriginalFirstName(int rowIndex)
        {
            if (originalFirstNames.ContainsKey(rowIndex))
            {
                return originalFirstNames[rowIndex];
            }
            return null;
        }

        public string GetOriginalMiddleName(int rowIndex)
        {
            if (originalMiddleNames.ContainsKey(rowIndex))
            {
                return originalMiddleNames[rowIndex];
            }
            return null;
        }

        public string GetOriginalClientName(int rowIndex)
        {
            if (originalClientNames.ContainsKey(rowIndex))
            {
                return originalClientNames[rowIndex];
            }
            return null;
        }

        public string GetOriginalUserName(int rowIndex)
        {
            if (originalUserNames.ContainsKey(rowIndex))
            {
                return originalUserNames[rowIndex];
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
