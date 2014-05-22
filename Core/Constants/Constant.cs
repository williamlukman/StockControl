namespace Core.Constant
{
    public class Constant
    {
        public class StockMutationItemCase
        {
            public static int Ready = 1;
            public static int PendingReceival = 2;
            public static int PendingDelivery = 3;
        }

        public class StockMutationStatus
        {
            public static int Addition = 1;
            public static int Deduction = 2;
        }

        public class SourceDocumentType
        {
            public static string PurchaseOrder = "PurchaseOrder";
            public static string PurchaseReceival = "PurchaseReceival";
            public static string SalesOrder = "SalesOrder";
            public static string DeliveryOrder = "DeliveryOrder";
        }

        public class SourceDocumentDetailType
        {
            public static string PurchaseOrderDetail = "PurchaseOrderDetail";
            public static string PurchaseReceivalDetail = "PurchaseReceivalDetail";
            public static string SalesOrderDetail = "SalesOrderDetail";
            public static string DeliveryOrderDetail = "DeliveryOrderDetail";

        }
    }
}