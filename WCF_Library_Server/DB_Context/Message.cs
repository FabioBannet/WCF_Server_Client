using System.ComponentModel.DataAnnotations;

namespace WCF_Library_Server.DB_Context
{
    /// <summary>
    /// Данный класс описывает диалог пользователя с другим пользователем
    /// </summary>
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        [Required]
        public string FromUser { get; set; }

        [Required]
        public string ToUser { get; set; }

        [Required]
        public string MessageData { get; set; }
    }


    // В общем мне подсказали как сделать.
    // Идея следующая - сообщения никчему не привзянны - мы с помощью простого запроса имеем к ним доступ
    // Проверку на дубликаты буду делать в методе отгрузки сообщеня на сервер
}
