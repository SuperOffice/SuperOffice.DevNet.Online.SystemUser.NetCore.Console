﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoSystemUserService
{
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30514-0828")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TokenType", Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1")]
    public enum TokenType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Jwt = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Saml = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30514-0828")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1", ConfigurationName="SoSystemUserService.IPartnerSystemUserService")]
    public interface IPartnerSystemUserService
    {
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.superoffice.com/superid/partnersystemuser/0.1/IPartnerSystemUserServic" +
            "e/Authenticate", ReplyAction="http://www.superoffice.com/superid/partnersystemuser/0.1/IPartnerSystemUserServic" +
            "e/AuthenticateResponse")]
        System.Threading.Tasks.Task<SoSystemUserService.AuthenticationResponse> AuthenticateAsync(SoSystemUserService.AuthenticationRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30514-0828")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AuthenticationRequest", WrapperNamespace="http://www.superoffice.com/superid/partnersystemuser/0.1", IsWrapped=true)]
    public partial class AuthenticationRequest
    {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1")]
        public string ApplicationToken;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1")]
        public string ContextIdentifier;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1", Order=0)]
        public string SignedSystemToken;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1", Order=1)]
        public SoSystemUserService.TokenType ReturnTokenType;
        
        public AuthenticationRequest()
        {
        }
        
        public AuthenticationRequest(string ApplicationToken, string ContextIdentifier, string SignedSystemToken, SoSystemUserService.TokenType ReturnTokenType)
        {
            this.ApplicationToken = ApplicationToken;
            this.ContextIdentifier = ContextIdentifier;
            this.SignedSystemToken = SignedSystemToken;
            this.ReturnTokenType = ReturnTokenType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30514-0828")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AuthenticationResponse", WrapperNamespace="http://www.superoffice.com/superid/partnersystemuser/0.1", IsWrapped=true)]
    public partial class AuthenticationResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1", Order=0)]
        public bool IsSuccessful;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1", Order=1)]
        public string Token;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.superoffice.com/superid/partnersystemuser/0.1", Order=2)]
        public string ErrorMessage;
        
        public AuthenticationResponse()
        {
        }
        
        public AuthenticationResponse(bool IsSuccessful, string Token, string ErrorMessage)
        {
            this.IsSuccessful = IsSuccessful;
            this.Token = Token;
            this.ErrorMessage = ErrorMessage;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30514-0828")]
    public interface IPartnerSystemUserServiceChannel : SoSystemUserService.IPartnerSystemUserService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1-preview-30514-0828")]
    public partial class PartnerSystemUserServiceClient : System.ServiceModel.ClientBase<SoSystemUserService.IPartnerSystemUserService>, SoSystemUserService.IPartnerSystemUserService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public PartnerSystemUserServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(PartnerSystemUserServiceClient.GetBindingForEndpoint(endpointConfiguration), PartnerSystemUserServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PartnerSystemUserServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PartnerSystemUserServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PartnerSystemUserServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(PartnerSystemUserServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PartnerSystemUserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<SoSystemUserService.AuthenticationResponse> AuthenticateAsync(SoSystemUserService.AuthenticationRequest request)
        {
            return base.Channel.AuthenticateAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IPartnerSystemUserService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IPartnerSystemUserService1))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IPartnerSystemUserService))
            {
                return new System.ServiceModel.EndpointAddress(SuperOffice.DevNet.Online.SystemUser.NetCore.ConsoleApp.Constants.SystemUserEndpoint);
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IPartnerSystemUserService1))
            {
                return new System.ServiceModel.EndpointAddress(SuperOffice.DevNet.Online.SystemUser.NetCore.ConsoleApp.Constants.SystemUserEndpoint);
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IPartnerSystemUserService,
            
            BasicHttpBinding_IPartnerSystemUserService1,
        }
    }
}