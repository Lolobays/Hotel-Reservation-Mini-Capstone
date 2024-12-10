using System;
using System.Windows.Forms;

namespace TRABYAHE
{
    internal class Placeholder
    {
        // User information
        public static string TransactionID { get; set; }
        public static string FullName { get; set; }
        public static string EmailAddress { get; set; }
        public static string ContactNumber { get; set; }
        public static string Address { get; set; }
        public static string Gender { get; set; }

        // Room details
        public static string RoomID { get; set; }
        public static string RoomName { get; set; }
        public static string RoomType { get; set; }
        public static string RoomNumber { get; set; }
        public static string RoomPrice { get; set; }
        public static string RoomDescription { get; set; }

        // Reservation details
        public static string Booking_ID { get; set; }
        public static DateTime CheckIn { get; set; } = DateTime.Now;
        public static DateTime CheckOut { get; set; } = DateTime.Now.AddDays(1);
        public static string TotalAmount { get; set; }

        //Payment Method
        public static string PaymentMethod { get; set; }

        // Update TotalAmount to a Label
        public static void UpdateTotalAmount(Label lblTotal)
        {
            lblTotal.Text = TotalAmount ?? "₱0.00";
        }

        //Button Disabled
        public static bool IsEnabledBtnSignIn { get; set; }
    }
}