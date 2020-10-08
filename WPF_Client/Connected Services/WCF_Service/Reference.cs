﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_Client.WCF_Service {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF_Service.IWCF_Service", CallbackContract=typeof(WPF_Client.WCF_Service.IWCF_ServiceCallback))]
    public interface IWCF_Service {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Service/RemoveUser", ReplyAction="http://tempuri.org/IWCF_Service/RemoveUserResponse")]
        bool RemoveUser(string userName, string WhoTryDelete);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Service/RemoveUser", ReplyAction="http://tempuri.org/IWCF_Service/RemoveUserResponse")]
        System.Threading.Tasks.Task<bool> RemoveUserAsync(string userName, string WhoTryDelete);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWCF_Service/SendMessage")]
        void SendMessage(string FromUserLogin, string ToLogin, string messageData);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWCF_Service/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string FromUserLogin, string ToLogin, string messageData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Service/Authorisation", ReplyAction="http://tempuri.org/IWCF_Service/AuthorisationResponse")]
        WPF_Client.WCF_Service.AuthorisationResponse Authorisation(WPF_Client.WCF_Service.AuthorisationRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Service/Authorisation", ReplyAction="http://tempuri.org/IWCF_Service/AuthorisationResponse")]
        System.Threading.Tasks.Task<WPF_Client.WCF_Service.AuthorisationResponse> AuthorisationAsync(WPF_Client.WCF_Service.AuthorisationRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Service/Registration", ReplyAction="http://tempuri.org/IWCF_Service/RegistrationResponse")]
        bool Registration(string userName, bool gender, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Service/Registration", ReplyAction="http://tempuri.org/IWCF_Service/RegistrationResponse")]
        System.Threading.Tasks.Task<bool> RegistrationAsync(string userName, bool gender, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWCF_ServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWCF_Service/MessageCallBack")]
        void MessageCallBack(string answer);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Authorisation", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class AuthorisationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string login;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string password;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public System.Collections.Generic.Dictionary<string, string> messages;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public System.Collections.Generic.Dictionary<string, bool> usersInDB;
        
        public AuthorisationRequest() {
        }
        
        public AuthorisationRequest(string login, string password, System.Collections.Generic.Dictionary<string, string> messages, System.Collections.Generic.Dictionary<string, bool> usersInDB) {
            this.login = login;
            this.password = password;
            this.messages = messages;
            this.usersInDB = usersInDB;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AuthorisationResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class AuthorisationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool AuthorisationResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public System.Collections.Generic.Dictionary<string, string> messages;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public System.Collections.Generic.Dictionary<string, bool> usersInDB;
        
        public AuthorisationResponse() {
        }
        
        public AuthorisationResponse(bool AuthorisationResult, System.Collections.Generic.Dictionary<string, string> messages, System.Collections.Generic.Dictionary<string, bool> usersInDB) {
            this.AuthorisationResult = AuthorisationResult;
            this.messages = messages;
            this.usersInDB = usersInDB;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWCF_ServiceChannel : WPF_Client.WCF_Service.IWCF_Service, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WCF_ServiceClient : System.ServiceModel.DuplexClientBase<WPF_Client.WCF_Service.IWCF_Service>, WPF_Client.WCF_Service.IWCF_Service {
        
        public WCF_ServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public WCF_ServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public WCF_ServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public WCF_ServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public WCF_ServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool RemoveUser(string userName, string WhoTryDelete) {
            return base.Channel.RemoveUser(userName, WhoTryDelete);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveUserAsync(string userName, string WhoTryDelete) {
            return base.Channel.RemoveUserAsync(userName, WhoTryDelete);
        }
        
        public void SendMessage(string FromUserLogin, string ToLogin, string messageData) {
            base.Channel.SendMessage(FromUserLogin, ToLogin, messageData);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string FromUserLogin, string ToLogin, string messageData) {
            return base.Channel.SendMessageAsync(FromUserLogin, ToLogin, messageData);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WPF_Client.WCF_Service.AuthorisationResponse WPF_Client.WCF_Service.IWCF_Service.Authorisation(WPF_Client.WCF_Service.AuthorisationRequest request) {
            return base.Channel.Authorisation(request);
        }
        
        public bool Authorisation(string login, string password, ref System.Collections.Generic.Dictionary<string, string> messages, ref System.Collections.Generic.Dictionary<string, bool> usersInDB) {
            WPF_Client.WCF_Service.AuthorisationRequest inValue = new WPF_Client.WCF_Service.AuthorisationRequest();
            inValue.login = login;
            inValue.password = password;
            inValue.messages = messages;
            inValue.usersInDB = usersInDB;
            WPF_Client.WCF_Service.AuthorisationResponse retVal = ((WPF_Client.WCF_Service.IWCF_Service)(this)).Authorisation(inValue);
            messages = retVal.messages;
            usersInDB = retVal.usersInDB;
            return retVal.AuthorisationResult;
        }
        
        public System.Threading.Tasks.Task<WPF_Client.WCF_Service.AuthorisationResponse> AuthorisationAsync(WPF_Client.WCF_Service.AuthorisationRequest request) {
            return base.Channel.AuthorisationAsync(request);
        }
        
        public bool Registration(string userName, bool gender, string password) {
            return base.Channel.Registration(userName, gender, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegistrationAsync(string userName, bool gender, string password) {
            return base.Channel.RegistrationAsync(userName, gender, password);
        }
    }
}
