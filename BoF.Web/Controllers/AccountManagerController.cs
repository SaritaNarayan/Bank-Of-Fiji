//using BoF.Application;
//using NHibernate;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using BoF.Web.Helpers;
//using BoF.Web.Models.Mappers;
//using BoF.Web.Models;
//using BoF.Domain.Entities;
//using System.Data;
//using BoF.Web.Models.Security;

//namespace BoF.Web.Controllers
//{
//    public class AccountManagerController : Controller
//    {
//        Database_Access_Layer.db dblayer = new Database_Access_Layer.db();
//        Database_Access_Layer.OPF OPFLayer = new Database_Access_Layer.OPF();

//        private readonly ISession session;
//        public IApplicationLogic applicationlogic { get; set; }
//        private readonly CostingSheetMasterMapper CostingSheetMasterMapper;
//        private readonly CostingSheetDetailMapper CostingSheetDetailMapper;
//        private readonly InstallationDetailMapper InstallationDetailMapper;
//        private readonly InstallationMasterMapper InstallationMasterMapper;
//        private readonly OPFMasterMapper OPFMasterMapper;
//        public AccountManagerController(ISession session)
//        {
//            this.session = session;

//            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }
//            CostingSheetMasterMapper = new CostingSheetMasterMapper(session);
//            CostingSheetDetailMapper = new CostingSheetDetailMapper(session);
//            InstallationDetailMapper = new InstallationDetailMapper(session);
//            InstallationMasterMapper = new InstallationMasterMapper(session);
//            OPFMasterMapper = new OPFMasterMapper(session);
//        }

//        // GET: AccountManager

//        public ActionResult Index(RateManagementModel rtmm)
//        {
//            //if (Session["Role"] == null)
//            //{
//            //    return RedirectToAction("Index", "Logon");
//            //}
//            //else
//            //{
//            //    var role = Session["Role"].ToString();

//            //if (role.Equals("Account Manager"))
//            //{
//            if (Session["fullname"] != null)
//            {
//                IApplicationLogic app = new ApplicationLogic(session);
//            RateManagementModel RTM = new RateManagementModel();
//            var value = app.GetVatValue();
//            var freight = app.GetFreightValue();
//            //var systemrolemodel = new SystemRolesModel();
//            //var currency = new List<SelectList>();
//            //   currency= app.GetListOfRoles();
//            //IList<string> ERModel = new List<string>();
//            //foreach (var _currency in currency)
//            //{
//            //    var curent = _currency.ToString();
//            //    ERModel.Add(curent);
//            //}

//            var NSLDropdown = new SelectList(new[]
//            {
//                new { ID = "1", Name = "-- Please Select --"},
//                new { ID = "Project Management", Name = "Project Management"},
//                new { ID = "Design", Name = "Design"},
//                new { ID = "Install", Name = "Install"},
//                new { ID = "Testing & Training", Name = "Testing & Training"},
//                new { ID = "Cutover/ Commissions", Name = "Cutover/ Commissions"},
//                new { ID = "Transport", Name = "Transport"},
//                new { ID = "Meals", Name = "Meals"},
//                new { ID = "Accomodation", Name = "Accomodation"},

//            }, "ID", "Name", 1);

//            var StatusDropDown = new SelectList(new[]
//           {
//                new { ID = "1", Name = "Status"},
//                new { ID = "Pending", Name = "Pending"},
//                new { ID = "Approved", Name = "Approved"},
//                new { ID = "Rejected", Name = "Rejected"},

//            }, "ID", "Name", 1);

//            var sysmodel = new ExchangeRateModel();
//            sysmodel.GetRole = getRoleSelectList();
//            RTM.ExchangeRateModel = sysmodel;


//            ViewData["ViewVatValue"] = value;
//            //ViewData["ViewCurrency"] = new SelectList(currency);

//            //ViewBag.LocationList = freight;
//            ViewData["ViewFreightValue"] = freight;
//            //ViewData["ViewCSDetails"] = CSModel;
//            ViewData["NSLDropdown"] = NSLDropdown;
//            ViewData["StatusDropDown"] = StatusDropDown;
//            return View("Index", RTM);

//            }
//            else
//                return RedirectToAction("Index", "Logon");
//            //}
//            //    else
//            //    {
//            //        return RedirectToAction("Index", "Logon");
//            //    }
//            //}
//        }

//        public ActionResult GetCounterValue(int id)
//        {
//            int counter = applicationlogic.GetCounterValue(id);
//            return Json(new
//            {
//                DivID = counter
//            }
//               , JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult KillSessions()
//        {
//            Session.Remove("Counter");
//            Session.Remove("mystring");
//            Session.Remove("installCounter");
//            Session.Remove("CostingsheetMasterForTotal");

//            var message = "Success";
//            return Json(new
//            {
//                DivID = message
//            }
//               , JsonRequestBehavior.AllowGet);
//        }
       
//        public ActionResult MakeHistoryofOPF(int id)
//        {
//            applicationlogic.MakeHistoryofOPF(id);
//            var counter = applicationlogic.GetCounterValue(id);
//            applicationlogic.updateCounterValuewhenediting(id,counter);
//            var message = "Success";
//            return Json(new
//            {
//                DivID = message
//            }
//               , JsonRequestBehavior.AllowGet);
//        }
//        public SelectList getRoleSelectList()
//        {
//            IApplicationLogic app = new ApplicationLogic(session);
//            var roles = app.GetListOfRoles();

//            ExchangeRate sr = new ExchangeRate();
//            sr.Id = -1;
//            sr.Country = "--Currency--";
//            roles.Add(sr);

//            IEnumerable<ExchangeRate> sortedRoles = roles.OrderBy(d => d.Id);
//            IList<ExchangeRate> _sortedRoles = sortedRoles.ToList();
//            return new SelectList(_sortedRoles, "Id", "Country");
//        }

//        public SelectList getQASelectList()
//        {
//            IApplicationLogic app = new ApplicationLogic(session);
//            var roles = app.getListOfQA();

//            ActiveDirectory sr = new ActiveDirectory();
//            sr.Id = -1;
//            sr.Username = "--Quality Assurance--";
//            roles.Add(sr);

//            IEnumerable<ActiveDirectory> sortedRoles = roles.OrderBy(d => d.Id);

//            IList<ActiveDirectory> _sortedRoles = sortedRoles.ToList();
//            _sortedRoles.Count();
//            return new SelectList(_sortedRoles, "Id", "Username");

//        }

//        public ActionResult FetchStatus(int RoleId)
//        {
//            IApplicationLogic applLogic = new ApplicationLogic(session);
//            var TreatmentFty = applLogic.GetStatus(RoleId);

//            return Json(new
//            {
//                DivID = TreatmentFty
//            }, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult FetchQAName(int Id)
//        {
//            IApplicationLogic applLogic = new ApplicationLogic(session);
//            var TreatmentFty = applLogic.GetQAName(Id);

//            return Json(new
//            {
//                DivID = TreatmentFty
//            }, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public ActionResult AddCostingSheetMasterDetails(RateManagementModel RateManagementModel)
//        {
//            IApplicationLogic applicationlogic = new ApplicationLogic(session);
//            var CostingSheetMasterMapper = new CostingSheetMasterMapper(session);
//            RateManagementModel RTM = new RateManagementModel();
//            var counter = Session["Counter"];
//            var costingsheetnum = Session["mystring"];
//            var costingSheetmasterModel = new CostingSheetMasterModel();
//            var costingSheetmasterentity = new CostingSheetMaster();
//            var costingSheetmasterentityd = new CostingSheetMaster();
            
//            var username = Session["Username"].ToString();
//            if (counter == null)
//            {
//                var costID = applicationlogic.getCostId();
//                int x = Convert.ToInt32(costID);
//                var newCostID = x + 1;
//                string myString = newCostID.ToString();
//                RateManagementModel.CostingSheetMasterModel.CostingSheetNumber = myString;

