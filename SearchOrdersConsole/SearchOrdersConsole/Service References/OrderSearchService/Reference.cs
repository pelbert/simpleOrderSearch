﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchOrdersConsole.OrderSearchService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchOrderViewModel", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class SearchOrderViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int OrderIdField;
        
        private int MSAField;
        
        private System.DateTime CompletionDteField;
        
        private int StatusField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int OrderId {
            get {
                return this.OrderIdField;
            }
            set {
                if ((this.OrderIdField.Equals(value) != true)) {
                    this.OrderIdField = value;
                    this.RaisePropertyChanged("OrderId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int MSA {
            get {
                return this.MSAField;
            }
            set {
                if ((this.MSAField.Equals(value) != true)) {
                    this.MSAField = value;
                    this.RaisePropertyChanged("MSA");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public System.DateTime CompletionDte {
            get {
                return this.CompletionDteField;
            }
            set {
                if ((this.CompletionDteField.Equals(value) != true)) {
                    this.CompletionDteField = value;
                    this.RaisePropertyChanged("CompletionDte");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Orders", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Orders : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int OrderIdField;
        
        private int ShipperIdField;
        
        private int DriverIdField;
        
        private System.DateTime CompletionDteField;
        
        private int StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        private int MSAField;
        
        private float DurationField;
        
        private int OfferTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int OrderId {
            get {
                return this.OrderIdField;
            }
            set {
                if ((this.OrderIdField.Equals(value) != true)) {
                    this.OrderIdField = value;
                    this.RaisePropertyChanged("OrderId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int ShipperId {
            get {
                return this.ShipperIdField;
            }
            set {
                if ((this.ShipperIdField.Equals(value) != true)) {
                    this.ShipperIdField = value;
                    this.RaisePropertyChanged("ShipperId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public int DriverId {
            get {
                return this.DriverIdField;
            }
            set {
                if ((this.DriverIdField.Equals(value) != true)) {
                    this.DriverIdField = value;
                    this.RaisePropertyChanged("DriverId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public System.DateTime CompletionDte {
            get {
                return this.CompletionDteField;
            }
            set {
                if ((this.CompletionDteField.Equals(value) != true)) {
                    this.CompletionDteField = value;
                    this.RaisePropertyChanged("CompletionDte");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public int Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public int MSA {
            get {
                return this.MSAField;
            }
            set {
                if ((this.MSAField.Equals(value) != true)) {
                    this.MSAField = value;
                    this.RaisePropertyChanged("MSA");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=7)]
        public float Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=8)]
        public int OfferType {
            get {
                return this.OfferTypeField;
            }
            set {
                if ((this.OfferTypeField.Equals(value) != true)) {
                    this.OfferTypeField = value;
                    this.RaisePropertyChanged("OfferType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrderSearchService.OrderSearchServiceSoap")]
    public interface OrderSearchServiceSoap {
        
        // CODEGEN: Generating message contract since element name model from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SearchOrder", ReplyAction="*")]
        SearchOrdersConsole.OrderSearchService.SearchOrderResponse SearchOrder(SearchOrdersConsole.OrderSearchService.SearchOrderRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SearchOrder", ReplyAction="*")]
        System.Threading.Tasks.Task<SearchOrdersConsole.OrderSearchService.SearchOrderResponse> SearchOrderAsync(SearchOrdersConsole.OrderSearchService.SearchOrderRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SearchOrderRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SearchOrder", Namespace="http://tempuri.org/", Order=0)]
        public SearchOrdersConsole.OrderSearchService.SearchOrderRequestBody Body;
        
        public SearchOrderRequest() {
        }
        
        public SearchOrderRequest(SearchOrdersConsole.OrderSearchService.SearchOrderRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SearchOrderRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SearchOrdersConsole.OrderSearchService.SearchOrderViewModel model;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int page;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int pageSize;
        
        public SearchOrderRequestBody() {
        }
        
        public SearchOrderRequestBody(SearchOrdersConsole.OrderSearchService.SearchOrderViewModel model, int page, int pageSize) {
            this.model = model;
            this.page = page;
            this.pageSize = pageSize;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SearchOrderResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SearchOrderResponse", Namespace="http://tempuri.org/", Order=0)]
        public SearchOrdersConsole.OrderSearchService.SearchOrderResponseBody Body;
        
        public SearchOrderResponse() {
        }
        
        public SearchOrderResponse(SearchOrdersConsole.OrderSearchService.SearchOrderResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SearchOrderResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SearchOrdersConsole.OrderSearchService.Orders[] SearchOrderResult;
        
        public SearchOrderResponseBody() {
        }
        
        public SearchOrderResponseBody(SearchOrdersConsole.OrderSearchService.Orders[] SearchOrderResult) {
            this.SearchOrderResult = SearchOrderResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface OrderSearchServiceSoapChannel : SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderSearchServiceSoapClient : System.ServiceModel.ClientBase<SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap>, SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap {
        
        public OrderSearchServiceSoapClient() {
        }
        
        public OrderSearchServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderSearchServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderSearchServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderSearchServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SearchOrdersConsole.OrderSearchService.SearchOrderResponse SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap.SearchOrder(SearchOrdersConsole.OrderSearchService.SearchOrderRequest request) {
            return base.Channel.SearchOrder(request);
        }
        
        public SearchOrdersConsole.OrderSearchService.Orders[] SearchOrder(SearchOrdersConsole.OrderSearchService.SearchOrderViewModel model, int page, int pageSize) {
            SearchOrdersConsole.OrderSearchService.SearchOrderRequest inValue = new SearchOrdersConsole.OrderSearchService.SearchOrderRequest();
            inValue.Body = new SearchOrdersConsole.OrderSearchService.SearchOrderRequestBody();
            inValue.Body.model = model;
            inValue.Body.page = page;
            inValue.Body.pageSize = pageSize;
            SearchOrdersConsole.OrderSearchService.SearchOrderResponse retVal = ((SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap)(this)).SearchOrder(inValue);
            return retVal.Body.SearchOrderResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SearchOrdersConsole.OrderSearchService.SearchOrderResponse> SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap.SearchOrderAsync(SearchOrdersConsole.OrderSearchService.SearchOrderRequest request) {
            return base.Channel.SearchOrderAsync(request);
        }
        
        public System.Threading.Tasks.Task<SearchOrdersConsole.OrderSearchService.SearchOrderResponse> SearchOrderAsync(SearchOrdersConsole.OrderSearchService.SearchOrderViewModel model, int page, int pageSize) {
            SearchOrdersConsole.OrderSearchService.SearchOrderRequest inValue = new SearchOrdersConsole.OrderSearchService.SearchOrderRequest();
            inValue.Body = new SearchOrdersConsole.OrderSearchService.SearchOrderRequestBody();
            inValue.Body.model = model;
            inValue.Body.page = page;
            inValue.Body.pageSize = pageSize;
            return ((SearchOrdersConsole.OrderSearchService.OrderSearchServiceSoap)(this)).SearchOrderAsync(inValue);
        }
    }
}
