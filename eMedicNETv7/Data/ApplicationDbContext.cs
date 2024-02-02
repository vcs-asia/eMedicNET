using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql;

using eMedicEntityModel.Models.v1;

namespace eMedicNETv7.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("tbl_users").HasKey(k => new { k.Id });
            builder.Entity<ApplicationUser>(e => e.HasIndex(k => new { k.Email, k.UserName }).IsUnique(true));

            builder.Entity<ApplicationRole>().ToTable("tbl_roles").HasKey(k => new { k.Id });

            builder.Entity<ApplicationUserRole>().ToTable("tbl_user_roles").HasKey(k => new { k.UserId, k.RoleId });
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
            builder.Entity<IdentityUserLogin<string>>().ToTable("tbl_user_logins").HasKey(k => new { k.LoginProvider, k.ProviderKey, k.UserId });

            builder.Entity<IdentityUserClaim<string>>().ToTable("tbl_user_claims").HasKey(k => new { k.Id });

            builder.Entity<IdentityUserToken<string>>().ToTable("tbl_user_tokens").HasKey(k => new { k.UserId });

            builder.Entity<IdentityRoleClaim<string>>().ToTable("tbl_role_claims").HasKey(k => new { k.Id });

            builder.Entity<Allergy>().ToTable("tbl_allergy").HasKey(k => new { k.AllAutid });
            builder.Entity<Allergy>(e => e.HasIndex(k => k.AllSdesc).IsUnique(true));

            builder.Entity<AllergyDrug>().ToTable("tbl_allergy_drug").HasKey(k => new { k.AlmAutid });

            builder.Entity<Appointment>().ToTable("tbl_appointment").HasKey(k => new { k.AptAutid });

            builder.Entity<Diagnosis>().ToTable("tbl_diagnosis").HasKey(k => new { k.DiaAutid });
            builder.Entity<Diagnosis>(e => e.HasIndex(k => k.DiaSname).IsUnique(true));

            builder.Entity<Investigation>().ToTable("tbl_investigation").HasKey(k => new { k.IngAutid });
            builder.Entity<Investigation>(e => e.HasIndex(k => k.IngDescr).IsUnique(true));

            builder.Entity<DoctorSchedule>().ToTable("tbl_doctor_schedule").HasKey(k => new { k.DshAutid });

            builder.Entity<StockAdjustment>().ToTable("tbl_stock_adjustment").HasKey(k => new { k.StaAutid });

            builder.Entity<StockAdjustmentDetail>().ToTable("tbl_stock_adjustment_detail").HasKey(k => new { k.SadAutid });

            builder.Entity<StockDispensingInfo>().ToTable("tbl_stock_dispensing_info").HasKey(k => new { k.SdiAutid });

            builder.Entity<StockGoodsReceiveNote>().ToTable("tbl_stock_goods_receive_note").HasKey(k => new { k.GrsAutid });

            builder.Entity<StockGoodsReceiveNoteDetail>().ToTable("tbl_stock_goods_receive_note_detail").HasKey(k => new { k.GsdAutid });

            builder.Entity<StockIssue>().ToTable("tbl_stock_issue").HasKey(k => new { k.SisAutid });

            builder.Entity<StockIssueDetail>().ToTable("tbl_issue_detail").HasKey(k => new { k.SidAutid });

            builder.Entity<StockPurchaseOrder>().ToTable("tbl_purchase_order").HasKey(k => new { k.PosAutid });

            builder.Entity<StockPurchaseOrderDetail>().ToTable("tbl_stock_purchase_order_detail").HasKey(k => new { k.PdsAutid });

            builder.Entity<StockTransfer>().ToTable("tbl_stock_transfer").HasKey(k => new { k.StrAutid });

            builder.Entity<StockTransferDetail>().ToTable("tbl_transfer_detail").HasKey(k => new { k.StdAutid });

            builder.Entity<EyeFinding>().ToTable("tbl_eye_finding").HasKey(k => new { k.EfiAutid });
            builder.Entity<EyeFinding>(e => e.HasIndex(k => k.EfiSdesc).IsUnique(true));

            builder.Entity<LabTest>().ToTable("tbl_lab_test").HasKey(k => new { k.LtsAutid });
            builder.Entity<LabTest>(e => e.HasIndex(k => k.LtsSdesc).IsUnique(true));

            builder.Entity<Letter>().ToTable("tbl_letter").HasKey(k => new { k.LtrAutid });

            builder.Entity<LetterTemplate>().ToTable("tbl_letter_template").HasKey(k => new { k.LtmAutid });

            builder.Entity<MedicalExam>().ToTable("tbl_medical_exam").HasKey(k => new { k.PmeVstid });
            builder.Entity<MedicalExamDetail>().ToTable("tbl_medical_exam_detail").HasKey(k => new { k.MidVstid, k.MidPrmid });

            builder.Entity<MedicalPackage>().ToTable("tbl_medical_package").HasKey(k => new { k.MpcAutid });
            builder.Entity<MedicalPackage>(e => e.HasIndex(k => k.MpcDescr).IsUnique(true));

            builder.Entity<OrganizationProfile>().ToTable("tbl_organization_profile").HasKey(k => new { k.ComAutid });

            builder.Entity<Outlet>().ToTable("tbl_outlet").HasKey(k => new { k.OulAutid });
            builder.Entity<Outlet>(e => e.HasIndex(k => k.OulSname).IsUnique(true));

            builder.Entity<OutletStockAdjustment>().ToTable("tbl_outlet_stock_adjustment").HasKey(k => new { k.OsaAutid });

            builder.Entity<OutletStockAdjustmentDetail>().ToTable("tbl_outlet_stock_adjustment_detail").HasKey(k => new { k.OsdAutid });

            builder.Entity<OutletStockInfo>().ToTable("tbl_outlet_stock_info").HasKey(k => new { k.OsiStkid });

            builder.Entity<OutletStockMovement>().ToTable("tbl_outlet_stock_movement").HasKey(k => new { k.OsmAutid });

            builder.Entity<PackageProduct>().ToTable("tbl_package_product").HasKey(k => new { k.MpiPakid, k.MpiItmid });

            builder.Entity<PackageService>().ToTable("tbl_package_service").HasKey(k => new { k.PsePakid, k.PseSerid });

            builder.Entity<Panel>().ToTable("tbl_panel").HasKey(k => new { k.PnlAutid });
            builder.Entity<Panel>(e => e.HasIndex(k => k.PnlRegno).IsUnique(true));

            builder.Entity<PanelInvoice>().ToTable("tbl_panel_invoice").HasKey(k => new { k.PniAutid });

            builder.Entity<PanelInvoiceDetail>().ToTable("tbl_panel_invoice_detail").HasKey(k => new { k.PidInvid, k.PidVstid });

            builder.Entity<PanelInvoiceFormat>().ToTable("tbl_panel_invoice_format").HasKey(k => new { k.PifAutid });

            builder.Entity<Parameter>().ToTable("tbl_parameter").HasKey(k => new { k.PrmAutid });
            builder.Entity<Parameter>(e => e.HasIndex(k => new { k.PrmPdesc, k.PrmPtype }).IsUnique(true));

            builder.Entity<Patient>().ToTable("tbl_patient").HasKey(k => new { k.PrnAutid });
            builder.Entity<Patient>(e => e.HasIndex(k => k.PrnTelhp).IsUnique(true));
            builder.Entity<Patient>(e => e.HasIndex(k => k.PrnIcpno).IsUnique(true));
            builder.Entity<Patient>(e => e.HasIndex(k => k.PrnEmail).IsUnique(true));

            builder.Entity<PatientQueue>().ToTable("tbl_patient_queue").HasKey(k => k.PtqAutid);

            builder.Entity<PatientAllergy>().ToTable("tbl_patient_allergy").HasKey(k => new { k.PalAlrid, k.PalPatid });

            builder.Entity<PatientDocument>().ToTable("tbl_patient_document").HasKey(k => new { k.PtdFilid });

            builder.Entity<PatientGuarantor>().ToTable("tbl_patient_guarantor").HasKey(k => new { k.PgnPatid });

            builder.Entity<PatientNextKin>().ToTable("tbl_patient_nextkin").HasKey(k => new { k.PknPatid });

            builder.Entity<PatientVisit>().ToTable("tbl_patient_visit").HasKey(k => new { k.PvtVstid });

            builder.Entity<Product>().ToTable("tbl_product").HasKey(k => new { k.ProAutid });
            builder.Entity<Product>(e => e.HasIndex(k => k.ProBcode).IsUnique(true));
            builder.Entity<Product>(e => e.HasIndex(k => k.ProSname).IsUnique(true));

            builder.Entity<ProductPrice>().ToTable("tbl_product_price").HasKey(k => new { k.PprAutid });

            builder.Entity<Receipt>().ToTable("tbl_receipt").HasKey(k => new { k.RecAutid });

            builder.Entity<ReceiptDetail>().ToTable("tbl_receipt_detail").HasKey(k => new { k.RcdRecid, k.RcdPmode });

            builder.Entity<Room>().ToTable("tbl_room").HasKey(k => new { k.RomAutid });

            builder.Entity<RoomAllotment>().ToTable("tbl_room_allotment").HasKey(k => new { k.RalAutid });

            builder.Entity<Service>().ToTable("tbl_service").HasKey(k => new { k.SerAutid });
            builder.Entity<Service>(e => e.HasIndex(k => k.SerSname).IsUnique(true));

            builder.Entity<Staff>().ToTable("tbl_staff").HasKey(k => new { k.StfAutid });
            builder.Entity<Staff>(e => e.HasIndex(k => k.StfIcppt).IsUnique(true));
            builder.Entity<Staff>(e => e.HasIndex(k => k.StfTelhp).IsUnique(true));
            builder.Entity<Staff>(e => e.HasIndex(k => k.StfEmail).IsUnique(true));

            builder.Entity<StaffLeave>().ToTable("tbl_staff_leave").HasKey(k => new { k.LevAutid });

            builder.Entity<StockDetailedInfo>().ToTable("tbl_stock_detailed_info").HasKey(k => new { k.SdiMitid });

            builder.Entity<StockMovement>().ToTable("tbl_stock_movement").HasKey(k => new { k.SmoAutid });

            builder.Entity<Supplier>().ToTable("tbl_supplier").HasKey(k => new { k.SupAutid });
            builder.Entity<Supplier>(e => e.HasIndex(k => k.SupRegno).IsUnique(true));

            builder.Entity<Support>().ToTable("tbl_support").HasKey(k => new { k.SrtAutid });

            builder.Entity<PatientQueue>().ToTable("tbl_patient_queue").HasKey(k => k.PtqAutid);

            builder.Entity<VisitCaseDetail>().ToTable("tbl_visit_case_detail").HasKey(k => new { k.VcaVstid });

            builder.Entity<VisitChildDetail>().ToTable("tbl_visit_child_detail").HasKey(k => new { k.VcdVstid });

            builder.Entity<VisitDiagnosisDetail>().ToTable("tbl_visit_diagnosis_detail").HasKey(k => new { k.VddDgnid, k.VddVstid });

            builder.Entity<VisitEyeFinding>().ToTable("tbl_visit_eye_finding").HasKey(k => new { k.VefFndid, k.VefVstid });

            builder.Entity<VisitInvestigationDetail>().ToTable("tbl_visit_investigation_detail").HasKey(k => new { k.VidInvid, k.VidVstid });

            builder.Entity<VisitLabDetail>().ToTable("tbl_visit_lab_detail").HasKey(k => new { k.VlbVstid, k.VlbTstid });

            builder.Entity<VisitParameterDetail>().ToTable("tbl_visit_parameter_detail").HasKey(k => new { k.VprVstid, k.VprPrmid });

            builder.Entity<VisitProductDetail>().ToTable("tbl_visit_product_detail").HasKey(k => new { k.VpdTrnid });

            builder.Entity<VisitServiceDetail>().ToTable("tbl_visit_service_detail").HasKey(k => new { k.VsdVstid, k.VsdSerid });
        }
        public DbSet<Allergy> GetAllergy => Set<Allergy>();
        public DbSet<AllergyDrug> GetAllergyDrugs => Set<AllergyDrug>();
        public DbSet<Appointment> GetAppointments => Set<Appointment>();
        public DbSet<Diagnosis> GetDiagnosis => Set<Diagnosis>();
        public DbSet<Investigation> GetInvestigations => Set<Investigation>();
        public DbSet<DoctorSchedule> GetDoctorSchedules => Set<DoctorSchedule>();
        public DbSet<StockAdjustment> GetAdjustments => Set<StockAdjustment>();
        public DbSet<StockAdjustmentDetail> GetAdjustmentDetails => Set<StockAdjustmentDetail>();
        public DbSet<StockDispensingInfo> GetDispensingInfos => Set<StockDispensingInfo>();
        public DbSet<StockGoodsReceiveNote> GetGoodsReceiveNotes => Set<StockGoodsReceiveNote>();
        public DbSet<StockGoodsReceiveNoteDetail> GetGoodsReceiveNoteDetails => Set<StockGoodsReceiveNoteDetail>();
        public DbSet<StockIssue> GetIssues => Set<StockIssue>();
        public DbSet<StockIssueDetail> GetIssueDetails => Set<StockIssueDetail>();
        public DbSet<StockPurchaseOrder> GetPurchaseOrders => Set<StockPurchaseOrder>();
        public DbSet<StockPurchaseOrderDetail> GetPurchaseOrderDetails => Set<StockPurchaseOrderDetail>();
        public DbSet<StockTransfer> GetTransfers => Set<StockTransfer>();
        public DbSet<StockTransferDetail> GetTransferDetails => Set<StockTransferDetail>();
        public DbSet<EyeFinding> GetEyeFindings => Set<EyeFinding>();
        public DbSet<LabTest> GetLabTests => Set<LabTest>();
        public DbSet<Letter> GetLetters => Set<Letter>();
        public DbSet<LetterTemplate> GetLetterTemplates => Set<LetterTemplate>();
        public DbSet<MedicalExam> GetMedicalExams => Set<MedicalExam>();
        public DbSet<MedicalExamDetail> GetMedicalExamDetails => Set<MedicalExamDetail>();
        public DbSet<OrganizationProfile> GetOrganizationProfiles => Set<OrganizationProfile>();
        public DbSet<Outlet> GetOutlets => Set<Outlet>();
        public DbSet<OutletStockAdjustment> GetOutletStockAdjustments => Set<OutletStockAdjustment>();
        public DbSet<OutletStockAdjustmentDetail> GetOutletStockAdjustmentDetails => Set<OutletStockAdjustmentDetail>();
        public DbSet<OutletStockInfo> GetOutletStockInfos => Set<OutletStockInfo>();
        public DbSet<OutletStockMovement> GetOutletStockMovements => Set<OutletStockMovement>();
        public DbSet<PackageProduct> GetPackageProducts => Set<PackageProduct>();
        public DbSet<PackageService> GetPackageServices => Set<PackageService>();
        public DbSet<Panel> GetPanels => Set<Panel>();
        public DbSet<PanelInvoice> GetPanelInvoices => Set<PanelInvoice>();
        public DbSet<PanelInvoiceDetail> GetPanelInvoiceDetails => Set<PanelInvoiceDetail>();
        public DbSet<PanelInvoiceFormat> GetPanelInvoiceFormats => Set<PanelInvoiceFormat>();
        public DbSet<Parameter> GetParameters => Set<Parameter>();
        public DbSet<Patient> GetPatients => Set<Patient>();
        public DbSet<PatientAllergy> GetPatientAllergies => Set<PatientAllergy>();
        public DbSet<PatientDocument> GetPatientDocuments => Set<PatientDocument>();
        public DbSet<PatientGuarantor> GetPatientGuarantors => Set<PatientGuarantor>();
        public DbSet<PatientNextKin> GetPatientNextKins => Set<PatientNextKin>();
        public DbSet<PatientVisit> GetPatientVisits => Set<PatientVisit>();
        public DbSet<PatientQueue> GetPatientQueues => Set<PatientQueue>();
        public DbSet<Product> GetProducts => Set<Product>();
        public DbSet<ProductPrice> GetProductPrices => Set<ProductPrice>();
        public DbSet<Receipt> GetReceipts => Set<Receipt>();
        public DbSet<ReceiptDetail> GetReceiptDetails => Set<ReceiptDetail>();
        public DbSet<Room> GetRooms => Set<Room>();
        public DbSet<RoomAllotment> GetRoomAllotments => Set<RoomAllotment>();
        public DbSet<Service> GetServices => Set<Service>();
        public DbSet<Staff> GetStaffs => Set<Staff>();
        public DbSet<StaffLeave> GetStaffLeaves => Set<StaffLeave>();
        public DbSet<StockDetailedInfo> GetStockDetailedInfos => Set<StockDetailedInfo>();
        public DbSet<StockMovement> GetStockMovements => Set<StockMovement>();
        public DbSet<Supplier> GetSuppliers => Set<Supplier>();
        public DbSet<Support> GetSupports => Set<Support>();
        public DbSet<VisitCaseDetail> GetVisitCaseDetails => Set<VisitCaseDetail>();
        public DbSet<VisitChildDetail> GetVisitChildDetails => Set<VisitChildDetail>();
        public DbSet<VisitDiagnosisDetail> GetVisitDiagnosisDetails => Set<VisitDiagnosisDetail>();
        public DbSet<VisitEyeFinding> GetVisitEyeFindings => Set<VisitEyeFinding>();
        public DbSet<VisitInvestigationDetail> GetVisitInvestigationDetails => Set<VisitInvestigationDetail>();
        public DbSet<VisitLabDetail> GetVisitLabDetails => Set<VisitLabDetail>();
        public DbSet<VisitParameterDetail> GetVisitParameterDetails => Set<VisitParameterDetail>();
        public DbSet<VisitProductDetail> GetVisitProductDetails => Set<VisitProductDetail>();
        public DbSet<VisitServiceDetail> GetVisitServiceDetails => Set<VisitServiceDetail>();

    }
}

