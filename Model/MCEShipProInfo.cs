using System;
namespace EuSoft.Model
{
    /// <summary>
    /// MCEShipProInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class MCEShipProInfo
    {
        public MCEShipProInfo()
        { }
        #region Model
        private int _id;
        private string _orderno;
        private string _trackingno;
        private string _shipcatalogno;
        private string _shipcsno;
        private decimal? _shipsize;
        private string _shipunit;
        private string _shipvalcode;
        private string _shipbatchno;
        private string _shipnote;
        private string _shiplibraryid;
        private string _orginalid;
        private string _flag;
        private DateTime? _updatetime;
        private string _person;
        private string _stockstatus = "";
        private DateTime? rkrq;

        public DateTime? Rkrq
        {
            get { return rkrq; }
            set { rkrq = value; }
        }
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
        public string TrackingNo
        {
            set { _trackingno = value; }
            get { return _trackingno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipCatalogNo
        {
            set { _shipcatalogno = value; }
            get { return _shipcatalogno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipCSNo
        {
            set { _shipcsno = value; }
            get { return _shipcsno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ShipSize
        {
            set { _shipsize = value; }
            get { return _shipsize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipUnit
        {
            set { _shipunit = value; }
            get { return _shipunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipValCode
        {
            set { _shipvalcode = value; }
            get { return _shipvalcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipBatchNo
        {
            set { _shipbatchno = value; }
            get { return _shipbatchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipNote
        {
            set { _shipnote = value; }
            get { return _shipnote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShipLibraryID
        {
            set { _shiplibraryid = value; }
            get { return _shiplibraryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrginalID
        {
            set { _orginalid = value; }
            get { return _orginalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
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
        public string StockStatus
        {
            set { _stockstatus = value; }
            get { return _stockstatus; }
        }
        #endregion Model

    }
}

