using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Diagnostics;

namespace WebSiteDev
{
    public static class FolderPermissions
    {
        public static void InitializeImagesFolder()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string imagesFolder = Path.Combine(appData, "WebShop", "Images");

                System.Diagnostics.Debug.WriteLine($"Инициализирую папку: {imagesFolder}");

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                    System.Diagnostics.Debug.WriteLine($"Папка создана: {imagesFolder}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Папка уже существует: {imagesFolder}");
                }

                if (!HasFullAccess(imagesFolder))
                {
                    GiveFullAccessToFolder(imagesFolder);
                    System.Diagnostics.Debug.WriteLine($"Права выданы для: {imagesFolder}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Права уже выданы для: {imagesFolder}");
                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Diagnostics.Debug.WriteLine("Нет прав администратора для выдачи прав доступа!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка инициализации папки: {ex.Message}");
            }
        }

        public static bool CreateFolderWithFullAccess(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    System.Diagnostics.Debug.WriteLine($"Папка создана: {folderPath}");
                }

                if (!HasFullAccess(folderPath))
                {
                    GiveFullAccessToFolder(folderPath);
                    System.Diagnostics.Debug.WriteLine($"Права выданы для: {folderPath}");
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка создания папки: {ex.Message}");
                return false;
            }
        }

        private static bool HasFullAccess(string folderPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                foreach (FileSystemAccessRule rule in directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                {
                    if (rule.IdentityReference.Value == "S-1-1-0" &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.FullControl) &&
                        rule.AccessControlType == AccessControlType.Allow)
                    {
                        System.Diagnostics.Debug.WriteLine($"Найдены полные права для: {folderPath}");
                        return true;
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Полные права не найдены для: {folderPath}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка проверки прав: {ex.Message}");
                return false;
            }
        }

        private static void GiveFullAccessToFolder(string folderPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                FileSystemAccessRule accessRule = new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );

                directorySecurity.AddAccessRule(accessRule);

                directoryInfo.SetAccessControl(directorySecurity);

                System.Diagnostics.Debug.WriteLine($"Права успешно выданы: {folderPath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                System.Diagnostics.Debug.WriteLine($"UnauthorizedAccessException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при выдаче прав: {ex.Message}");
                throw;
            }
        }
    }
}