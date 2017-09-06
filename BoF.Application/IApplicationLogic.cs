using System;
using System.Collections.Generic;
using BoF.Domain.Entities;
using System.Web.Security;

namespace BoF.Application
{
    public interface IApplicationLogic
    {
        T GetEntityById<T>(int Id);
        int AuthenticateUser(string Username, string Password);
        int getCustomerId(string Username);
        string getRole(int customerId);
        Customer CreateUser(Customer Customer);
        Account AddAccount(Account Account);
        AccountType AddAccountType(AccountType AccountType);
        IList<Customer> GetCustomerDetails();
        AccountType AddSimpleInterest(AccountType AccountType);
        decimal GetSimpleInterest();
        IList<Account> GetListofAccounts();
        IList<Customer> GetCustomerDetail(int id);
        IList<Account> GetAccountDetails(int id);
        int GetAccountId(int id);
        int GetTypeAccountId(int id);
        IList<AccountType> GetAccountTypeDetails(int id);
        AccountType AddSavingsInterest(AccountType AccountType);
        decimal GetSavingsInterest();
        Transaction AddTransaction(Transaction AccountType);
        //IList<Account> GetCustomerAccounts(int id);
        UserProfile InsertRole(UserProfile UserProfile);
        int GetaccountId(int id);
        void insertTransaction(decimal TransAmount, string TransDetails,  int Account_id, int TransTypeId);
        void updateAccountBalance(int Account_id, decimal TransAmount);
        void insertBillPay(int Account_id, string biller, string billerAccNo, string desc, decimal TransAmount, string nextRunDate, string Freq);
        //VatValue
        //VAT InsertVatValue(VAT Vat);
        //IList<ActiveDirectory> getDeptManagerList();
        //string getOPFNumber(int id);
        //string getDept(string username);
        //void updateTrasnferValue(int OPF_ID, string username);
        //void DeleteUSer(int id);
        //IList<ActiveDirectory> GetAutocompleteforAMNames(string deptName);
        //void InsertTrasnferDetails(int[] ids, string comments, string deptManager, string usernaem);
        //void updateCounterValue(int id, int value, string lastmodfiedby);
        //IList<CostingSheetDetail> getTotalTaxFree(int id);
        //IList<CostingSheetDetail> getTotalVIPFJD(int id);
        //IList<CostingSheetMaster> getCostMasterDetails_byOPFID(int id);
        //IList<InstallationDetails> getNSLTotal(int id);
        //void changeRowStatus(int id);
        //IList<Comments> getListOfComments(int id);
        //string GetGMName(int OPF_ID);
        //string ApproveOPF_byQA(int OPF_ID);
        //string ApproveOPF_byDM(int OPF_ID);
        //string ApproveOPF_byCFO(int OPF_ID);
        //string ApproveOPF_byGM(int OPF_ID);
        //IList<TransferTable> getTransferDetails();
        //IList<ActiveDirectory> GetListofAMs();
        //void RejectOPFs(int id);
        //void insertComment(string comment, String cstatus, int opfID, string user, DateTime date);
        //int GetCounterValue(int id);
        //int GetNSLMasterId(int costingSheetMasterid);
        //void DuplicateCostingSheetMaster(int id, string newcostingsheetnumber, DateTime date);
        //int getupdatedCostMasterID();
        //void DuplicateCostingSheetDetails(int id, int newCostingSheetMasterId, DateTime date);
        //void DuplicateInstallationMaster(int id, int newCostingSheetMasterId, DateTime date);
        //int getnewInstallationMasterId();
        //void DuplicateInstallationDetails(int installationMasterId, int newInstallationMasterId, DateTime date);
        //void MakeHistoryofOPF(int id);
        //IList<OPFMaster> getOPFDetails(int id);
        //IList<OPFDetail> GetListofIdsFromOPFDetails(int id);
        //ActiveDirectory UpdateADValue(ActiveDirectory er);
        //IList<ActiveDirectory> getListOfQA();
        //string GetQAName(int id);
        //int getGM();
        //int getCFO();
        //int getManager();
        //string getGMName();
        //string getCFOName();
        //string getManagerName(string username);
        //string GetEmail(string name);
        //IList<CostingSheetMaster> GetSavedCostingSheetMasterDetails(string CreatedBy);
        //IList<CostingSheetMaster> GetCostingSheetMasterDetails(int CostingSheetMasterId);
        //IList<CostingSheetDetail> GetCostingSheetDetails(int CostingSheetMasterId);
        ////CostingSheetMaster UpdateCostingSheetMasterDetails(CostingSheetMaster csm);
        // CostingSheetDetail UpdateCostingSheetDetails(CostingSheetDetail csd);
        //IList<CostingSheetMaster> SearchCostingSheetMaster(string costnum, DateTime? start, DateTime? end, string customername);
        //decimal GetVatValue();
        //int getCostMasterID();
        //int getInstallationMasterID();
        //FreightTable InsertFreightValue(FreightTable FT);
        //decimal GetFreightValue();
        //ExchangeRate UpdateRateValue(ExchangeRate er);
        //IList<ExchangeRate> GetListOfCurrency();
        //ExchangeRate InsertExchangeRateValue(ExchangeRate ER);
        //IList<ActiveDirectory> GetListofActiveDirectoryInfo();
        //IList<CostingSheetDetail> GetListOfCostingSheetDetails(List<int> ids);
        //IList<CostingSheetDetail> GetCostingSheetDetailInfo(int costMasterID);
        //IList<InstallationDetails> GetInstallationDetailInfo(int InstallationMasterID);
        //CostingSheetMaster UpdateCostingSheetMaster(CostingSheetMaster er);
        //SystemRoles AddUserRole(SystemRoles systemrole);
        //string getCostId();
        //IList<CostingSheetMaster> GetMasterID(int id);
        //IList<CostingSheetDetail> getTotalVIP(List<int> ids);
        //ActiveDirectory InsertActiveDirectoryDetails(ActiveDirectory activedirectory);
        //IList<SystemRoles> GetListofSystemRoles();
        //void saveOPFs(List<int> ids, int OPFId);
        //IList<SystemRoles> GetListofSRoles();
        //IList<ExchangeRate> GetListOfRoles();
        //decimal GetStatus(int id);
        //OPFMaster InsertOPFMaster(OPFMaster FT);
        //CostingSheetMaster InsertCostingSheetMasterDetails(CostingSheetMaster costingsheetmaster);
        //int getLastOPFNum();
        //CostingSheetDetail InsertCostingSheetDetails(CostingSheetDetail Costingsheetdetail);
        //InstallationDetails InsertInstallationDetails(InstallationDetails Installationdetails);
        //InstallationMaster InsertInstallationMaster(InstallationMaster Installationmaster);
        //int getMasterIdByCostingDetail(int id);
        //void saveCostingsheetmasterintoOPF(int costid, int opfid);
        //InstallationMaster UpdateInstallationMaster(InstallationMaster im);
        //void GetSavedOPFDetails(string CreatedBy);
        //string getDeptManager(string username);
        //string getAMEmail(int id);
        //void updatecounterstatus(int OPF_ID);
        //void updateCounterValuewhenediting(int id, int counter);
        //void updateDept_Status(int id);
        //void updateQA_Status(int id);
        //string GetRole(string username);
        //string GetFullName(string username);
        //string SavBoFSequenceNumber_Full(int id);
        //void UpdateCostingSheetDetails(int id, string salesNo, string upcCode, string VendorPartNum, string description, string Department_Location_Code, string warranty,
        //   string SupplierCompany_Country, string SupplierQuoteReferenceNumber, DateTime deliveryTime, string stockAvailable, int dimensions, int quantity, decimal listprice, decimal discountfactor, decimal DatecUnitBuyExGST, decimal datectotalbuy,
        //   decimal fcunit, decimal fctotal, decimal freight, decimal ITLevyCredit, decimal LC_Ex, decimal currency, decimal UnitLC_FJD, decimal TotalLC_FJD, decimal margin, decimal unitmargin, decimal totalmarginvalue, decimal unittax,
        //   decimal totaltaxfree, decimal duty, decimal vat, decimal unitvipfjd, decimal totalvipfjd);
        //int getUpdatedCostingSMasterID(int updateddetailid);
        //void UpdateInstallationDetails(int id, string type, int labour, int days, decimal hourspd, decimal hourrate, decimal linetotal);
        //int getUpdatedInstallationMasterID(int updateddetailid);
        //int getUpdatedCostingSheetMasterID(int updatedid);
        //void DeleteRecords(int id);
        //IList<OPFHistory> GetOPFHistory(int ids, int version);
        //IList<CostingSheetDetail> GetCostingSheetDetailInfoByCostDetailsID(int costMasterID);
        //int opfID(int id);

        //UserLoginDetails GetUserLoginDetailsByUserName(string userName);
        //UserLoginDetails AddUserLoginDetails(UserLoginDetails userLoginDetails);
        //UserLoginDetails UpdateUserLoginDetails(UserLoginDetails objUserLoginDetails);
        //int UpdateUserLoginDetailsByQuery(string query);

        //bool IsUserNameAlreadyExists(MembershipUser currentUser, string NewUsername);
        //bool ChangeUsername(string oldUsername, string newUsername);

        //IList<Patient> GetListofPatient();
        //IList<Patient> GetListofPatientByFilters(string NHN, string Name);
        //#endregion
    }
}
