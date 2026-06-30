namespace MM.Optiyol.Api.Constants
{
    public static class OptiyolHookEventNames
    {
        /// <summary>
        /// Rota planlandı. (Dağıtıma hazır rota planının rota kuryesi/şoförüne atamasının yapıldığını belirten aksiyondur)
        /// </summary>
        public const string RoutePlanned = "route.planned";

        /// <summary>
        /// Rota başlatılmadan önce rota için yapılan yükleme listesi tamamlandı.
        /// </summary>
        public const string RouteLoadListCompleted = "route.load.list.completed";

        /// <summary>
        /// Rota başlatıldı.
        /// </summary>
        public const string RouteStarted = "route.started";

        /// <summary>
        /// Alım ve Teslim servis tipindeki sipariş yerine vardı. Sipariş servisi tipi alım için pickup veya teslim için delivery olabilir.
        /// </summary>
        public const string OrderArrived = "order.arrived";

        /// <summary>
        /// Sipariş teslim edildi.
        /// </summary>
        public const string OrderCompleted = "order.completed";

        /// <summary>
        /// Sipariş kalemlerine sahip siparişin tamamlama işlemi gerçekleştirildi.
        /// </summary>
        public const string OrderCompletedWithItems = "order.completed.with.items";

        /// <summary>
        /// Sipariş iptal edildi.
        /// </summary>
        public const string OrderCanceled = "order.canceled";

        /// <summary>
        /// Sipariş kalemlerine sahip siparişin iptal işlemi gerçekleştirildi.
        /// </summary>
        public const string OrderCanceledWithItems = "order.canceled.with.items";

        /// <summary>
        /// Sipariş iptali geri alındı.
        /// </summary>
        public const string OrderUndoCanceled = "order.undo.canceled";

        /// <summary>
        /// Sipariş iade edildi.
        /// </summary>
        public const string OrderReturned = "order.returned";

        /// <summary>
        /// Rota bitirildi.
        /// </summary>
        public const string RouteFinished = "route.finished";

        /// <summary>
        /// Sipariş alma sanal tamamlandı
        /// </summary>
        public const string OrderPickupVirtualCompleted = "order.pickup.virtual.completed";
    }
}
