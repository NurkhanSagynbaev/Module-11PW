using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11PW
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public struct CourierWaybill
    {
        public int WaybillId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public DateTime DeliveryDate { get; set; }
        // Другие свойства накладной

        public override string ToString()
        {
            return $"Waybill ID: {WaybillId}\nSender: {SenderName}\nReceiver: {ReceiverName}\nDelivery Date: {DeliveryDate}\n";
        }
    }

    public class WaybillManager
    {
        private List<CourierWaybill> waybills = new List<CourierWaybill>();

        public void CreateWaybill(CourierWaybill waybill)
        {
            // Логика создания накладной, например, добавление в базу данных
            waybills.Add(waybill);
        }

        public List<CourierWaybill> GetAllWaybills()
        {
            return waybills;
        }

        public CourierWaybill GetWaybillInfo(int waybillId)
        {
            var waybill = waybills.Find(w => w.WaybillId == waybillId);
            return waybill;
        }

        public void CopyWaybillFile(int waybillId, string destinationPath)
        {
            var waybill = waybills.Find(w => w.WaybillId == waybillId);
            if (waybill != null)
            {
                // Логика копирования файла, например, изображения накладной
                string sourceFilePath = $"Waybill_{waybillId}.txt";
                File.WriteAllText(sourceFilePath, waybill.ToString()); // Создаем текстовый файл с информацией о накладной
                File.Copy(sourceFilePath, destinationPath, true);
                File.Delete(sourceFilePath); // Удаляем временный файл
            }
        }
    }

    class Program
    {
        static void Main()
        {
            WaybillManager waybillManager = new WaybillManager();

            // Пример использования методов
            CourierWaybill waybill1 = new CourierWaybill
            {
                WaybillId = 1,
                SenderName = "John Doe",
                ReceiverName = "Jane Doe",
                DeliveryDate = DateTime.Now.AddDays(3)
                // Другие свойства
            };

            waybillManager.CreateWaybill(waybill1);

            List<CourierWaybill> allWaybills = waybillManager.GetAllWaybills();
            Console.WriteLine("All Waybills:");
            foreach (var wb in allWaybills)
            {
                Console.WriteLine(wb.ToString());
            }

            int waybillIdToRetrieve = 1;
            CourierWaybill retrievedWaybill = waybillManager.GetWaybillInfo(waybillIdToRetrieve);
            if (retrievedWaybill != null)
            {
                Console.WriteLine($"Waybill Info for ID {waybillIdToRetrieve}:\n{retrievedWaybill.ToString()}");
            }

            int waybillIdToCopy = 1;
            string destinationPath = "C:\\DestinationFolder\\Waybill_Copy.txt";
            waybillManager.CopyWaybillFile(waybillIdToCopy, destinationPath);
            Console.WriteLine($"Waybill file copied to {destinationPath}");
        }
    }

}
