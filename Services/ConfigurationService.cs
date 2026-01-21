using UnattendBuilder.Web.Models;

namespace UnattendBuilder.Web.Services
{
    public class ConfigurationService
    {
        public UnattendConfig Config { get; private set; } = new UnattendConfig();

        public event Action? OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();

        public void Reset()
        {
            Config = new UnattendConfig();
            NotifyStateChanged();
        }

        public void AddUserAccount()
        {
            Config.UserAccounts.Add(new UserAccount
            {
                Username = $"User{Config.UserAccounts.Count + 1}",
                Password = "",
                IsAdmin = Config.UserAccounts.Count == 0, // First user is admin by default
                IsEnabled = true
            });
            NotifyStateChanged();
        }

        public void RemoveUserAccount(int index)
        {
            if (index >= 0 && index < Config.UserAccounts.Count)
            {
                Config.UserAccounts.RemoveAt(index);
                NotifyStateChanged();
            }
        }

        public (bool IsValid, List<string> Errors) Validate()
        {
            var errors = new List<string>();

            // Check for empty usernames in enabled accounts
            foreach (var account in Config.UserAccounts.Where(a => a.IsEnabled))
            {
                if (string.IsNullOrWhiteSpace(account.Username))
                {
                    errors.Add("One or more user accounts have empty usernames.");
                    break;
                }
            }

            // Check product key for placeholder
            if (Config.ProductKey == "00000-00000-00000-00000-00000")
            {
                errors.Add("Using placeholder product key - you will need to edit the XML with your actual key.");
            }

            // Check BitLocker settings
            if (Config.EnableBitLocker)
            {
                if (Config.BitLockerProtectionMethod == "TPMAndPIN" && string.IsNullOrEmpty(Config.BitLockerPIN))
                {
                    errors.Add("BitLocker TPM+PIN selected but no PIN provided. User will be prompted during setup.");
                }
            }

            return (errors.Count == 0, errors);
        }

        public List<string> GetWarnings()
        {
            var warnings = new List<string>();

            if (Config.UserAccounts.Count == 0)
            {
                warnings.Add("No user accounts configured. Windows will prompt during installation.");
            }

            if (Config.UserAccounts.Any(a => string.IsNullOrWhiteSpace(a.Password)))
            {
                warnings.Add("One or more accounts have empty passwords.");
            }

            if (Config.UseKMSKey)
            {
                warnings.Add("Using KMS key - Windows will activate against your KMS server.");
            }

            return warnings;
        }

        public string GenerateXml()
        {
            return XmlGenerator.GenerateUnattendXml(Config);
        }

        public void UpdateProductKey()
        {
            if (Config.UseKMSKey)
            {
                Config.ProductKey = Config.Edition switch
                {
                    "Home" => "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99",
                    "Education" => "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2",
                    "LTSC" => "M7XTQ-FN8P6-TTKYV-9D4CC-J462D",
                    "IoT LTSC" => "QPM6N-7J2WJ-P88HH-P3YRH-YY74H",
                    _ => "W269N-WFGWX-YVC9B-4J6C9-T83GX" // Pro
                };
            }
            else
            {
                Config.ProductKey = Config.Edition switch
                {
                    "Home" => "YTMG3-N6DKC-DKB77-7M9GH-8HVX7",
                    "Education" => "YNMGQ-8RYV3-4PGQ3-C8XTP-7CFBY",
                    "LTSC" => "00000-00000-00000-00000-00000",
                    "IoT LTSC" => "00000-00000-00000-00000-00000",
                    _ => "VK7JG-NPHTM-C97JM-9MPGT-3V66T" // Pro
                };
            }
            NotifyStateChanged();
        }
    }
}
