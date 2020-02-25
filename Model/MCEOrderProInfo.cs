using System;
namespace EuSoft.Model
{
    /// <summary>
    /// MCEOrderProInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class MCEOrderProInfo
    {
        public MCEOrderProInfo()
        { }
        #region Model
        private int _id;
        private string _orderno;
        private string _procatalogno;
        private string _prodescription;
        private decimal? _prosize;
        private string _prounit;
        private int? _proquantity;
        private decimal? _proamount;
        private string _procurrency;
        private DateTime? _produnon;
        private string _pronote;
        private string _prolibraryid;
        private string _productstatus;
        private string _productprocess;
        private DateTime? _updatetime;
        private string _person;
        private DateTime? _tasktime;
        private string _stockstatus = "";
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProCatalogNo
        {
            set { _procatalogno = value; }
            get { return _procatalogno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProDescription
        {
            set { _prodescription = value; }
            get { return _prodescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ProSize
        {
            set { _prosize = value; }
            get { return _prosize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProUnit
        {
            set { _prounit = value; }
            get { return _prounit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProQuantity
        {
            set { _proquantity = value; }
            get { return _proquantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ProAmount
        {
            set { _proamount = value; }
            get { return _proamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProCurrency
        {
            set { _procurrency = value; }
            get { return _procurrency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ProDunOn
        {
            set { _produnon = value; }
            get { return _produnon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProNote
        {
            set { _pronote = value; }
            get { return _pronote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProLibraryID
        {
            set { _prolibraryid = value; }
            get { return _prolibraryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductProcess
        {
            set { _productprocess = value; }
            get { return _productprocess; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Person
        {
            set { _person = value; }
            get { return _person; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TaskTime
        {
            set { _tasktime = value; }
            get { return _tasktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StockStatus
        {
            set { _stockstatus = value; }
            get { return _stockstatus; }
        }
        #endregion Model

    }
}

