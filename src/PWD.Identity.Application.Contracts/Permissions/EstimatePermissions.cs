namespace PWD.Identity.Permissions
{
    public static class EstimatePermissions
    {
        public const string GroupName = "PWDEstimate";

        public static class Projects
        {
            public const string Default = GroupName + ".Projects";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class ProjectVariables
        {
            public const string Default = GroupName + ".ProjectVariables";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class ProjectAnalysisItems
        {
            public const string Default = GroupName + ".ProjectAnalysisItems";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class CivilEstimates
        {
            public const string Default = GroupName + ".CivilEstimates";
            public const string Create = Default + ".Create";
            public const string PopulateOutput = Default + ".PopulateOutput";
        }
        public static class CivilReview
        {
            public const string Default = GroupName + ".CivilReview";
            public const string PopulateOutput = Default + ".PopulateOutput";
            public const string OTM = Default + ".OTM";
            public const string LTM = Default + ".LTM";
        }

        public static class EMEstimates
        {
            public const string Default = GroupName + ".EMEstimates";
            public const string Create = Default + ".Create";
            public const string PopulateOutput = Default + ".PopulateOutput";
        }

        public static class EMReview
        {
            public const string Default = GroupName + ".EMReview";
            public const string PopulateOutput = Default + ".PopulateOutput";
            public const string OTM = Default + ".OTM";
            public const string LTM = Default + ".LTM";
        }
        //public static class SiteFacilities
        //{
        //    public const string Default = GroupName + ".SiteFacilities";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class RCC
        //{
        //    public const string Default = GroupName + ".RCC";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Wall
        //{
        //    public const string Default = GroupName + ".Wall";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class ConcreteBlock
        //{
        //    public const string Default = GroupName + ".ConcreteBlock";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Door
        //{
        //    public const string Default = GroupName + ".Door";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Window
        //{
        //    public const string Default = GroupName + ".Window";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class MosaicTiles
        //{
        //    public const string Default = GroupName + ".MosaicTiles";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class PlasterPaint
        //{
        //    public const string Default = GroupName + ".PlasterPaint";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Sanitary
        //{
        //    public const string Default = GroupName + ".Sanitary";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class LimeTerracing
        //{
        //    public const string Default = GroupName + ".LimeTerracing";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class FalseCeiling
        //{
        //    public const string Default = GroupName + ".FalseCeiling";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class FabricationConstructionJoint
        //{
        //    public const string Default = GroupName + ".FabricationConstructionJoint";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class RailingFencing
        //{
        //    public const string Default = GroupName + ".RailingFencing";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class RoadPavement
        //{
        //    public const string Default = GroupName + ".RoadPavement";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Ferrocement
        //{
        //    public const string Default = GroupName + ".Ferrocement";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Arboriculture
        //{
        //    public const string Default = GroupName + ".Arboriculture";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Tubewell
        //{
        //    public const string Default = GroupName + ".Tubewell";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class GasConnection
        //{
        //    public const string Default = GroupName + ".GasConnection";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class TermiteTreatment
        //{
        //    public const string Default = GroupName + ".TermiteTreatment";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class SubSoilInvestigation
        //{
        //    public const string Default = GroupName + ".SubSoilInvestigation";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class MiscellaneousItems
        //{
        //    public const string Default = GroupName + ".MiscellaneousItems";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}


        //public static class CableSetup
        //{
        //    public const string Default = GroupName + ".Cable";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Transformer
        //{
        //    public const string Default = GroupName + ".Transformer";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Generator
        //{
        //    public const string Default = GroupName + ".Generator";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Lift
        //{
        //    public const string Default = GroupName + ".Lift";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Pump
        //{
        //    public const string Default = GroupName + ".Pump";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class AC
        //{
        //    public const string Default = GroupName + ".AC";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class MedicalGas
        //{
        //    public const string Default = GroupName + ".MedicalGas";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class OverheadDistribution
        //{
        //    public const string Default = GroupName + ".OverheadDistribution";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class LightFittings
        //{
        //    public const string Default = GroupName + ".LightFittings";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class StageLight
        //{
        //    public const string Default = GroupName + ".StageLight";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class WoodFurniture
        //{
        //    public const string Default = GroupName + ".WoodFurniture";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class MultimediaSystem
        //{
        //    public const string Default = GroupName + ".MultimediaSystem";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class SolarSystem
        //{
        //    public const string Default = GroupName + ".SolarSystem";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class VehicleRepair
        //{
        //    public const string Default = GroupName + ".VehicleRepair";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class FireDetection
        //{
        //    public const string Default = GroupName + ".FireDetection";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class Surveillance
        //{
        //    public const string Default = GroupName + ".Surveillance";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class SoundSystem
        //{
        //    public const string Default = GroupName + ".SoundSystem";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class InternetSystem
        //{
        //    public const string Default = GroupName + ".InternetSystem";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}

        //public static class ElectricalItems
        //{
        //    public const string Default = GroupName + ".ElectricalItems";
        //    public const string Create = Default + ".Create";
        //    public const string PopulateOutput = Default + ".PopulateOutput";
        //}


    }
}
