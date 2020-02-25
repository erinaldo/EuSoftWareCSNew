using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Dictionary;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

using Medical.Yottor.UI.BLL;
using Medical.Yottor.UI.Entity;

namespace Medical.Yottor.UI.UI
{
    public partial class FrmEditSysQxUser : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private SysQxUserInfo tempInfo = new SysQxUserInfo();
    	
        public FrmEditSysQxUser()
        {
            InitializeComponent();
        }
                
        /// <summary>
        /// ʵ�ֿؼ�������ĺ���
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//Ĭ���ǿ���ͨ��

            #region MyRegion
            if (this.txtUserid.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("������");
                this.txtUserid.Focus();
                result = false;
            }
             else if (this.txtUsername.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("������");
                this.txtUsername.Focus();
                result = false;
            }
             else if (this.txtUserpwd.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("������");
                this.txtUserpwd.Focus();
                result = false;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// ��ʼ�������ֵ�
        /// </summary>
        private void InitDictItem()
        {
			//��ʼ������
        }                        

        /// <summary>
        /// ������ʾ�ĺ���
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//�����ֵ���أ����ã�

            if (!string.IsNullOrEmpty(ID))
            {
                #region ��ʾ��Ϣ
                SysQxUserInfo info = BLLFactory<SysQxUser>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
	                    txtUserid.Text = info.Userid;
           	                    txtUsername.Text = info.Username;
           	                    txtUserpwd.Text = info.Userpwd;
                             } 
                #endregion
                //this.btnOK.Enabled = HasFunction("SysQxUser/Edit");             
            }
            else
            {
   
                //this.btnOK.Enabled = Portal.gc.HasFunction("SysQxUser/Add");  
            }
            
            //tempInfo�ڶ��������Ϊָ�������½�����ȫ�µĶ��󣬵���һЩ��ʼ����GUID���ڸ����ϴ�
            //SetAttachInfo(tempInfo);
        }

        //private void SetAttachInfo(SysQxUserInfo info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = txtName.Text;
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, tempInfo.ID, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new SysQxUserInfo();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(SysQxUserInfo info)
        {
	            info.Userid = txtUserid.Text;
       	            info.Username = txtUsername.Text;
       	            info.Userpwd = txtUserpwd.Text;
               }
         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            SysQxUserInfo info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������
                //����Ƿ���������ͬ�ؼ��ֵļ�¼
                string condition = string.Format("userpwd ='{0}' ", info.Userpwd);
                bool exist = BLLFactory<SysQxUser>.Instance.IsExistRecord(condition);
                  if (exist)
                {
                    MessageDxUtil.ShowTips("ָ���ġ����Ѿ����ڣ������ظ���ӣ����޸�");
                    return false;
                }

                bool succeed = BLLFactory<SysQxUser>.Instance.Insert(info);
                if (succeed)
                {
                    //�����������������

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }                 

        /// <summary>
        /// �༭״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {
			//��鲻ͬID�Ƿ���������ͬ�ؼ��ֵļ�¼
			string condition = string.Format("userpwd ='{0}' and ID <> '{1}' ", this.txtUserpwd.Text, ID);
            bool exist = BLLFactory<SysQxUser>.Instance.IsExistRecord(condition);
             if (exist)
            {
                MessageDxUtil.ShowTips("ָ���ġ����Ѿ����ڣ������ظ���ӣ����޸�");
                return false;
            }

            SysQxUserInfo info = BLLFactory<SysQxUser>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<SysQxUser>.Instance.Update(info, info.ID);
                    if (succeed)
                    {
                        //�����������������
                       
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
           return false;
        }
    }
}