//                Session["mystring"] = myString;
//                var CostingSheetMasterEntity = CostingSheetMasterMapper.CostingSheetMasterToMopdelCostingSheetMaster(RateManagementModel.CostingSheetMasterModel);
//                CostingSheetMasterEntity.CreatedBy = Session["Username"].ToString();
//                CostingSheetMasterEntity.LastModifiedBy = Session["Username"].ToString();
//                costingSheetmasterentity = applicationlogic.InsertCostingSheetMasterDetails(CostingSheetMasterEntity);

//                Session["Counter"] = costingSheetmasterentity.Id;
//                costingSheetmasterModel = CostingSheetMasterMapper.CostingSheetMasterToCostingSheetMasterModel(costingSheetmasterentity);

//                var CostingSheetDetailMappper = new CostingSheetDetailMapper(session);

//                //int costMasterID = applicationlogic.getCostMasterID(myString);
//                RateManagementModel.CostingSheetDetailModel.CostingSheetMaster = costingSheetmasterModel;
//                var CostingSheetDetailEntity = CostingSheetDetailMapper.CostingSheetDetailToCostingSheetDetailModel(RateManagementModel.CostingSheetDetailModel, costingSheetmasterentity);
//                //CostingSheetDetailEntity.CostingSheetMaster.Id = Session["Counter"];
//                applicationlogic.InsertCostingSheetDetails(CostingSheetDetailEntity);

//            }
//            else
//            {
//                //update
//                //CostingSheetMaster csm = new CostingSheetMaster();
//                //csm = applicationlogic.GetEntityById<CostingSheetMaster>(Convert.ToInt32(counter));
//                RateManagementModel.CostingSheetMasterModel.ID = Convert.ToInt32(counter);
               
//                RateManagementModel.CostingSheetMasterModel.CostingSheetNumber = Session["mystring"].ToString();
//                var csm = CostingSheetMasterMapper.CostingSheetMasterToMopdelCostingSheetMaster(RateManagementModel.CostingSheetMasterModel);
//                csm.CreatedBy = Session["Username"].ToString(); 
//                csm.LastModifiedBy = Session["Username"].ToString();
//                costingSheetmasterentity = applicationlogic.UpdateCostingSheetMaster(csm);
//                costingSheetmasterModel = CostingSheetMasterMapper.CostingSheetMasterToCostingSheetMasterModel(costingSheetmasterentity);

//                var CostingSheetDetailMappper = new CostingSheetDetailMapper(session);

//                //int costMasterID = applicationlogic.getCostMasterID(myString);
//                RateManagementModel.CostingSheetDetailModel.CostingSheetMaster = costingSheetmasterModel;
//                var CostingSheetDetailEntity = CostingSheetDetailMapper.CostingSheetDetailToCostingSheetDetailModel(RateManagementModel.CostingSheetDetailModel, costingSheetmasterentity);
//                //CostingSheetDetailEntity.CostingSheetMaster.Id = Session["Counter"];
//                applicationlogic.InsertCostingSheetDetails(CostingSheetDetailEntity);

//            }

//            IList<CostingSheetDetailModel> CSModel = new List<CostingSheetDetailModel>();
//            int costMasterID = Convert.ToInt32(Session["Counter"]);

//            var costingsheetDetails = applicationlogic.GetCostingSheetDetailInfo(costMasterID);

//            foreach (var _costingsheetDetails in costingsheetDetails)
//            {
//                var costingsheetdetailModel = CostingSheetDetailMapper.CostingSheetDetailModelToCostingSheetDetail(_costingsheetDetails);
//                CSModel.Add(costingsheetdetailModel);
//            }
//            List<string> list = new List<string>();
//            foreach (var item in CSModel)
//            {
//                var a = item.ID.ToString();
//                list.Add(a);
//                var b = item.SalesNo;
//                list.Add(b);
//                var c = item.UPCCode;
//                list.Add(c);
//                var d = item.VendorPartNum;
//                list.Add(d);
//                var e = item.Description;
//                list.Add(e);
//                var ee = item.Department_Location_Code;
//                list.Add(ee);
//                var f = item.Warranty;
//                list.Add(f);
//                var g = item.SupplierCompany_Country;
//                list.Add(g);
//                var h = item.SupplierQuoteReferenceNumber;
//                list.Add(h);
//                var i = item.DeliveryTime.ToString();
//                list.Add(i);
//                var j = item.StockAvailable;
//                list.Add(j);
//                var k = item.Dimensions.ToString();
//                list.Add(k);
//                var l = item.Quantity.ToString();
//                list.Add(l);
//                var m = item.ListPrice.ToString();
//                list.Add(m);
//                var n = item.DiscountFactor.ToString();
//                list.Add(n);
//                var o = item.DatecUnitBuyExGST;
//                list.Add(o);
//                var p = item.DatecTotalBuy.ToString();
//                list.Add(p);
//                var r = item.FCUnit.ToString();
//                list.Add(r);
//                var s = item.FCTotal.ToString();
//                list.Add(s);
//                var q = item.Freight.ToString();
//                list.Add(q);
//                var t = item.ITLevyCredit.ToString();
//                list.Add(t);
//                var u = item.LC_Ex.ToString();
//                list.Add(u);
//                var v = item.Currency.ToString();
//                list.Add(v);
//                var w = item.UnitLC_FJD.ToString();
//                list.Add(w);
//                var x = item.TotalLC_FJD.ToString();
//                list.Add(x);
//                var y = item.Margin.ToString();
//                list.Add(y);
//                var z = item.UnitMargin.ToString();
//                list.Add(z);
//                var aa = item.TotalMarginValue.ToString();
//                list.Add(aa);
//                var ab = item.UnitTax.ToString();
//                list.Add(ab);
//                var ac = item.TotalTaxFree.ToString();
//                list.Add(ac);
//                var ad = item.Duty.ToString();
//                list.Add(ad);
//                var ae = item.VAT.ToString();
//                list.Add(ae);
//                var af = item.UnitVIPFJD.ToString();
//                list.Add(af);
//                var ag = item.TotalVIPFJD.ToString();
//                list.Add(ag);
//            }

//            //  ViewData["ViewCSDetails"] = CSModel;
//            int costingSheetMasterId = Convert.ToInt32(Session["Counter"]);
//            var totaltaxfree = applicationlogic.getTotalTaxFree(costingSheetMasterId);
//            var totaltaxFree = totaltaxfree[0].TotalTaxFree;

//            var totalvipfjd = applicationlogic.getTotalVIPFJD(costingSheetMasterId);
//            var totalvipFjd = totalvipfjd[0].TotalVIPFJD;

//            var message = CSModel;
//            return Json(new
//            {
//                DivID = list,
//                TotalTaxFree = totaltaxFree,
//                TotalVipFjd = totalvipFjd
//            }
//                , JsonRequestBehavior.AllowGet);
//        }
    
    


//    public ActionResult UpdateCostingSheetDetails(RateManagementModel rsm)
//        {
//            var id = rsm.CostingSheetDetailModel.ID;
//            var salesNo = rsm.CostingSheetDetailModel.SalesNo;
//            var upcCode = rsm.CostingSheetDetailModel.UPCCode;
//            var vendorPart = rsm.CostingSheetDetailModel.VendorPartNum;
//            var description = rsm.CostingSheetDetailModel.Description;
//            var departmentlocationCode = rsm.CostingSheetDetailModel.Department_Location_Code;
//            var warranty = rsm.CostingSheetDetailModel.Warranty;
//            var suppliercomapany = rsm.CostingSheetDetailModel.SupplierCompany_Country;
//            var supplierquote = rsm.CostingSheetDetailModel.SupplierQuoteReferenceNumber;
//            var deliveryTime = rsm.CostingSheetDetailModel.DeliveryTime;
//            var stockAvailable = rsm.CostingSheetDetailModel.StockAvailable;
//            var dimensions = rsm.CostingSheetDetailModel.Dimensions;
//            var quantity = rsm.CostingSheetDetailModel.Quantity;
//            var listprice = rsm.CostingSheetDetailModel.ListPrice;
//            var discountfactor = rsm.CostingSheetDetailModel.DiscountFactor;
//            var datecUnitbuy = rsm.CostingSheetDetailModel.DatecUnitBuyExGST;
//            var datectotalbuy = rsm.CostingSheetDetailModel.DatecTotalBuy;
//            var fcunit = rsm.CostingSheetDetailModel.FCUnit;
//            var fctotal = rsm.CostingSheetDetailModel.FCTotal;
//            var freight = rsm.CostingSheetDetailModel.Freight;
//            var itlevy = rsm.CostingSheetDetailModel.ITLevyCredit;
//            var lcex = rsm.CostingSheetDetailModel.LC_Ex;
//            var currency = rsm.CostingSheetDetailModel.Currency;
//            var unitlcfjd = rsm.CostingSheetDetailModel.UnitLC_FJD;
//            var totallcfjd = rsm.CostingSheetDetailModel.TotalLC_FJD;
//            var margin = rsm.CostingSheetDetailModel.Margin;
//            var unitmargin = rsm.CostingSheetDetailModel.UnitMargin;
//            var totalmarginvalue = rsm.CostingSheetDetailModel.TotalMarginValue;
//            var unittax = rsm.CostingSheetDetailModel.UnitTax;
//            var totaltaxfree = rsm.CostingSheetDetailModel.TotalTaxFree;
//            var duty = rsm.CostingSheetDetailModel.Duty;
//            var vat = rsm.CostingSheetDetailModel.VAT;
//            var unitvipfjd = rsm.CostingSheetDetailModel.UnitVIPFJD;
//            var totalvipfjd = rsm.CostingSheetDetailModel.TotalVIPFJD;

