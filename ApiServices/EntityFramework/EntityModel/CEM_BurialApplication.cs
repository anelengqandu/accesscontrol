//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiServices.EntityFramework.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class CEM_BurialApplication
    {
        public long app_ID { get; set; }
        public string app_Deceased_Fname { get; set; }
        public string app_Deceased_Sname { get; set; }
        public string app_Deceased_Address { get; set; }
        public Nullable<System.DateTime> app_Deceased_DOB { get; set; }
        public Nullable<System.DateTime> app_Deceased_DOD { get; set; }
        public Nullable<int> app_Deceased_Age { get; set; }
        public string app_Deceased_Resident { get; set; }
        public Nullable<long> app_Deceased_CODID { get; set; }
        public string app_Deceased_Gender { get; set; }
        public string app_Deceased_Nationality { get; set; }
        public string app_Deceased_Race { get; set; }
        public string app_NoK_Title { get; set; }
        public string app_NoK_Fname { get; set; }
        public string app_NoK_Sname { get; set; }
        public string app_NoK_Address { get; set; }
        public string app_NoK_Tel { get; set; }
        public string app_NoK_Cell { get; set; }
        public string app_NoK_EMail { get; set; }
        public string app_Applicant_Relationship { get; set; }
        public string app_Applicant_Title { get; set; }
        public string app_Applicant_Fname { get; set; }
        public string app_Applicant_Sname { get; set; }
        public string app_Applicant_Address { get; set; }
        public string app_Applicant_Tel { get; set; }
        public string app_Applicant_Cell { get; set; }
        public string app_Applicant_Email { get; set; }
        public string app_Applicant_IDcheck { get; set; }
        public string app_Applicant_IDDoc { get; set; }
        public string app_Applicant_Deathcheck { get; set; }
        public string app_Applicant_DeathDoc { get; set; }
        public string app_Applicant_BurialOrdercheck { get; set; }
        public string app_Applicant_BurialOrderDoc { get; set; }
        public Nullable<long> app_Quote_CemeteryID { get; set; }
        public string app_Quote_Total { get; set; }
        public string app_Receipt_Type { get; set; }
        public string app_Receipt_IssuedBy { get; set; }
        public Nullable<System.DateTime> app_Receipt_Date { get; set; }
        public string app_Receipt_Nr { get; set; }
        public Nullable<decimal> app_Receipt_Amount { get; set; }
        public Nullable<long> app_Allocation_CemeteryID { get; set; }
        public Nullable<long> app_Allocation_SectionID { get; set; }
        public Nullable<long> app_Allocation_BlockID { get; set; }
        public string app_Allocation_Row { get; set; }
        public string app_Allocation_Grave { get; set; }
        public Nullable<System.DateTime> app_Allocation_BurialDate { get; set; }
        public string app_Allocation_BurialTime { get; set; }
        public Nullable<long> app_CaptureUserID { get; set; }
        public Nullable<System.DateTime> app_CaptureDate { get; set; }
        public Nullable<long> app_UpdateUserID { get; set; }
        public Nullable<System.DateTime> app_UpdateDate { get; set; }
        public Nullable<long> app_BurialID { get; set; }
        public Nullable<long> app_UndertakerID { get; set; }
        public string app_Converted { get; set; }
        public string app_PrintedQuote { get; set; }
        public string app_BO_Number { get; set; }
        public string app_BO_IssuedBy { get; set; }
        public string app_Deceased_IDNr { get; set; }
        public string app_Deceased_IDType { get; set; }
        public string App_UndertakerName { get; set; }
        public string app_Deleted { get; set; }
        public Nullable<int> app_Stage { get; set; }
        public string app_COD_Description { get; set; }
        public string app_COD_Contagious_Disease { get; set; }
        public string app_Remarks { get; set; }
        public string app_Applicant_AppIDcheck { get; set; }
        public string app_Applicant_Cremationcheck { get; set; }
        public string app_Cremation_OriginalPaycheck { get; set; }
        public string app_Cremation_ScheduleEcheck { get; set; }
        public string app_Cremation_ScheduleFcheck { get; set; }
        public string app_Cremation_ScheduleGcheck { get; set; }
        public string app_Cremation_ScheduleHcheck { get; set; }
        public Nullable<long> GRAVETYPEID { get; set; }
        public string app_NoKIDNumber { get; set; }
        public string app_NoKRelation { get; set; }
        public string app_Receipt_Comment { get; set; }
        public Nullable<int> app_Exceeded_Threshold { get; set; }
        public Nullable<int> app_Deceased_Age_Months { get; set; }
        public Nullable<int> app_Deceased_Age_Days { get; set; }
        public Nullable<int> app_Deceased_Age_Hours { get; set; }
        public string app_Deceased_Contagious_Description { get; set; }
        public string app_Burial_Register_Number { get; set; }
        public string app_DuplicateReceiptComment { get; set; }
        public Nullable<long> ServiceTypeId { get; set; }
        public string ToLongitude { get; set; }
        public string ToLatitude { get; set; }
        public string DeleteApplicationReason { get; set; }
        public string AuthoriseSubSequentBurialReason { get; set; }
        public Nullable<int> AccessCount { get; set; }
        public Nullable<System.DateTime> OTPDate { get; set; }
        public Nullable<int> MunicipalApplicantId { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public Nullable<int> ServiceTypeCategoryId { get; set; }
        public Nullable<int> CemeterySupervisorId { get; set; }
        public Nullable<int> MonumentalCompanyId { get; set; }
        public Nullable<int> app_Applicant_BurialPlan { get; set; }
        public Nullable<int> app_Applicant_GravePhoto { get; set; }
        public Nullable<int> ExhumedFromApplicationId { get; set; }
        public string ExhumationTo { get; set; }
        public string ExhumationDetails { get; set; }
        public Nullable<long> ServiceTypeRegisterNumber { get; set; }
        public Nullable<int> PauperType { get; set; }
        public Nullable<int> ChkAffidavit { get; set; }
        public Nullable<int> ChkCremationCertificate { get; set; }
        public string EnviromentHealthNumber { get; set; }
    }
}
