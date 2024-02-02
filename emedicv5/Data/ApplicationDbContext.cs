using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql;
using eMedicNETEMv1.Models;

using eMedicv5.Models;

namespace eMedicv5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("emn_users").HasKey(k => new { k.Id });
            builder.Entity<ApplicationUser>(e => e.HasIndex(k => new { k.Email, k.UserName }).IsUnique(true));

            builder.Entity<ApplicationRole>().ToTable("emn_roles").HasKey(k => new { k.Id });
            builder.Entity<ApplicationUserRole>().ToTable("emn_user_roles").HasKey(k => new { k.UserId, k.RoleId });
            builder.Entity<ApplicationUserRole>(x =>
            {
                x.HasKey(y => new { y.UserId, y.RoleId });
                x.HasOne(y => y.Role)
                .WithMany(z => z.UserRoles)
                .HasForeignKey(y => y.RoleId)
                .IsRequired();
                x.HasOne(y => y.User)
                .WithMany(z => z.UserRoles)
                .HasForeignKey(y => y.UserId)
                .IsRequired();
            });
            builder.Entity<IdentityUserLogin<string>>().ToTable("emn_user_logins").HasKey(k => new { k.LoginProvider, k.ProviderKey, k.UserId });
            builder.Entity<IdentityUserClaim<string>>().ToTable("emn_user_claims").HasKey(k => new { k.Id });
            builder.Entity<IdentityUserToken<string>>().ToTable("emn_user_tokens").HasKey(k => new { k.UserId });
            builder.Entity<IdentityRoleClaim<string>>().ToTable("emn_role_claims").HasKey(k => new { k.Id });

            builder.Entity<Allergy>().ToTable("emn_allergy").HasKey(k => new { k.AllAutid });
            builder.Entity<Allergy>(e => e.HasIndex(k => k.AllSdesc).IsUnique(true));

            builder.Entity<AllergyDrug>().ToTable("emn_allergy_drug").HasKey(k => new { k.AlmAllid, k.AlmStkid });
            builder.Entity<Appointment>().ToTable("emn_appointment").HasKey(k => new { k.AptAutid });

            builder.Entity<Diagnosis>().ToTable("emn_diagnosis").HasKey(k => new { k.DiaAutid });
            builder.Entity<Diagnosis>(e => e.HasIndex(k => k.DiaSname).IsUnique(true));

            builder.Entity<DoctorSchedule>().ToTable("emn_doctor_schedule").HasKey(k => new { k.DshAutid });
            builder.Entity<StockAdjustment>().ToTable("emn_drug_adjustment").HasKey(k => new { k.DadAutid });
            builder.Entity<StockAdjustmentDetail>().ToTable("emn_drug_adjustment_detail").HasKey(k => new { k.DddAdjid });
            builder.Entity<StockDispensingInfo>().ToTable("emn_drug_dispensing_info").HasKey(k => new { k.MddAutid });
            builder.Entity<StockGoodsReceiveNote>().ToTable("emn_drug_goods_receive_note").HasKey(k => new { k.GrdAutid });
            builder.Entity<StockGoodsReceiveNoteDetail>().ToTable("emn_drug_goods_receive_note_detail").HasKey(k => new { k.GddGrnid, k.GddStkid });
            builder.Entity<StockIssue>().ToTable("emn_drug_issue").HasKey(k => new { k.DisAutid });
            builder.Entity<StockIssueDetail>().ToTable("emn_issue_detail").HasKey(k => new { k.DidRefid, k.DidStkid });
            builder.Entity<StockPurchaseOrder>().ToTable("emn_purchase_order").HasKey(k => new { k.PodAutid });
            builder.Entity<StockPurchaseOrderDetail>().ToTable("emn_drug_purchase_order_detail").HasKey(k => new { k.PddAutid });
            builder.Entity<StockTransfer>().ToTable("emn_drug_transfer").HasKey(k => new { k.DtrAutid });
            builder.Entity<StockTransferDetail>().ToTable("emn_transfer_detail").HasKey(k => new { k.DtdAutid });

            builder.Entity<EyeFinding>().ToTable("emn_eye_finding").HasKey(k => new { k.EfiAutid });
            builder.Entity<EyeFinding>(e => e.HasIndex(k => k.EfiSdesc).IsUnique(true));

            builder.Entity<ItemAdjustment>().ToTable("emn_item_adjustment").HasKey(k => new { k.IadAutid });
            builder.Entity<ItemAdjustmentDetail>().ToTable("emn_item_adjustment_detail").HasKey(k => new { k.IddAdjid, k.IddStkid });
            builder.Entity<ItemGoodsReceiveNote>().ToTable("emn_item_goods_receive_note").HasKey(k => new { k.IgrAutid });
            builder.Entity<ItemGoodsReceiveNoteDetail>().ToTable("emn_goods_receive_note_detail").HasKey(k => new { k.IgdAutid });
            builder.Entity<ItemIssue>().ToTable("emn_item_issue").HasKey(k => new { k.IiiAutid });
            builder.Entity<ItemIssueDetail>().ToTable("emn_item_issue_detail").HasKey(k => new { k.IidRefid, k.IidStkid });
            builder.Entity<ItemPurchaseOrder>().ToTable("emn_item_purchase_order").HasKey(k => new { k.IpoAutid });
            builder.Entity<ItemPurchaseOrderDetail>().ToTable("emn_item_purchase_order_detail").HasKey(k => new { k.IpdAutid });
            builder.Entity<ItemTransfer>().ToTable("emn_item_transfer").HasKey(k => new { k.ItrAutid });
            builder.Entity<ItemTransferDetail>().ToTable("emn_item_transfer_detail").HasKey(k => new { k.ItdAutid });

            builder.Entity<LabTest>().ToTable("emn_lab_test").HasKey(k => new { k.LtsAutid });
            builder.Entity<LabTest>(e => e.HasIndex(k => k.LtsSdesc).IsUnique(true));

            builder.Entity<Letter>().ToTable("emn_letter").HasKey(k => new { k.LtrAutid });
            builder.Entity<LetterTemplate>().ToTable("emn_letter_template").HasKey(k => new { k.LtmAutid });

            builder.Entity<MedicalExam>().ToTable("emn_medical_exam").HasKey(k => new { k.PmeVstid });
            builder.Entity<MedicalExamDetail>().ToTable("emn_medical_exam_detail").HasKey(k => new { k.MidVstid, k.MidPrmid });
            builder.Entity<MedicalPackage>().ToTable("emn_medical_package").HasKey(k => new { k.MpcAutid });
            builder.Entity<MedicalPackage>(e => e.HasIndex(k => k.MpcDescr).IsUnique(true));

            builder.Entity<OrganizationProfile>().ToTable("emn_organization_profile").HasKey(k => new { k.ComAutid });

            builder.Entity<Outlet>().ToTable("emn_outlet").HasKey(k => new { k.OulAutid });
            builder.Entity<Outlet>(e => e.HasIndex(k => k.OulSname).IsUnique(true));

            builder.Entity<OutletStockAdjustment>().ToTable("emn_outlet_stock_adjustment").HasKey(k => new { k.OsaAutid });
            builder.Entity<OutletStockAdjustmentDetail>().ToTable("emn_outlet_stock_adjustment_detail").HasKey(k => new { k.OsdAdjid, k.OsdStkid });
            builder.Entity<OutletStockInfo>().ToTable("emn_outlet_stock_info").HasKey(k => new { k.OsiStkid });
            builder.Entity<OutletStockMovement>().ToTable("emn_outlet_stock_movement").HasKey(k => new { k.OsmAutid });
            builder.Entity<PackageProduct>().ToTable("emn_package_product").HasKey(k => new { k.MpiPakid, k.MpiItmid });
            builder.Entity<PackageService>().ToTable("emn_package_service").HasKey(k => new { k.PsePakid, k.PseSerid });

            builder.Entity<Panel>().ToTable("emn_panel").HasKey(k => new { k.PnlAutid });
            builder.Entity<Panel>(e => e.HasIndex(k => k.PnlRegno).IsUnique(true));

            builder.Entity<PanelInvoice>().ToTable("emn_panel_invoice").HasKey(k => new { k.PniAutid });
            builder.Entity<PanelInvoiceDetail>().ToTable("emn_panel_invoice_detail").HasKey(k => new { k.PidInvid, k.PidVstid });
            builder.Entity<PanelInvoiceFormat>().ToTable("emn_panel_invoice_format").HasKey(k => new { k.PifAutid });

            builder.Entity<Parameter>().ToTable("emn_parameter").HasKey(k => new { k.PrmAutid });
            builder.Entity<Parameter>(e => e.HasIndex(k => k.PrmPdesc).IsUnique(true));

            builder.Entity<Patient>().ToTable("emn_patient").HasKey(k => new { k.PrnAutid });
            builder.Entity<Patient>(e => e.HasIndex(k => k.PrnTelhp).IsUnique(true));
            builder.Entity<Patient>(e => e.HasIndex(k => k.PrnIcpno).IsUnique(true));
            builder.Entity<Patient>(e => e.HasIndex(k => k.PrnEmail).IsUnique(true));

            builder.Entity<PatientAllergy>().ToTable("emn_patient_allergy").HasKey(k => new { k.PalAlrid, k.PalPatid });
            builder.Entity<PatientDocument>().ToTable("emn_patient_document").HasKey(k => new { k.PtdFilid });
            builder.Entity<PatientGuarantor>().ToTable("emn_patient_guarantor").HasKey(k => new { k.PgnPatid });
            builder.Entity<PatientNextKin>().ToTable("emn_patient_nextkin").HasKey(k => new { k.PknPatid });
            builder.Entity<PatientVisit>().ToTable("emn_patient_visit").HasKey(k => new { k.PvtVstid });

            builder.Entity<Product>().ToTable("emn_product").HasKey(k => new { k.ProAutid });
            builder.Entity<Product>(e => e.HasIndex(k => k.ProBcode).IsUnique(true));
            builder.Entity<Product>(e => e.HasIndex(k => k.ProSname).IsUnique(true));

            builder.Entity<ProductPrice>().ToTable("emn_product_price").HasKey(k => new { k.PprAutid });
            builder.Entity<Receipt>().ToTable("emn_receipt").HasKey(k => new { k.RecAutid });
            builder.Entity<ReceiptDetail>().ToTable("emn_receipt_detail").HasKey(k => new { k.RcdRecid, k.RcdPmode });
            builder.Entity<Room>().ToTable("emn_room").HasKey(k => new { k.RomAutid });
            builder.Entity<RoomAllotment>().ToTable("emn_room_allotment").HasKey(k => new { k.RalAutid });

            builder.Entity<Service>().ToTable("emn_service").HasKey(k => new { k.SerAutid });
            builder.Entity<Service>(e => e.HasIndex(k => k.SerSname).IsUnique(true));

            builder.Entity<Staff>().ToTable("emn_staff").HasKey(k => new { k.StfAutid });
            builder.Entity<Staff>(e => e.HasIndex(k => k.StfIcppt).IsUnique(true));
            builder.Entity<Staff>(e => e.HasIndex(k => k.StfTelhp).IsUnique(true));
            builder.Entity<Staff>(e => e.HasIndex(k => k.StfEmail).IsUnique(true));

            builder.Entity<StaffLeave>().ToTable("emn_staff_leave").HasKey(k => new { k.LevAutid });
            builder.Entity<StockDetailedInfo>().ToTable("emn_stock_detailed_info").HasKey(k => new { k.SdiMitid });
            builder.Entity<StockMovement>().ToTable("emn_stock_movement").HasKey(k => new { k.SmoAutid });
            builder.Entity<Supplier>().ToTable("emn_supplier").HasKey(k => new { k.SupAutid });
            builder.Entity<Supplier>(e => e.HasIndex(k => k.SupRegno).IsUnique(true));

            builder.Entity<Support>().ToTable("emn_support").HasKey(k => new { k.SrtAutid });
            builder.Entity<VisitCaseDetail>().ToTable("emn_visit_case_detail").HasKey(k => new { k.VcaVstid });
            builder.Entity<VisitChildDetail>().ToTable("emn_visit_child_detail").HasKey(k => new { k.VcdVstid });
            builder.Entity<VisitDiagnosisDetail>().ToTable("emn_visit_diagnosis_detail").HasKey(k => new { k.VddDgnid, k.VddVstid });
            builder.Entity<VisitEyeFinding>().ToTable("emn_visit_eye_finding").HasKey(k => new { k.VefFndid, k.VefVstid });
            builder.Entity<VisitInvestigationDetail>().ToTable("emn_visit_investigation_detail").HasKey(k => new { k.VidInvid, k.VidVstid });
            builder.Entity<VisitLabDetail>().ToTable("emn_visit_lab_detail").HasKey(k => new { k.VlbVstid, k.VlbTstid });
            builder.Entity<VisitParameterDetail>().ToTable("emn_visit_parameter_detail").HasKey(k => new { k.VprVstid, k.VprPrmid });
            builder.Entity<VisitProductDetail>().ToTable("emn_visit_product_detail").HasKey(k => new { k.VpdTrnid });
            builder.Entity<VisitServiceDetail>().ToTable("emn_visit_service_detail").HasKey(k => new { k.VsdVstid, k.VsdSerid });
        }
        public DbSet<Allergy> GetAllergy { get; set; }
        public DbSet<AllergyDrug> GetAllergyDrugs { get; set; }
        public DbSet<Appointment> GetAppointments { get; set; }
        public DbSet<Diagnosis> GetDiagnosis { get; set; }
        public DbSet<DoctorSchedule> GetDoctorSchedules { get; set; }
        public DbSet<StockAdjustment> GetDrugAdjustments { get; set; }
        public DbSet<StockAdjustmentDetail> GetDrugAdjustmentDetails { get; set; }
        public DbSet<StockDispensingInfo> GetDrugDispensingInfos { get; set; }
        public DbSet<StockGoodsReceiveNote> GetDrugGoodsReceiveNotes { get; set; }
        public DbSet<StockGoodsReceiveNoteDetail> GetDrugGoodsReceiveNoteDetails { get; set; }
        public DbSet<StockIssue> GetDrugIssues { get; set; }
        public DbSet<StockIssueDetail> GetDrugIssueDetails { get; set; }
        public DbSet<StockPurchaseOrder> GetDrugPurchaseOrders { get; set; }
        public DbSet<StockPurchaseOrderDetail> GetDrugPurchaseOrderDetails { get; set; }
        public DbSet<StockTransfer> GetDrugTransfers { get; set; }
        public DbSet<StockTransferDetail> GetDrugTransferDetails { get; set; }
        public DbSet<EyeFinding> GetEyeFindings { get; set; }
        public DbSet<ItemAdjustment> GetItemAdjustments { get; set; }
        public DbSet<ItemAdjustmentDetail> GetItemAdjustmentDetails { get; set; }
        public DbSet<ItemGoodsReceiveNote> GetItemGoodsReceiveNotes { get; set; }
        public DbSet<ItemGoodsReceiveNoteDetail> GetItemGoodsReceiveNoteDetails { get; set; }
        public DbSet<ItemIssue> GetItemIssues { get; set; }
        public DbSet<ItemIssueDetail> GetItemIssueDetails { get; set; }
        public DbSet<ItemPurchaseOrder> GetItemPurchaseOrders { get; set; }
        public DbSet<ItemPurchaseOrderDetail> GetItemPurchaseOrderDetails { get; set; }
        public DbSet<ItemTransfer> GetItemTransfers { get; set; }
        public DbSet<ItemTransferDetail> GetItemTransferDetails { get; set; }
        public DbSet<LabTest> GetLabTests { get; set; }
        public DbSet<Letter> GetLetters { get; set; }
        public DbSet<LetterTemplate> GetLetterTemplates { get; set; }
        public DbSet<MedicalExam> GetMedicalExams { get; set; }
        public DbSet<MedicalExamDetail> GetMedicalExamDetails { get; set; }
        public DbSet<OrganizationProfile> GetOrganizationProfiles { get; set; }
        public DbSet<Outlet> GetOutlets { get; set; }
        public DbSet<OutletStockAdjustment> GetOutletStockAdjustments { get; set; }
        public DbSet<OutletStockAdjustmentDetail> GetOutletStockAdjustmentDetails { get; set; }
        public DbSet<OutletStockInfo> GetOutletStockInfos { get; set; }
        public DbSet<OutletStockMovement> GetOutletStockMovements { get; set; }
        public DbSet<PackageProduct> GetPackageProducts { get; set; }
        public DbSet<PackageService> GetPackageServices { get; set; }
        public DbSet<Panel> GetPanels { get; set; }
        public DbSet<PanelInvoice> GetPanelInvoices { get; set; }
        public DbSet<PanelInvoiceDetail> GetPanelInvoiceDetails { get; set; }
        public DbSet<PanelInvoiceFormat> GetPanelInvoiceFormats { get; set; }
        public DbSet<Parameter> GetParameters { get; set; }
        public DbSet<Patient> GetPatients { get; set; }
        public DbSet<PatientAllergy> GetPatientAllergies { get; set; }
        public DbSet<PatientDocument> GetPatientDocuments { get; set; }
        public DbSet<PatientGuarantor> GetPatientGuarantors { get; set; }
        public DbSet<PatientNextKin> GetPatientNextKins { get; set; }
        public DbSet<PatientVisit> GetPatientVisits { get; set; }
        public DbSet<Product> GetProducts { get; set; }
        public DbSet<ProductPrice> GetProductPrices { get; set; }
        public DbSet<Receipt> GetReceipts { get; set; }
        public DbSet<ReceiptDetail> GetReceiptDetails { get; set; }
        public DbSet<Room> GetRooms { get; set; }
        public DbSet<RoomAllotment> GetRoomAllotments { get; set; }
        public DbSet<Service> GetServices { get; set; }
        public DbSet<Staff> GetStaffs { get; set; }
        public DbSet<StaffLeave> GetStaffLeaves { get; set; }
        public DbSet<StockDetailedInfo> GetStockDetailedInfos { get; set; }
        public DbSet<StockMovement> GetStockMovements { get; set; }
        public DbSet<Supplier> GetSuppliers { get; set; }
        public DbSet<Support> GetSupports { get; set; }
        public DbSet<VisitCaseDetail> GetVisitCaseDetails { get; set; }
        public DbSet<VisitChildDetail> GetVisitChildDetails { get; set; }
        public DbSet<VisitDiagnosisDetail> GetVisitDiagnosisDetails { get; set; }
        public DbSet<VisitEyeFinding> GetVisitEyeFindings { get; set; }
        public DbSet<VisitInvestigationDetail> GetVisitInvestigationDetails { get; set; }
        public DbSet<VisitLabDetail> GetVisitLabDetails { get; set; }
        public DbSet<VisitParameterDetail> GetVisitParameterDetails { get; set; }
        public DbSet<VisitProductDetail> GetVisitProductDetails { get; set; }
        public DbSet<VisitServiceDetail> GetVisitServiceDetails { get; set; }

    }
}

