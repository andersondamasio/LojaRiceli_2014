﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.18444
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _1_WebForm.Painel2.correios.ServiceReferenceCorreios {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="correios.ServiceReferenceCorreios.CalcPrecoPrazoWSSoap")]
    public interface CalcPrecoPrazoWSSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrecoPrazo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrecoPrazo(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrecoPrazo", ReplyAction="*")]
        System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoPrazoAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrecoPrazoData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrecoPrazoData(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrecoPrazoData", ReplyAction="*")]
        System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoPrazoDataAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPreco", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPreco(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPreco", ReplyAction="*")]
        System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrecoData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrecoData(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrecoData", ReplyAction="*")]
        System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoDataAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrazo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrazo(string nCdServico, string sCepOrigem, string sCepDestino);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrazo", ReplyAction="*")]
        System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrazoAsync(string nCdServico, string sCepOrigem, string sCepDestino);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrazoData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrazoData(string nCdServico, string sCepOrigem, string sCepDestino, string sDtCalculo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalcPrazoData", ReplyAction="*")]
        System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrazoDataAsync(string nCdServico, string sCepOrigem, string sCepDestino, string sDtCalculo);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class cResultado : object, System.ComponentModel.INotifyPropertyChanged {
        
        private cServico[] servicosField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public cServico[] Servicos {
            get {
                return this.servicosField;
            }
            set {
                this.servicosField = value;
                this.RaisePropertyChanged("Servicos");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class cServico : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int codigoField;
        
        private string valorField;
        
        private string prazoEntregaField;
        
        private string valorMaoPropriaField;
        
        private string valorAvisoRecebimentoField;
        
        private string valorValorDeclaradoField;
        
        private string entregaDomiciliarField;
        
        private string entregaSabadoField;
        
        private string erroField;
        
        private string msgErroField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
                this.RaisePropertyChanged("Codigo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
                this.RaisePropertyChanged("Valor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string PrazoEntrega {
            get {
                return this.prazoEntregaField;
            }
            set {
                this.prazoEntregaField = value;
                this.RaisePropertyChanged("PrazoEntrega");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string ValorMaoPropria {
            get {
                return this.valorMaoPropriaField;
            }
            set {
                this.valorMaoPropriaField = value;
                this.RaisePropertyChanged("ValorMaoPropria");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string ValorAvisoRecebimento {
            get {
                return this.valorAvisoRecebimentoField;
            }
            set {
                this.valorAvisoRecebimentoField = value;
                this.RaisePropertyChanged("ValorAvisoRecebimento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string ValorValorDeclarado {
            get {
                return this.valorValorDeclaradoField;
            }
            set {
                this.valorValorDeclaradoField = value;
                this.RaisePropertyChanged("ValorValorDeclarado");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string EntregaDomiciliar {
            get {
                return this.entregaDomiciliarField;
            }
            set {
                this.entregaDomiciliarField = value;
                this.RaisePropertyChanged("EntregaDomiciliar");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string EntregaSabado {
            get {
                return this.entregaSabadoField;
            }
            set {
                this.entregaSabadoField = value;
                this.RaisePropertyChanged("EntregaSabado");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string Erro {
            get {
                return this.erroField;
            }
            set {
                this.erroField = value;
                this.RaisePropertyChanged("Erro");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string MsgErro {
            get {
                return this.msgErroField;
            }
            set {
                this.msgErroField = value;
                this.RaisePropertyChanged("MsgErro");
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
    public interface CalcPrecoPrazoWSSoapChannel : _1_WebForm.Painel2.correios.ServiceReferenceCorreios.CalcPrecoPrazoWSSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalcPrecoPrazoWSSoapClient : System.ServiceModel.ClientBase<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.CalcPrecoPrazoWSSoap>, _1_WebForm.Painel2.correios.ServiceReferenceCorreios.CalcPrecoPrazoWSSoap {
        
        public CalcPrecoPrazoWSSoapClient() {
        }
        
        public CalcPrecoPrazoWSSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalcPrecoPrazoWSSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalcPrecoPrazoWSSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalcPrecoPrazoWSSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrecoPrazo(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento) {
            return base.Channel.CalcPrecoPrazo(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento);
        }
        
        public System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoPrazoAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento) {
            return base.Channel.CalcPrecoPrazoAsync(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento);
        }
        
        public _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrecoPrazoData(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo) {
            return base.Channel.CalcPrecoPrazoData(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento, sDtCalculo);
        }
        
        public System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoPrazoDataAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo) {
            return base.Channel.CalcPrecoPrazoDataAsync(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento, sDtCalculo);
        }
        
        public _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPreco(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento) {
            return base.Channel.CalcPreco(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento);
        }
        
        public System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento) {
            return base.Channel.CalcPrecoAsync(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento);
        }
        
        public _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrecoData(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo) {
            return base.Channel.CalcPrecoData(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento, sDtCalculo);
        }
        
        public System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrecoDataAsync(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento, string sDtCalculo) {
            return base.Channel.CalcPrecoDataAsync(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento, sDtCalculo);
        }
        
        public _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrazo(string nCdServico, string sCepOrigem, string sCepDestino) {
            return base.Channel.CalcPrazo(nCdServico, sCepOrigem, sCepDestino);
        }
        
        public System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrazoAsync(string nCdServico, string sCepOrigem, string sCepDestino) {
            return base.Channel.CalcPrazoAsync(nCdServico, sCepOrigem, sCepDestino);
        }
        
        public _1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado CalcPrazoData(string nCdServico, string sCepOrigem, string sCepDestino, string sDtCalculo) {
            return base.Channel.CalcPrazoData(nCdServico, sCepOrigem, sCepDestino, sDtCalculo);
        }
        
        public System.Threading.Tasks.Task<_1_WebForm.Painel2.correios.ServiceReferenceCorreios.cResultado> CalcPrazoDataAsync(string nCdServico, string sCepOrigem, string sCepDestino, string sDtCalculo) {
            return base.Channel.CalcPrazoDataAsync(nCdServico, sCepOrigem, sCepDestino, sDtCalculo);
        }
    }
}