//            var datecunitbuy = Convert.ToDecimal(datecUnitbuy);

//            applicationlogic.UpdateCostingSheetDetails(id, salesNo, upcCode, vendorPart, description, departmentlocationCode, warranty,
//            suppliercomapany, supplierquote, deliveryTime, stockAvailable, dimensions, quantity, listprice, discountfactor, datecunitbuy, datectotalbuy,
//            fcunit, fctotal, freight, itlevy, lcex, currency, unitlcfjd, totallcfjd, margin, unitmargin, totalmarginvalue, unittax,
//            totaltaxfree, duty, vat, unitvipfjd, totalvipfjd);

//            var updateddetailid = rsm.CostingSheetDetailModel.ID;
//            var updatedid = applicationlogic.getUpdatedCostingSMasterID(updateddetailid);


//            return RedirectToAction("CostingSheet", "AccountManager", new { id = updatedid });
//        }
//        public ActionResult UpdateCostingSheetDetailsFromOPF(RateManagementModel rsm)
//        {
//            var id = rsm.CostingSheetDetailModel.ID;
//            var salesNo = rsm.CostingSheetDetailModel.SalesNo;
//            var upcCode = rsm.CostingSheetDetailModel.UPCCode;
//            var vendorPart = rsm.CostingSheetDetailModel.VendorPartNum;
//            var description = rsm.CostingSheetDetailModel.Description;
//            var departmentlocationCode = rsm.CostingSheetDetailModel.Department_Location_Code;
//            var warranty = rsm.CostingSheetDetailModel.Warranty;
//            var suppliercomapany = rsm.CostingSheetDetailModel.SupplierCompany_Country;
//            var supplierquote = rsm.CostingSheetDetailModel.SupplierQuoteReferenceNumber;
//            var deliveryTime = rsm.CostingSheetDetailModel.DeliveryTime;
//            var stockAvailable = rsm.CostingSheetDetailModel.StockAvailable;
//            var dimensions = rsm.CostingSheetDetailModel.Dimensions;
//            var quantity = rsm.CostingSheetDetailModel.Quantity;
//            var listprice = rsm.CostingSheetDetailModel.ListPrice;
//            var discountfactor = rsm.CostingSheetDetailModel.DiscountFactor;
//            var datecUnitbuy = rsm.CostingSheetDetailModel.DatecUnitBuyExGST;
//            var datectotalbuy = rsm.CostingSheetDetailModel.DatecTotalBuy;
//            var fcunit = rsm.CostingSheetDetailModel.FCUnit;
//            var fctotal = rsm.CostingSheetDetailModel.FCTotal;
//            var freight = rsm.CostingSheetDetailModel.Freight;
//            var itlevy = rsm.CostingSheetDetailModel.ITLevyCredit;
//            var lcex = rsm.CostingSheetDetailModel.LC_Ex;
//            var currency = rsm.CostingSheetDetailModel.Currency;
//            var unitlcfjd = rsm.CostingSheetDetailModel.UnitLC_FJD;
//            var totallcfjd = rsm.CostingSheetDetailModel.TotalLC_FJD;
//            var margin = rsm.CostingSheetDetailModel.Margin;
//            var unitmargin = rsm.CostingSheetDetailModel.UnitMargin;
//            var totalmarginvalue = rsm.CostingSheetDetailModel.TotalMarginValue;
//            var unittax = rsm.CostingSheetDetailModel.UnitTax;
//            var totaltaxfree = rsm.CostingSheetDetailModel.TotalTaxFree;
//            var duty = rsm.CostingSheetDetailModel.Duty;
//            var vat = rsm.CostingSheetDetailModel.VAT;
//            var unitvipfjd = rsm.CostingSheetDetailModel.UnitVIPFJD;
//            var totalvipfjd = rsm.CostingSheetDetailModel.TotalVIPFJD;

//            var datecunitbuy = Convert.ToDecimal(datecUnitbuy);

//            applicationlogic.UpdateCostingSheetDetails(id, salesNo, upcCode, vendorPart, description, departmentlocationCode, warranty,
//            suppliercomapany, supplierquote, deliveryTime, stockAvailable, dimensions, quantity, listprice, discountfactor, datecunitbuy, datectotalbuy,
//            fcunit, fctotal, freight, itlevy, lcex, currency, unitlcfjd, totallcfjd, margin, unitmargin, totalmarginvalue, unittax,
//            totaltaxfree, duty, vat, unitvipfjd, totalvipfjd);

//            var updateddetailid = rsm.CostingSheetDetailModel.ID;
//            var updatedid = applicationlogic.getUpdatedCostingSMasterID(updateddetailid);
//            var opfmID = applicationlogic.opfID(updatedid);
//            return RedirectToAction("OPF", "ApprovingChannel", new { id = opfmID });
//        }
//        public ActionResult UpdateInstallationDetails(RateManagementModel IDetails)
//        {
//            var id = IDetails.InstallationDetailModel.ID;
//            var type = IDetails.InstallationDetailModel.Type;
//            var labour = IDetails.InstallationDetailModel.Labour;
//            var days = IDetails.InstallationDetailModel.Days;
//            var hourspd = IDetails.InstallationDetailModel.HoursPD;
//            var hourrate = IDetails.InstallationDetailModel.HourRate;
//            var linetotal = IDetails.InstallationDetailModel.LineTotal;

//            applicationlogic.UpdateInstallationDetails(id, type, labour, days, hourspd, hourrate, linetotal);
//            var updateddetailid = IDetails.InstallationDetailModel.ID;
//            var updatedid = applicationlogic.getUpdatedInstallationMasterID(updateddetailid);
//            var costingSheetMasterId = applicationlogic.getUpdatedCostingSheetMasterID(updatedid);

//            return RedirectToAction("CostingSheet", "AccountManager", new { id = costingSheetMasterId });
//        }
//        public ActionResult AddNSLDetails(RateManagementModel RateManagementModel)
//        {
//            {
//                IApplicationLogic applicationlogic = new ApplicationLogic(session);
//                var counter = Session["Counter"];
//                var installCounter = Session["installCounter"];
//                if (installCounter == null)
//                {
//                    if(counter == null)
//                    {
//                        var costingSheetmasterentity = new CostingSheetMaster();
//                        var costID = applicationlogic.getCostId();
//                        int x = Convert.ToInt32(costID);
//                        var newCostID = x + 1;
//                        string myString = newCostID.ToString();
//                        RateManagementModel.CostingSheetMasterModel.CostingSheetNumber = myString;
//                        RateManagementModel.CostingSheetMasterModel.CostingSheetNumber = myString;

