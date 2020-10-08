using System.Collections.Generic;
using System.ServiceModel;

namespace WCF_Library_Server
{
    [ServiceContract(CallbackContract = typeof(IWCF_ServiceChatCallBack))]
    public interface IWCF_Service
    {

        [OperationContract]
        bool RemoveUser(string userName);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string FromUserLogin, string ToLogin, string messageData);

        [OperationContract]
        // я в курсе, что коллекция - с-сылочный тип, но почему-то без явного указания передачи по с-сылке ничего не передаётся
        bool Authorisation(string login, string password, ref IDictionary<string, string> messages, ref IDictionary<string, bool> usersInDB);

        [OperationContract]
        bool Registration(string userName, bool gender, string password);

    }

    public interface IWCF_ServiceChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string answer);
    }
}
