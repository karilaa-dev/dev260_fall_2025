using System;

namespace FinalProject
{
    /// <summary>
    /// Represents a single item in the pantry inventory.
    /// Contains information about quantity, unit, category, expiration, and stock thresholds.
    /// </summary>
    public class PantryItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int LowStockThreshold { get; set; }
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Creates a new pantry item with the specified properties.
        /// </summary>
        public PantryItem(string name, int quantity, string unit, string category, DateTime? expirationDate = null, int lowStockThreshold = 2)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity;
            Unit = unit ?? "pieces";
            Category = category ?? "General";
            ExpirationDate = expirationDate;
            LowStockThreshold = lowStockThreshold;
            DateAdded = DateTime.Now;
        }

        /// <summary>
        /// Checks if the item is below or at the low stock threshold.
        /// </summary>
        public bool IsLowStock => Quantity <= LowStockThreshold;

        /// <summary>
        /// Checks if the item is expiring within the specified number of days.
        /// </summary>
        public bool IsExpiringSoon(int days = 7)
        {
            if (ExpirationDate == null) return false;
            return ExpirationDate.Value <= DateTime.Now.AddDays(days);
        }

        /// <summary>
        /// Checks if the item has already expired.
        /// </summary>
        public bool IsExpired => ExpirationDate.HasValue && ExpirationDate.Value < DateTime.Now;

        /// <summary>
        /// Returns a formatted string representation of the pantry item.
        /// </summary>
        public override string ToString()
        {
            string expInfo = ExpirationDate.HasValue 
                ? $"Expires: {ExpirationDate.Value:MMM dd, yyyy}" 
                : "No expiration";
            
            string stockWarning = IsLowStock ? " [LOW STOCK]" : "";
            string expiredWarning = IsExpired ? " [EXPIRED]" : "";
            
            return $"{Name} - {Quantity} {Unit} ({Category}) | {expInfo}{stockWarning}{expiredWarning}";
        }

        /// <summary>
        /// Returns a detailed multi-line display string for the item.
        /// </summary>
        public string ToDetailedString()
        {
            string expInfo = ExpirationDate.HasValue 
                ? ExpirationDate.Value.ToString("MMM dd, yyyy") 
                : "None";
            
            return $"  Name: {Name}\n" +
                   $"  Quantity: {Quantity} {Unit}\n" +
                   $"  Category: {Category}\n" +
                   $"  Expiration: {expInfo}\n" +
                   $"  Low Stock Threshold: {LowStockThreshold}\n" +
                   $"  Status: {(IsLowStock ? "LOW STOCK" : "OK")}{(IsExpired ? ", EXPIRED" : "")}";
        }
    }
}