//                        Session["mystring"] = myString;
//                        var CostingSheetMasterEntity = CostingSheetMasterMapper.CostingSheetMasterToMopdelCostingSheetMaster(RateManagementModel.CostingSheetMasterModel);
//                        CostingSheetMasterEntity.CreatedBy = Session["Username"].ToString();
//                        CostingSheetMasterEntity.LastModifiedBy = Session["Username"].ToString();
//                        costingSheetmasterentity = applicationlogic.InsertCostingSheetMasterDetails(CostingSheetMasterEntity);

//                        Session["Counter"] = costingSheetmasterentity.Id;
//                        counter = costingSheetmasterentity.Id;
//                    }
//                    else
//                    {//update costing sheet master details
//                        var costingSheetmasterentity = new CostingSheetMaster();
//                        RateManagementModel.CostingSheetMasterModel.ID = Convert.ToInt32(counter);

//                        RateManagementModel.CostingSheetMasterModel.CostingSheetNumber = Session["mystring"].ToString();
//                        var csm = CostingSheetMasterMapper.CostingSheetMasterToMopdelCostingSheetMaster(RateManagementModel.CostingSheetMasterModel);
//                        csm.CreatedBy = Session["Username"].ToString();
//                        csm.LastModifiedBy = Session["Username"].ToString();
//                        costingSheetmasterentity = applicationlogic.UpdateCostingSheetMaster(csm);
//                    }

//                    var InstallationMasterMapper = new InstallationMasterMapper(session);

//                    InstallationMaster im = new InstallationMaster();
//                    //var installationMasterModel = new InstallationMasterModel();
//                    int SotingSeetMAster_id = Convert.ToInt32(counter);
//                    var costing = new CostingSheetMaster();
//                    var costingmodal = new CostingSheetMasterModel();
//                    costing = applicationlogic.GetEntityById<CostingSheetMaster>(SotingSeetMAster_id);
//                    // costingmodal = CostingSheetMasterMapper.CostingSheetMasterToCostingSheetMasterModel(costing);
//                    //  RateManagementModel.InstallationMasterModel.CostingSheetMaster = costingmodal;

//                    var installationMastermodel = InstallationMasterMapper.InstallationMasterModelToInstallationMaster(RateManagementModel.InstallationMasterModel, costing);

//                    var Installationmasterentity = applicationlogic.InsertInstallationMaster(installationMastermodel);
//                    Session["installCounter"] = Installationmasterentity.Id;
//                    var Installationmastermodel = InstallationMasterMapper.InstallationMasterToInstallationMasterModel(Installationmasterentity);

//                    var InstallationDetailMapper = new InstallationDetailMapper(session);
//                    RateManagementModel.InstallationDetailModel.InstallationMaster = Installationmastermodel;
//                    var InstallationDetailEntity = InstallationDetailMapper.InstallationDetailsModelToInstallationDetails(RateManagementModel.InstallationDetailModel, Installationmasterentity);

//                    applicationlogic.InsertInstallationDetails(InstallationDetailEntity);
//                }
//                else
//                {
//                    var costingSheetmasterentity = new CostingSheetMaster();
//                    RateManagementModel.CostingSheetMasterModel.ID = Convert.ToInt32(counter);

//                    RateManagementModel.CostingSheetMasterModel.CostingSheetNumber = Session["mystring"].ToString();
//                    var csm = CostingSheetMasterMapper.CostingSheetMasterToMopdelCostingSheetMaster(RateManagementModel.CostingSheetMasterModel);
//                    csm.CreatedBy = Session["Username"].ToString();
//                    csm.LastModifiedBy = Session["Username"].ToString();
//                    costingSheetmasterentity = applicationlogic.UpdateCostingSheetMaster(csm);

//                    var InstallationMasterMapper = new InstallationMasterMapper(session);

//                    InstallationMaster im = new InstallationMaster();
//                    var costing = new CostingSheetMaster();
//                    int SotingSeetMAster_id = Convert.ToInt32(counter);
//                    costing = applicationlogic.GetEntityById<CostingSheetMaster>(SotingSeetMAster_id);
//                    im = InstallationMasterMapper.InstallationMasterModelToInstallationMaster(RateManagementModel.InstallationMasterModel, costing);

//                    im.Id = Convert.ToInt32(installCounter);
//                    //im.CostingSheetMaster.Id = Convert.ToInt32(counter);
//                    var Installationmasterentity = applicationlogic.UpdateInstallationMaster(im);

//                    //Session["Counter"] = Installationmasterentity.Id;
//                    var Installationmastermodel = InstallationMasterMapper.InstallationMasterToInstallationMasterModel(Installationmasterentity);

//                    var InstallationDetailMapper = new InstallationDetailMapper(session);
//                    RateManagementModel.InstallationDetailModel.InstallationMaster = Installationmastermodel;
//                    var InstallationDetailEntity = InstallationDetailMapper.InstallationDetailsModelToInstallationDetails(RateManagementModel.InstallationDetailModel, Installationmasterentity);

//                    applicationlogic.InsertInstallationDetails(InstallationDetailEntity);
//                }
//                IList<InstallationDetailsModel> IDModel = new List<InstallationDetailsModel>();
//                int InstallationMasterID = Convert.ToInt32(Session["installCounter"]);

//                var installationDetails = applicationlogic.GetInstallationDetailInfo(InstallationMasterID);

//                foreach (var _installationDetails in installationDetails)
//                {
//                    var installationdetailModel = InstallationDetailMapper.InstallationDetailsToInstallationDetailsModel(_installationDetails);
//                    IDModel.Add(installationdetailModel);
//                }
//                List<string> list = new List<string>();
//                foreach (var item in IDModel)
//                {
//                    var a = item.ID.ToString();
//                    list.Add(a);
//                    var b = item.Type;
//                    list.Add(b);
//                    var c = item.Labour.ToString();
//                    list.Add(c);
//                    var de = item.Days;
//                    var d = de.ToString();
//                    list.Add(d);
//                    var e = item.HoursPD.ToString();
//                    list.Add(e);
//                    var f = item.HourRate.ToString();
//                    list.Add(f);
//                    var g = item.LineTotal.ToString();
//                    list.Add(g);
//                }

//                var nslmasterid = Convert.ToInt32(Session["installCounter"]);
//                var nsltotal = applicationlogic.getNSLTotal(nslmasterid);
//                var total = nsltotal[0].LineTotal;


//                return Json(new
//                {
//                    DivID = list,
//                    ltotal = total
//                }
//                , JsonRequestBehavior.AllowGet);
//            }
//        }

//        public void SendCounterRequest(int id)
//        {
//            applicationlogic.updatecounterstatus(id);
//        }


