using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Web.Models
{
    public class EpisodeModel
    {
        public int id { get; set; }
        public DiagnosisModel DiagnosisModel { get; set; }
        public MedicationModel MedicationModel { get; set; }
        public ReferralModel  ReferralModel { get; set; }
        public ARFModel ARFModel { get; set;}
        public RHDModel RHDModel { get; set; }
    }

    public class DiagnosisModel 
    {
        public int id { get; set; }
        public  DateTime DiagnosisDate { get; set; }
        public  string Diagnosisby { get; set; }
        public  string Name { get; set; }
        public  string ReferralInformation { get; set; }
        public  string PreganancyPostDelivery { get; set; }

    }
    public class ReferralModel
    {
        public virtual DateTime ReferralDate { get; set; }
        public virtual string ReferralSource { get; set; }
        public virtual string Role { get; set; }
        public virtual string ReferralFacility { get; set; }
        public virtual string ReferredBy { get; set; }

    }
    public class MedicationModel
    {
   
        public int id { get; set; }
        public  string BenzathineFrequenency { get; set; }
        public  DateTime MedicationStartDate { get; set; }
        public  string OtherProphylaxis { get; set; }
        public  bool Warfarin { get; set; }
        public  IList<HeartFailureMedicationModel> HeartFailureMedications { get; set; }
    }

    public class HeartFailureMedicationModel
    {
        public int id { get; set; }
        public  string MedicationName { get; set; }
    }

    public class ARFModel
    {
        public int id { get; set; }
        public  string ARFDiagnosisSuspected { get; set; }
        public  string StrepEvidence { get; set; }
        public  IList<ARFMajorCriteriaModel> ARFMajorCriteria { get; set; }
        public  IList<ARFMinorCriteriaModel> ARFMinorCriteria { get; set; }
       
    }

    public class ARFMajorCriteriaModel
    {
        public int id { get; set; }
        public string Description { get; set; }
        
    }
    public class ARFMinorCriteriaModel
    {
        public int id { get; set; }
        public  string Description { get; set; }
        
    }
    public class RHDModel
    {

        public int id { get; set; }
        public  bool RHDDiagnosisSuspected { get; set; }
        public  bool RHDPreviouslyDiagnosed { get; set; }
        public  string OtherEvidence { get; set; }
        public  string EchoResult { get; set; }
        public  IList<EvidenceModel> Evidence { get; set; }

    }
    public class EvidenceModel
    {
        public int id { get; set; }
        public  string Description { get; set; }
    }
    }