//        public JsonResult GetRecord(string _search)
//        {
//            DataSet ds = dblayer.GetDetails(_search);
//            List<DBTestAccPAc> searchlist = new List<DBTestAccPAc>();
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                searchlist.Add(new DBTestAccPAc
//                {
//                    ITEMNO = dr["ITEMNO"].ToString(),
//                    DESC = dr["DESC"].ToString(),
//                    LOCATION = dr["LOCATION"].ToString(),
//                    QTYONHAND = dr["QTYONHAND"].ToString()
//                });
//            }

//            List<string> mystring = new List<string>();

//            foreach (var i in searchlist)
//            {
//                var a = i.ITEMNO;
//                mystring.Add(a);
//                var b = i.DESC;
//                mystring.Add(b);
//                var c = i.LOCATION;
//                mystring.Add(c);
//                var d = i.QTYONHAND;
//                mystring.Add(d);
//            }
//            return Json(new
//            {
//                DivID = mystring
//            }, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult GetCustomerRecords(string customername)
//        {
//            DataSet ds = dblayer.GetCustomerDetails(customername);
//            List<CustomerInfoAccPac> searchlist = new List<CustomerInfoAccPac>();
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                searchlist.Add(new CustomerInfoAccPac
//                {
//                    CustomerName = dr["NAMECUST"].ToString(),
//                    CustomerContact = dr["NAMECTAC"].ToString(),
//                    Telephone = dr["TEXTPHON1"].ToString(),
//                    AddressForDelivery = dr["AddressofDelivery"].ToString()
//                });
//            }

//            List<string> mystring = new List<string>();

//            foreach (var i in searchlist)
//            {
//                var a = i.CustomerName;
//                mystring.Add(a);
//                var b = i.CustomerContact;
//                mystring.Add(b);
//                var c = i.Telephone;
//                mystring.Add(c);
//                var d = i.AddressForDelivery;
//                mystring.Add(d);
//            }
//            return Json(new
//            {
//                DivID = mystring
//            }, JsonRequestBehavior.AllowGet);
//        }
//        public JsonResult GetSavedOPFDetails()
//        {
//            var Createdby = Session["Username"].ToString();
//            DataSet ds = OPFLayer.GetDetailsfromOPF(Createdby);
//            List<DBOPFModel> searchlist = new List<DBOPFModel>();
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                searchlist.Add(new DBOPFModel
//                {
//                    ID = dr["Id"].ToString(),
//                    OPFSequenceNumber_Full = dr["OPFSequenceNumber_Full"].ToString(),
//                    CustomerName = dr["CustomerName"].ToString(),
//                    CustomerContact = dr["CustomerContact"].ToString(),
//                    OPFStatus = dr["OPFStatus"].ToString()
//                });
//            }

//            List<string> mystring = new List<string>();

//            foreach (var i in searchlist)
//            {
//                var a = i.ID;
//                mystring.Add(a);
//                var b = i.OPFSequenceNumber_Full;
//                mystring.Add(b);
//                var c = i.CustomerName;
//                mystring.Add(c);
//                var d = i.CustomerContact;
//                mystring.Add(d);
//                var e = i.OPFStatus;
//                mystring.Add(e);
//            }

//            return Json(new
//            {
//                DivID = mystring
//            }, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult GetSavedOPFDetailsforOPFS()
//        {
//            var Createdby = Session["Username"].ToString();
//            DataSet ds = OPFLayer.GetDetailsforSavedOPF(Createdby);
//            List<DBOPFModel> searchlist = new List<DBOPFModel>();
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                searchlist.Add(new DBOPFModel
//                {
//                    ID = dr["Id"].ToString(),
//                    OPFSequenceNumber_Full = dr["OPFSequenceNumber_Full"].ToString(),
//                    CustomerName = dr["CustomerName"].ToString(),
//                    CustomerContact = dr["CustomerContact"].ToString(),
//                    OPFStatus = dr["OPFStatus"].ToString()
//                });
//            }

//            List<string> mystring = new List<string>();

//            foreach (var i in searchlist)
//            {
//                var a = i.ID;
//                mystring.Add(a);
//                var b = i.OPFSequenceNumber_Full;
//                mystring.Add(b);
//                var c = i.CustomerName;
//                mystring.Add(c);
//                var d = i.CustomerContact;
//                mystring.Add(d);
//                var e = i.OPFStatus;
//                mystring.Add(e);
//            }

//            return Json(new
//            {
//                DivID = mystring
//            }, JsonRequestBehavior.AllowGet);
//        }


//        public JsonResult GetSavedCSDetails()
//        {
//            //DataSet ds = dblayer.GetSavedCSDetails("AKS");
//            //List<SavedCostingSheet> searchlist = new List<SavedCostingSheet>();
//            //foreach (DataRow dr in ds.Tables[0].Rows)
//            //{
//            //    searchlist.Add(new SavedCostingSheet
//            //    {
//            //        Id = dr["Id"].ToString(),
//            //        Date = dr["Date"].ToString(),
//            //        CustomerName = dr["CustomerName"].ToString(),
//            //        CostingSheetNumber = dr["CostingSheetNumber"].ToString(),
//            //        VendorPartNum = dr["VendorPartNum"].ToString(),
//            //        Description = dr["Description"].ToString()
//            //    });
//            //}

//            IApplicationLogic applicationlogic = new ApplicationLogic(session);
//            IList<CostingSheetMasterModel> CSMasterModel = new List<CostingSheetMasterModel>();
//            var CreatedBy = Session["Username"].ToString();

//            var masterDetails = applicationlogic.GetSavedCostingSheetMasterDetails(CreatedBy);

//            foreach (var _masterDetails in masterDetails)
//            {
//                var masterDetailsModel = CostingSheetMasterMapper.CostingSheetMasterToCostingSheetMasterModel(_masterDetails);
//                CSMasterModel.Add(masterDetailsModel);
//            }

//            List<string> costingSheetMasterList = new List<string>();
//            foreach (var item in CSMasterModel)
//            {
//                var aa = item.ID.ToString();
//                costingSheetMasterList.Add(aa);
//                var a = item.Date.ToString();
//                costingSheetMasterList.Add(a);
//                var b = item.CustomerName;
//                costingSheetMasterList.Add(b);
//                var c = item.CustomerContact;
//                costingSheetMasterList.Add(c);
//                var d = item.CostingSheetNumber;
//                costingSheetMasterList.Add(d);
//            }
//            return Json(new
//            {
//                DivID = costingSheetMasterList
//            }, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult GetCOstingSheetDetails(int id)
//        {
//            var costingsheetDetails = applicationlogic.GetCostingSheetDetailInfoByCostDetailsID(id);
//            IList<CostingSheetDetailModel> CSDetailModel = new List<CostingSheetDetailModel>();
//            foreach (var _costingsheetDetails in costingsheetDetails)
//            {
//                var costingsheetDetailsModel = CostingSheetDetailMapper.CostingSheetDetailModelToCostingSheetDetail(_costingsheetDetails);
//                CSDetailModel.Add(costingsheetDetailsModel);
//            }
//            List<string> costingSheetDetailList = new List<string>();
//            foreach (var item in CSDetailModel)
//            {
//                var a = item.ID.ToString();
//                costingSheetDetailList.Add(a);
//                var b = item.SalesNo;
//                costingSheetDetailList.Add(b);
//                var d = item.VendorPartNum;
//                costingSheetDetailList.Add(d);
//                var f = item.Warranty;
//                costingSheetDetailList.Add(f);
//                var h = item.SupplierQuoteReferenceNumber;
//                costingSheetDetailList.Add(h);
//                var j = item.StockAvailable;
//                costingSheetDetailList.Add(j);
//                var l = item.Quantity.ToString();
//                costingSheetDetailList.Add(l);
//                var n = item.DiscountFactor.ToString();
//                costingSheetDetailList.Add(n);
//                var p = item.DatecTotalBuy.ToString();
//                costingSheetDetailList.Add(p);
//                var c = item.UPCCode;
//                costingSheetDetailList.Add(c);                
//                var e = item.Description;
//                costingSheetDetailList.Add(e);
//                var ee = item.Department_Location_Code;
//                costingSheetDetailList.Add(ee);             
//                var g = item.SupplierCompany_Country;
//                costingSheetDetailList.Add(g);                
//                var i = item.DeliveryTime.ToString();
//                costingSheetDetailList.Add(i);                
//                var k = item.Dimensions.ToString();
//                costingSheetDetailList.Add(k);                
//                var m = item.ListPrice.ToString();
//                costingSheetDetailList.Add(m);                
//                var o = item.DatecUnitBuyExGST;
//                costingSheetDetailList.Add(o);                
//                var r = item.FCUnit.ToString();
//                costingSheetDetailList.Add(r);
//                var q = item.Freight.ToString();
//                costingSheetDetailList.Add(q);
//                var t = item.ITLevyCredit.ToString();
//                costingSheetDetailList.Add(t);
//                var v = item.Currency.ToString();
//                costingSheetDetailList.Add(v);
//                var x = item.TotalLC_FJD.ToString();
//                costingSheetDetailList.Add(x);
//                var z = item.UnitMargin.ToString();
//                costingSheetDetailList.Add(z);
//                var ab = item.UnitTax.ToString();
//                costingSheetDetailList.Add(ab);
//                var ad = item.Duty.ToString();
//                costingSheetDetailList.Add(ad);
//                var af = item.UnitVIPFJD.ToString();
//                costingSheetDetailList.Add(af);
//                var s = item.FCTotal.ToString();
//                costingSheetDetailList.Add(s); 
//                var u = item.LC_Ex.ToString();
//                costingSheetDetailList.Add(u);                
//                var w = item.UnitLC_FJD.ToString();
//                costingSheetDetailList.Add(w);                
//                var y = item.Margin.ToString();
//                costingSheetDetailList.Add(y);                
//                var aa = item.TotalMarginValue.ToString();
//                costingSheetDetailList.Add(aa);                
//                var ac = item.TotalTaxFree.ToString();
//                costingSheetDetailList.Add(ac);                
//                var ae = item.VAT.ToString();
//                costingSheetDetailList.Add(ae);                
//                var ag = item.TotalVIPFJD.ToString();
//                costingSheetDetailList.Add(ag);
//            }

          

//            return Json(new
//            {
//                DivID = costingSheetDetailList
//            }, JsonRequestBehavior.AllowGet);
//        }
//        //Costing Sheet Edit and Update
//        public ActionResult CostingSheet(int id)
//        {
//            Session["CostingsheetMasterForTotal"] = id;
//            IApplicationLogic applicationlogic = new ApplicationLogic(session);
//            IList<CostingSheetMasterModel> CSMasterModel = new List<CostingSheetMasterModel>();
//            IList<InstallationDetailsModel> IDetailsModel = new List<InstallationDetailsModel>();
//            IList<CostingSheetDetailModel> CSDetailModel = new List<CostingSheetDetailModel>();
//            var CostingSheetMasterId = id;

//            var nslMasterId = applicationlogic.GetNSLMasterId(CostingSheetMasterId);
//            var installationDetails = applicationlogic.GetInstallationDetailInfo(nslMasterId);

//            var masterDetails = applicationlogic.GetCostingSheetMasterDetails(CostingSheetMasterId);
//            var costingsheetDetails = applicationlogic.GetCostingSheetDetails(CostingSheetMasterId);

//            foreach (var _masterDetails in masterDetails)
//            {
//                var masterDetailsModel = CostingSheetMasterMapper.CostingSheetMasterToCostingSheetMasterModel(_masterDetails);
//                CSMasterModel.Add(masterDetailsModel);
//            }

//            foreach (var _installationDetails in installationDetails)
//            {
//                var installationDetailsModel = InstallationDetailMapper.InstallationDetailsToInstallationDetailsModel(_installationDetails);
//                IDetailsModel.Add(installationDetailsModel);
//            }

//            foreach (var _costingsheetDetails in costingsheetDetails)
//            {
//                var costingsheetDetailsModel = CostingSheetDetailMapper.CostingSheetDetailModelToCostingSheetDetail(_costingsheetDetails);
//                CSDetailModel.Add(costingsheetDetailsModel);
//            }

//            List<string> costingSheetMasterList = new List<string>();
//            foreach (var item in CSMasterModel)
//            {
//                var aa = item.ID.ToString();
//                costingSheetMasterList.Add(aa);
//                var a = item.Date.ToString();
//                costingSheetMasterList.Add(a);
//                var b = item.CustomerName;
//                costingSheetMasterList.Add(b);
//                var c = item.CustomerContact;
//                costingSheetMasterList.Add(c);
//                var d = item.CostingSheetNumber;
//                costingSheetMasterList.Add(d);
//                var e = item.Telephone;
//                costingSheetMasterList.Add(e);
//                var f = item.AddressForDelivery;
//                costingSheetMasterList.Add(f);
//            }

//            List<string> installationDetailList = new List<string>();
//            foreach (var item in IDetailsModel)
//            {
//                var a = item.ID.ToString();
//                installationDetailList.Add(a);
//                var ab = item.Type.ToString();
//                installationDetailList.Add(ab);
//                var aa = item.Labour.ToString();
//                installationDetailList.Add(aa);
//                var b = item.Days.ToString();
//                installationDetailList.Add(b);
//                var c = item.HourRate.ToString();
//                installationDetailList.Add(c);
//                var d = item.HoursPD.ToString();
//                installationDetailList.Add(d);
//                var e = item.LineTotal.ToString();
//                installationDetailList.Add(e);
//            }

//            List<string> costingSheetDetailList = new List<string>();
//            foreach (var item in CSDetailModel)
//            {
//                var a = item.ID.ToString();
//                costingSheetDetailList.Add(a);
//                var b = item.SalesNo;
//                costingSheetDetailList.Add(b);
//                var c = item.UPCCode;
//                costingSheetDetailList.Add(c);
//                var d = item.VendorPartNum;
//                costingSheetDetailList.Add(d);
//                var e = item.Description;
//                costingSheetDetailList.Add(e);
//                var ee = item.Department_Location_Code;
//                costingSheetDetailList.Add(ee);
//                var f = item.Warranty;
//                costingSheetDetailList.Add(f);
//                var g = item.SupplierCompany_Country;
//                costingSheetDetailList.Add(g);
//                var h = item.SupplierQuoteReferenceNumber;
//                costingSheetDetailList.Add(h);
//                var i = item.DeliveryTime.ToString();
//                costingSheetDetailList.Add(i);
//                var j = item.StockAvailable;
//                costingSheetDetailList.Add(j);
//                var k = item.Dimensions.ToString();
//                costingSheetDetailList.Add(k);
//                var l = item.Quantity.ToString();
//                costingSheetDetailList.Add(l);
//                var m = item.ListPrice.ToString();
//                costingSheetDetailList.Add(m);
//                var n = item.DiscountFactor.ToString();
//                costingSheetDetailList.Add(n);
//                var o = item.DatecUnitBuyExGST;
//                costingSheetDetailList.Add(o);
//                var p = item.DatecTotalBuy.ToString();
//                costingSheetDetailList.Add(p);
//                var r = item.FCUnit.ToString();
//                costingSheetDetailList.Add(r);
//                var s = item.FCTotal.ToString();
//                costingSheetDetailList.Add(s);
//                var q = item.Freight.ToString();
//                costingSheetDetailList.Add(q);
//                var t = item.ITLevyCredit.ToString();
//                costingSheetDetailList.Add(t);
//                var u = item.LC_Ex.ToString();
//                costingSheetDetailList.Add(u);
//                var v = item.Currency.ToString();
//                costingSheetDetailList.Add(v);
//                var w = item.UnitLC_FJD.ToString();
//                costingSheetDetailList.Add(w);
//                var x = item.TotalLC_FJD.ToString();
//                costingSheetDetailList.Add(x);
//                var y = item.Margin.ToString();
//                costingSheetDetailList.Add(y);
//                var z = item.UnitMargin.ToString();
//                costingSheetDetailList.Add(z);
//                var aa = item.TotalMarginValue.ToString();
//                costingSheetDetailList.Add(aa);
//                var ab = item.UnitTax.ToString();
//                costingSheetDetailList.Add(ab);
//                var ac = item.TotalTaxFree.ToString();
//                costingSheetDetailList.Add(ac);
//                var ad = item.Duty.ToString();
//                costingSheetDetailList.Add(ad);
//                var ae = item.VAT.ToString();
//                costingSheetDetailList.Add(ae);
//                var af = item.UnitVIPFJD.ToString();
//                costingSheetDetailList.Add(af);
//                var ag = item.TotalVIPFJD.ToString();
//                costingSheetDetailList.Add(ag);
//            }

//            // return Json(new
//            // {
//            //     DivID = costingSheetMasterList,
//            //     DivIDD = costingSheetDetailList
//            // }
//            //, JsonRequestBehavior.AllowGet);
//            RateManagementModel RTM = new RateManagementModel();
//            var sysmodel = new ExchangeRateModel();
//            sysmodel.GetRole = getRoleSelectList();
//            RTM.ExchangeRateModel = sysmodel;



//            ViewData["ViewSavedCSMasterDetails"] = CSMasterModel;
//            ViewData["ViewSavedInstallationDetails"] = IDetailsModel;
//            ViewData["ViewSavedCSDetailDetails"] = CSDetailModel;

//            return View(RTM);
//        }


//        public ActionResult GetTotalValues()
//        {
//            int csm = Convert.ToInt32(Session["CostingsheetMasterForTotal"]);
//            var nslmasterid = applicationlogic.GetNSLMasterId(csm);
//            var nsltotal = applicationlogic.getNSLTotal(nslmasterid);
//            var total = nsltotal[0].LineTotal;

//            var totaltaxfree = applicationlogic.getTotalTaxFree(csm);
//            var totaltaxFree = totaltaxfree[0].TotalTaxFree;

//            var totalvipfjd = applicationlogic.getTotalVIPFJD(csm);
//            var totalvipFjd = totalvipfjd[0].TotalVIPFJD;

//            return Json(new
//            {
//                NSLTotal = total,
//                totalTaxFree = totaltaxFree,
//                totalVIPFJD = totalvipFjd
//            }
//                 , JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult OdPF(List<int> ids)
//        {
          
//            foreach (var i in ids)
//            {
//                int a = Convert.ToInt16(i);
//            }

//            TempData["IDs"] = ids;
//            return Json(new
//            {
//                redirectUrl = Url.Action("OPF", "AccountManager"),
//                isRedirect = true
//            }
//                  , JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult OPF()
//        {
//            List<int> objectArray = (List<int>)TempData.Peek("IDs");
//            RateManagementModel RTM = new RateManagementModel();
//            if (objectArray != null)
//            {
//                int MasterID = objectArray[0];

//                var Costingsheetmasterid = applicationlogic.GetMasterID(MasterID);
//                var Customername = Costingsheetmasterid[0].CustomerName;
//                var CustomerContact = Costingsheetmasterid[0].CustomerContact;
//                var accountmanager = Costingsheetmasterid[0].CreatedBy;
//                var tel = Costingsheetmasterid[0].Telephone;
//                var add = Costingsheetmasterid[0].AddressForDelivery;
//                var sum = applicationlogic.getTotalVIP(objectArray);

//                var total = sum[0].TotalVIPFJD;

//                var QAList = new ActiveDirectoryModel();
//                QAList.GetRoles = getQASelectList();
//                RTM.ActiveDirectoryModel = QAList;

//                var GM = applicationlogic.getGM();

//                var GMName = "";

//                if (total > 20000){
//                    GMName = applicationlogic.getGMName();
//                  }
//                // var AccountManager = applicationlogic.GetAMbyCreatedBy(Costingsheetmasterid);
//                var CFO = applicationlogic.getCFO();
//                var CFOName = applicationlogic.getCFOName();
//                var Manager = applicationlogic.getManager();
//                var ManagerName = applicationlogic.getManagerName("sen.shinal");


//               // ViewData["ViewOPFDetails"] = DetailsForOPFfromCostingSheet;
//                ViewData["Customername"] = Customername;
//                ViewData["CustomerContact"] = CustomerContact;
//                ViewData["tel"] = tel;
//                ViewData["add"] = add;
//                ViewData["accountmanager"] = accountmanager;
//                ViewData["TotalVipFJD"] = total;
//                ViewData["GM"] = GM;
//                ViewData["CFO"] = CFO;
//                ViewData["Manager"] = Manager;
//                ViewData["GMName"] = GMName;
//                ViewData["CFOName"] = CFOName;
//                ViewData["ManagerNAme"] = ManagerName;
//            }

//            return View(RTM);
//        }

//        public ActionResult ShowdataForOPF()
//        {
//            List<int> objectArray = (List<int>)TempData.Peek("IDs");
//            IList<CostingSheetDetailModel> DetailsForOPFfromCostingSheet = new List<CostingSheetDetailModel>();
//            var csd = applicationlogic.GetListOfCostingSheetDetails(objectArray);
//            foreach (var a in csd)
//            {
//                var csModelo = CostingSheetDetailMapper.CostingSheetDetailModelToCostingSheetDetail(a);
//                DetailsForOPFfromCostingSheet.Add(csModelo);
//            }

//            List<string> CostingSheetData = new List<string>();
//            foreach(var item in DetailsForOPFfromCostingSheet)
//            {
//                var a = item.Department_Location_Code;
//                CostingSheetData.Add(a);
//                var b = item.VendorPartNum;
//                CostingSheetData.Add(b);
//                var c = item.Description;
//                CostingSheetData.Add(c);
//                var d = item.Warranty;
//                CostingSheetData.Add(d);
//                var e = item.SupplierCompany_Country;
//                CostingSheetData.Add(e);
//                var f = item.Quantity.ToString();
//                CostingSheetData.Add(f);
//                var g = item.TotalTaxFree;
//                CostingSheetData.Add(g.ToString());
//                var h = item.DatecUnitBuyExGST.ToString();
//                CostingSheetData.Add(h);
//                var i = item.TotalLC_FJD.ToString();
//                CostingSheetData.Add(i);
//                var j = item.TotalMarginValue.ToString();
//                CostingSheetData.Add(j);
//            }

//            return Json(new
//            {
//                DivId = CostingSheetData
//            }
//              , JsonRequestBehavior.AllowGet);

//        }


//        public ActionResult SaveOPFs(RateManagementModel RateManagementModel)
//        {
//             if (Request.Form["saveopf"] != null)
//            {
//                OPFMaster OPFMaster = new OPFMaster();
//                int OPFNum = applicationlogic.getLastOPFNum();
//                var QA = RateManagementModel.OPFMasterModel.QA;
//                var Dept = RateManagementModel.OPFMasterModel.DeptManager;
//                //var GetQAID = applicationlogic.GetIDofQA();
//                List<int> objectArray = (List<int>)TempData.Peek("IDs");
//                var costingdetailid = objectArray[0];
//                var costingsheetmaster = applicationlogic.getMasterIdByCostingDetail(costingdetailid);

//                OPFMaster = OPFMasterMapper.OPFMasterModelToOPFMaster(RateManagementModel.OPFMasterModel);
//                OPFMaster.OPFSequenceNum = OPFNum.ToString();
//                OPFMaster.OPFStatus = "Saved";
//                OPFMaster.CreatedBy = Session["Username"].ToString();
//                OPFMaster.LastModifiedBy = Session["Username"].ToString();
//                OPFMaster.Counter = 1;

//                var OPFMasterEnttity = applicationlogic.InsertOPFMaster(OPFMaster);

//                int OPFMAsterID = OPFMasterEnttity.Id;

//                var opfnumber = applicationlogic.SavBoFSequenceNumber_Full(OPFMAsterID);
//                applicationlogic.saveCostingsheetmasterintoOPF(costingsheetmaster, OPFMAsterID);

//                foreach (var id in objectArray)
//                {
//                    applicationlogic.changeRowStatus(id);
//                }

//                EmailNotification email = new EmailNotification();
//                applicationlogic.saveOPFs(objectArray, OPFMAsterID);
//                //string header = "Request For Approval For OPF#: " + opfnumber;
//                //string body = "You have a Pending OPF on your queue" +
//                //    "\n\nOPF Number: " + opfnumber +
//                //    "\nCreated By: sen.shinal" +
//                //    "\nCreated Date: 03/06/2017" +
//                //    "\n\nPlease log in to the system to view the OPF" +
//                //    "\n\nThank you";
//                //if (QA == null || QA == "-1")
//                //{
//                //    var DEPTEmail = applicationlogic.GetEmail(Dept);
//                //    email.sendMail("sen.shinal@datec.com.fj", DEPTEmail, header, body);
//                //    applicationlogic.updateDept_Status(OPFMAsterID);
//                //}
//                //else
//                //{
//                //    var QAEmail = applicationlogic.GetEmail(QA);
//                //    email.sendMail("sen.shinal@datec.com.fj", QAEmail, header, body);
//                //    applicationlogic.updateQA_Status(OPFMAsterID);
//                //}

//            }
//            else
//            {
//                OPFMaster OPFMaster = new OPFMaster();
//                int OPFNum = applicationlogic.getLastOPFNum();
//                var QA = RateManagementModel.OPFMasterModel.QA;
//                var Dept = RateManagementModel.OPFMasterModel.DeptManager;
//                //var GetQAID = applicationlogic.GetIDofQA();
//                List<int> objectArray = (List<int>)TempData.Peek("IDs");
//                var costingdetailid = objectArray[0];
//                var costingsheetmaster = applicationlogic.getMasterIdByCostingDetail(costingdetailid);

//                OPFMaster = OPFMasterMapper.OPFMasterModelToOPFMaster(RateManagementModel.OPFMasterModel);
//                OPFMaster.OPFSequenceNum = OPFNum.ToString();
//                OPFMaster.OPFStatus = "Pending";
//                OPFMaster.CreatedBy = Session["Username"].ToString();
//                OPFMaster.LastModifiedBy = Session["Username"].ToString();
//                OPFMaster.Counter = 1;

//                var OPFMasterEnttity = applicationlogic.InsertOPFMaster(OPFMaster);

//                int OPFMAsterID = OPFMasterEnttity.Id;

//                var opfnumber = applicationlogic.SavBoFSequenceNumber_Full(OPFMAsterID);
//                applicationlogic.saveCostingsheetmasterintoOPF(costingsheetmaster, OPFMAsterID);



//                EmailNotification email = new EmailNotification();
//                applicationlogic.saveOPFs(objectArray, OPFMAsterID);
//                string header = "Request For Approval For OPF#: " + opfnumber;
//                string body = "You have a Pending OPF on your queue" +
//                    "\n\nOPF Number: " + opfnumber +
//                    "\nCreated By:" + OPFMasterEnttity.CreatedBy +
//                    "\nCreated Date: " + OPFMasterEnttity.CreatedDate +
//                    "\n\nPlease log in to the system to view the OPF" +
//                    "\n\nThank you";
//                if (QA == null || QA == "-1")
//                {
//                    var DEPTEmail = applicationlogic.GetEmail(Dept);
//                    var senderemail = applicationlogic.GetEmail(OPFMasterEnttity.CreatedBy);
//                    email.sendMail(senderemail, DEPTEmail, header, body);
//                    applicationlogic.updateDept_Status(OPFMAsterID);
//                }
//                else
//                {
//                    var QAEmail = applicationlogic.GetEmail(QA);
//                    var senderemail = applicationlogic.GetEmail(OPFMasterEnttity.CreatedBy);
//                    email.sendMail(senderemail, QAEmail, header, body);
//                    applicationlogic.updateQA_Status(OPFMAsterID);
//                }

//            }
//            //else if (Request.Form["saveopf"] != null)
//            //{
//            //    OPFMaster OPFMaster = new OPFMaster();
//            //    int OPFNum = applicationlogic.getLastOPFNum();
//            //    //var QA = RateManagementModel.OPFMasterModel.QA;
//            //    //var Dept = RateManagementModel.OPFMasterModel.DeptManager;
//            //    //var GetQAID = applicationlogic.GetIDofQA();
//            //    List<int> objectArray = (List<int>)TempData.Peek("IDs");
//            //    var costingdetailid = objectArray[0];
//            //    var costingsheetmaster = applicationlogic.getMasterIdByCostingDetail(costingdetailid);

//            //    OPFMaster = OPFMasterMapper.OPFMasterModelToOPFMaster(RateManagementModel.OPFMasterModel);
//            //    OPFMaster.OPFSequenceNum = OPFNum.ToString();

//            //    OPFMaster.OPFStatus = "Saved";

//            //    OPFMaster.CreatedBy = Session["Username"].ToString();
//            //    OPFMaster.LastModifiedBy = Session["Username"].ToString();

//            //    var OPFMasterEnttity = applicationlogic.InsertOPFMaster(OPFMaster);

//            //    int OPFMAsterID = OPFMasterEnttity.Id;

//            //    var opfnumber = applicationlogic.SavBoFSequenceNumber_Full(OPFMAsterID);
//            //    applicationlogic.saveCostingsheetmasterintoOPF(costingsheetmaster, OPFMAsterID);              

//            //}
//            return RedirectToAction("Index", "AccountManager");
//        }

//        public ActionResult Delete(int ids)
//        {
//            applicationlogic.DeleteRecords(ids);

//            return Json(new
//            {
                
//            }
//              , JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult SearchResult(string costnum, DateTime? Start, DateTime? end, string Customername)
//        {
//            var Costingresultentity = applicationlogic.SearchCostingSheetMaster(costnum,Start, end, Customername);

//            List<string> costingSheetmasterList = new List<string>();
//            foreach (var item in Costingresultentity)
//            {
//                var a = item.Id.ToString();
//                costingSheetmasterList.Add(a);
//                var b = item.CreatedDate;
//                costingSheetmasterList.Add(Convert.ToString(b));
//                var c = item.CustomerName;
//                costingSheetmasterList.Add(c);
//                var d = item.CustomerContact;
//                costingSheetmasterList.Add(d);
//                var e = item.CostingSheetNumber;
//                costingSheetmasterList.Add(e);
//            }

//                return Json(new
//            {
//               DivId = costingSheetmasterList
//            }
//                  , JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult searchOPF(string OPFSequencenumber, DateTime? Start, DateTime? end, string Customername, string status)
//        {
//            DataSet ds = OPFLayer.SearchOPF(OPFSequencenumber, Start, end, Customername,status);
//            List<DBOPFModel> searchlist = new List<DBOPFModel>();
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                searchlist.Add(new DBOPFModel
//                {
//                    ID = dr["Id"].ToString(),
//                    OPFSequenceNumber_Full = dr["OPFSequenceNumber_Full"].ToString(),
//                    CustomerName = dr["CustomerName"].ToString(),
//                    CustomerContact = dr["CustomerContact"].ToString(),
//                    OPFStatus = dr["OPFStatus"].ToString(),
//                    accountManager = dr["CreatedBy"].ToString()
//                });
//            }

//            List<string> mystring = new List<string>();

//            foreach (var i in searchlist)
//            {
//                var a = i.ID;
//                mystring.Add(a);
//                var b = i.OPFSequenceNumber_Full;
//                mystring.Add(b);
//                var c = i.CustomerName;
//                mystring.Add(c);
//                var d = i.CustomerContact;
//                mystring.Add(d);
//                var e = i.OPFStatus;
//                mystring.Add(e);
//                var f = i.accountManager;
//                mystring.Add(f);
//            }

//            return Json(new
//            {
//                DivId = mystring
//            }
//              , JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult DuplicateCS(int id)
//        {           
//            var costingSheetMasterID = id;
//            var installationMasterId = applicationlogic.GetNSLMasterId(costingSheetMasterID);

//            var costingsheetNumber = applicationlogic.getCostId();
//            int x = Convert.ToInt32(costingsheetNumber);
//            var costingsheetnumber = x + 1;
//            string newcostingsheetnumber = costingsheetnumber.ToString();

//            var date = DateTime.Now;

//            applicationlogic.DuplicateCostingSheetMaster(costingSheetMasterID, newcostingsheetnumber, date);

//            var newCostingSheetMasterId = applicationlogic.getupdatedCostMasterID();

//            applicationlogic.DuplicateCostingSheetDetails(costingSheetMasterID, newCostingSheetMasterId, date);

//            applicationlogic.DuplicateInstallationMaster(costingSheetMasterID, newCostingSheetMasterId, date);

//            var newInstallationMasterId = applicationlogic.getnewInstallationMasterId();

//            applicationlogic.DuplicateInstallationDetails(installationMasterId, newInstallationMasterId, date);

//            return RedirectToAction("CostingSheet", "AccountManager", new { id = newCostingSheetMasterId });
//        }
//        public ActionResult TransferDetails(int[] ids, string Comments,string departmang)
//        {
//            applicationlogic.InsertTrasnferDetails(ids, Comments,departmang, Session["Username"].ToString());
//            var user_Email = applicationlogic.GetEmail(Session["Username"].ToString());
//            var dept_email = applicationlogic.GetEmail(departmang);
//            EmailNotification email = new EmailNotification();
//            var date = DateTime.Now;
//            // applicationlogic.saveOPFs(objectArray, OPFMAsterID);
//            string header = "Request For Transfer of OPFs";
//            string body = "You have a Transfer on your queue" +                
//                "\nCreated By: " + Session["Username"].ToString()+
//                "\nCreated Date: " + date+
//                "\n\nPlease log in to the system to view the request" +
//                "\n\nThank you";
//            email.sendMail(user_Email, dept_email, header,body);
//            return Json(new
//            {
               
//            }
//                 , JsonRequestBehavior.AllowGet);
//        }


//    }
//}