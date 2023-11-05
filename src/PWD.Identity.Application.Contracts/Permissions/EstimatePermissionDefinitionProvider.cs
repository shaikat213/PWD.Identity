using PWD.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PWD.Identity.Permissions
{
    public class EstimatePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(EstimatePermissions.GroupName, L("Permission:Estimate"));

            var projectsPermission = group.AddPermission(EstimatePermissions.Projects.Default, L("Permission:Projects"));
            projectsPermission.AddChild(EstimatePermissions.Projects.Create, L("Permission:Projects.Create"));
            projectsPermission.AddChild(EstimatePermissions.Projects.Edit, L("Permission:Projects.Edit"));
            projectsPermission.AddChild(EstimatePermissions.Projects.Delete, L("Permission:Projects.Delete"));

            //var varibalePermission = group.AddPermission(EstimatePermissions.ProjectVariables.Default, L("Permission:ProjectVariables"));
            //varibalePermission.AddChild(EstimatePermissions.ProjectVariables.Create, L("Permission:ProjectVariables.Create"));
            //varibalePermission.AddChild(EstimatePermissions.ProjectVariables.Edit, L("Permission:ProjectVariables.Edit"));
            //varibalePermission.AddChild(EstimatePermissions.ProjectVariables.Delete, L("Permission:ProjectVariables.Delete"));

            var analysisItemPermission = group.AddPermission(EstimatePermissions.ProjectAnalysisItems.Default, L("Permission:ProjectAnalysisItems"));
            analysisItemPermission.AddChild(EstimatePermissions.ProjectAnalysisItems.Create, L("Permission:ProjectAnalysisItems.Create"));
            analysisItemPermission.AddChild(EstimatePermissions.ProjectAnalysisItems.Edit, L("Permission:ProjectAnalysisItems.Edit"));
            analysisItemPermission.AddChild(EstimatePermissions.ProjectAnalysisItems.Delete, L("Permission:ProjectAnalysisItems.Delete"));

            var civilEstimatePermission = group.AddPermission(EstimatePermissions.CivilEstimates.Default, L("Permission:CivilEstimates"));
            civilEstimatePermission.AddChild(EstimatePermissions.CivilEstimates.Create, L("Permission:CivilEstimates.Create"));
            civilEstimatePermission.AddChild(EstimatePermissions.CivilEstimates.PopulateOutput, L("Permission:CivilEstimates.PopulateOutput"));

            var reviewPermission = group.AddPermission(EstimatePermissions.CivilReview.Default, L("Permission:CivilReview"));
            reviewPermission.AddChild(EstimatePermissions.CivilReview.PopulateOutput, L("Permission:CivilReview.PopulateOutput"));
            reviewPermission.AddChild(EstimatePermissions.CivilReview.OTM, L("Permission:CivilReview.OTM"));
            reviewPermission.AddChild(EstimatePermissions.CivilReview.LTM, L("Permission:CivilReview.LTM"));

            var emEstimatePermission = group.AddPermission(EstimatePermissions.EMEstimates.Default, L("Permission:EMEstimates"));
            emEstimatePermission.AddChild(EstimatePermissions.EMEstimates.Create, L("Permission:EMEstimates.Create"));
            emEstimatePermission.AddChild(EstimatePermissions.EMEstimates.PopulateOutput, L("Permission:EMEstimates.PopulateOutput"));


            var emReviewPermission = group.AddPermission(EstimatePermissions.EMReview.Default, L("Permission:EMReview"));
            emReviewPermission.AddChild(EstimatePermissions.EMReview.PopulateOutput, L("Permission:EMReview.PopulateOutput"));
            emReviewPermission.AddChild(EstimatePermissions.EMReview.OTM, L("Permission:EMReview.OTM"));
            emReviewPermission.AddChild(EstimatePermissions.EMReview.LTM, L("Permission:EMReview.LTM"));

            //var sityFacilityPermission = group.AddPermission(EstimatePermissions.SiteFacilities.Default, L("Permission:SiteFacilities"));
            //sityFacilityPermission.AddChild(EstimatePermissions.SiteFacilities.Create, L("Permission:SiteFacilities.Create"));
            //sityFacilityPermission.AddChild(EstimatePermissions.SiteFacilities.PopulateOutput, L("Permission:SiteFacilities.PopulateOutput"));

            //var rccPermission = group.AddPermission(EstimatePermissions.RCC.Default, L("Permission:RCC"));
            //rccPermission.AddChild(EstimatePermissions.RCC.Create, L("Permission:RCC.Create"));
            //rccPermission.AddChild(EstimatePermissions.RCC.PopulateOutput, L("Permission:RCC.PopulateOutput"));

            //var wallPermission = group.AddPermission(EstimatePermissions.Wall.Default, L("Permission:Wall"));
            //wallPermission.AddChild(EstimatePermissions.Wall.Create, L("Permission:Wall.Create"));
            //wallPermission.AddChild(EstimatePermissions.Wall.PopulateOutput, L("Permission:Wall.PopulateOutput"));

            //var concreteBlockPermission = group.AddPermission(EstimatePermissions.ConcreteBlock.Default, L("Permission:ConcreteBlock"));
            //concreteBlockPermission.AddChild(EstimatePermissions.ConcreteBlock.Create, L("Permission:ConcreteBlock.Create"));
            //concreteBlockPermission.AddChild(EstimatePermissions.ConcreteBlock.PopulateOutput, L("Permission:ConcreteBlock.PopulateOutput"));

            //var doorPermission = group.AddPermission(EstimatePermissions.Door.Default, L("Permission:Door"));
            //doorPermission.AddChild(EstimatePermissions.Door.Create, L("Permission:Door.Create"));
            //doorPermission.AddChild(EstimatePermissions.Door.PopulateOutput, L("Permission:Door.PopulateOutput"));

            //var windowPermission = group.AddPermission(EstimatePermissions.Window.Default, L("Permission:Window"));
            //windowPermission.AddChild(EstimatePermissions.Window.Create, L("Permission:Window.Create"));
            //windowPermission.AddChild(EstimatePermissions.Window.PopulateOutput, L("Permission:Window.PopulateOutput"));

            //var mosaicTilesPermission = group.AddPermission(EstimatePermissions.MosaicTiles.Default, L("Permission:MosaicTiles"));
            //mosaicTilesPermission.AddChild(EstimatePermissions.MosaicTiles.Create, L("Permission:MosaicTiles.Create"));
            //mosaicTilesPermission.AddChild(EstimatePermissions.MosaicTiles.PopulateOutput, L("Permission:MosaicTiles.PopulateOutput"));

            //var plasterPaintPermission = group.AddPermission(EstimatePermissions.PlasterPaint.Default, L("Permission:PlasterPaint"));
            //plasterPaintPermission.AddChild(EstimatePermissions.PlasterPaint.Create, L("Permission:PlasterPaint.Create"));
            //plasterPaintPermission.AddChild(EstimatePermissions.PlasterPaint.PopulateOutput, L("Permission:PlasterPaint.PopulateOutput"));

            //var sanitaryPermission = group.AddPermission(EstimatePermissions.Sanitary.Default, L("Permission:Sanitary"));
            //sanitaryPermission.AddChild(EstimatePermissions.Sanitary.Create, L("Permission:Sanitary.Create"));
            //sanitaryPermission.AddChild(EstimatePermissions.Sanitary.PopulateOutput, L("Permission:Sanitary.PopulateOutput"));

            //var limeTerracingPermission = group.AddPermission(EstimatePermissions.LimeTerracing.Default, L("Permission:LimeTerracing"));
            //limeTerracingPermission.AddChild(EstimatePermissions.LimeTerracing.Create, L("Permission:LimeTerracing.Create"));
            //limeTerracingPermission.AddChild(EstimatePermissions.LimeTerracing.PopulateOutput, L("Permission:LimeTerracing.PopulateOutput"));

            //var falseCeilingPermission = group.AddPermission(EstimatePermissions.FalseCeiling.Default, L("Permission:FalseCeiling"));
            //falseCeilingPermission.AddChild(EstimatePermissions.FalseCeiling.Create, L("Permission:FalseCeiling.Create"));
            //falseCeilingPermission.AddChild(EstimatePermissions.FalseCeiling.PopulateOutput, L("Permission:FalseCeiling.PopulateOutput"));

            //var fabricationConstructionJointPermission = group.AddPermission(EstimatePermissions.FabricationConstructionJoint.Default, L("Permission:FabricationConstructionJoint"));
            //fabricationConstructionJointPermission.AddChild(EstimatePermissions.FabricationConstructionJoint.Create, L("Permission:FabricationConstructionJoint.Create"));
            //fabricationConstructionJointPermission.AddChild(EstimatePermissions.FabricationConstructionJoint.PopulateOutput, L("Permission:FabricationConstructionJoint.PopulateOutput"));

            //var railingFencingPermission = group.AddPermission(EstimatePermissions.RailingFencing.Default, L("Permission:RailingFencing"));
            //railingFencingPermission.AddChild(EstimatePermissions.RailingFencing.Create, L("Permission:RailingFencing.Create"));
            //railingFencingPermission.AddChild(EstimatePermissions.RailingFencing.PopulateOutput, L("Permission:RailingFencing.PopulateOutput"));


            //var roadPavementPermission = group.AddPermission(EstimatePermissions.RoadPavement.Default, L("Permission:RoadPavement"));
            //roadPavementPermission.AddChild(EstimatePermissions.RoadPavement.Create, L("Permission:RoadPavement.Create"));
            //roadPavementPermission.AddChild(EstimatePermissions.RoadPavement.PopulateOutput, L("Permission:RoadPavement.PopulateOutput"));

            //var ferrocementPermission = group.AddPermission(EstimatePermissions.Ferrocement.Default, L("Permission:Ferrocement"));
            //ferrocementPermission.AddChild(EstimatePermissions.Ferrocement.Create, L("Permission:Ferrocement.Create"));
            //ferrocementPermission.AddChild(EstimatePermissions.Ferrocement.PopulateOutput, L("Permission:Ferrocement.PopulateOutput"));

            //var arboriculturePermission = group.AddPermission(EstimatePermissions.Arboriculture.Default, L("Permission:Arboriculture"));
            //arboriculturePermission.AddChild(EstimatePermissions.Arboriculture.Create, L("Permission:Arboriculture.Create"));
            //arboriculturePermission.AddChild(EstimatePermissions.Arboriculture.PopulateOutput, L("Permission:Arboriculture.PopulateOutput"));

            //var tubewellPermission = group.AddPermission(EstimatePermissions.Tubewell.Default, L("Permission:Tubewell"));
            //tubewellPermission.AddChild(EstimatePermissions.Tubewell.Create, L("Permission:Tubewell.Create"));
            //tubewellPermission.AddChild(EstimatePermissions.Tubewell.PopulateOutput, L("Permission:Tubewell.PopulateOutput"));

            //var gasConnectionPermission = group.AddPermission(EstimatePermissions.GasConnection.Default, L("Permission:GasConnection"));
            //gasConnectionPermission.AddChild(EstimatePermissions.GasConnection.Create, L("Permission:GasConnection.Create"));
            //gasConnectionPermission.AddChild(EstimatePermissions.GasConnection.PopulateOutput, L("Permission:GasConnection.PopulateOutput"));

            //var termiteTreatmentPermission = group.AddPermission(EstimatePermissions.TermiteTreatment.Default, L("Permission:TermiteTreatment"));
            //termiteTreatmentPermission.AddChild(EstimatePermissions.TermiteTreatment.Create, L("Permission:TermiteTreatment.Create"));
            //termiteTreatmentPermission.AddChild(EstimatePermissions.TermiteTreatment.PopulateOutput, L("Permission:TermiteTreatment.PopulateOutput"));

            //var subSoilInvestigationPermission = group.AddPermission(EstimatePermissions.SubSoilInvestigation.Default, L("Permission:SubSoilInvestigation"));
            //subSoilInvestigationPermission.AddChild(EstimatePermissions.SubSoilInvestigation.Create, L("Permission:SubSoilInvestigation.Create"));
            //subSoilInvestigationPermission.AddChild(EstimatePermissions.SubSoilInvestigation.PopulateOutput, L("Permission:SubSoilInvestigation.PopulateOutput"));

            //var miscellaneousItemsPermission = group.AddPermission(EstimatePermissions.MiscellaneousItems.Default, L("Permission:MiscellaneousItems"));
            //miscellaneousItemsPermission.AddChild(EstimatePermissions.MiscellaneousItems.Create, L("Permission:MiscellaneousItems.Create"));
            //miscellaneousItemsPermission.AddChild(EstimatePermissions.MiscellaneousItems.PopulateOutput, L("Permission:MiscellaneousItems.PopulateOutput"));


            //var cableSetupPermission = group.AddPermission(EstimatePermissions.CableSetup.Default, L("Permission:Cable"));
            //cableSetupPermission.AddChild(EstimatePermissions.CableSetup.Create, L("Permission:Cable.Create"));
            //cableSetupPermission.AddChild(EstimatePermissions.CableSetup.PopulateOutput, L("Permission:Cable.PopulateOutput"));

            //var transformerConfigPermission = group.AddPermission(EstimatePermissions.Transformer.Default, L("Permission:Transformer"));
            //transformerConfigPermission.AddChild(EstimatePermissions.Transformer.Create, L("Permission:Transformer.Create"));
            //transformerConfigPermission.AddChild(EstimatePermissions.Transformer.PopulateOutput, L("Permission:Transformer.PopulateOutput"));

            //var generatorSetupPermission = group.AddPermission(EstimatePermissions.Generator.Default, L("Permission:Generator"));
            //generatorSetupPermission.AddChild(EstimatePermissions.Generator.Create, L("Permission:Generator.Create"));
            //generatorSetupPermission.AddChild(EstimatePermissions.Generator.PopulateOutput, L("Permission:Generator.PopulateOutput"));

            //var liftSetupPermission = group.AddPermission(EstimatePermissions.Lift.Default, L("Permission:Lift"));
            //liftSetupPermission.AddChild(EstimatePermissions.Lift.Create, L("Permission:Lift.Create"));
            //liftSetupPermission.AddChild(EstimatePermissions.Lift.PopulateOutput, L("Permission:Lift.PopulateOutput"));

            //var pumpSetupPermission = group.AddPermission(EstimatePermissions.Pump.Default, L("Permission:Pump"));
            //pumpSetupPermission.AddChild(EstimatePermissions.Pump.Create, L("Permission:Pump.Create"));
            //pumpSetupPermission.AddChild(EstimatePermissions.Pump.PopulateOutput, L("Permission:Pump.PopulateOutput"));

            //var acSetupPermission = group.AddPermission(EstimatePermissions.AC.Default, L("Permission:AC"));
            //acSetupPermission.AddChild(EstimatePermissions.AC.Create, L("Permission:AC.Create"));
            //acSetupPermission.AddChild(EstimatePermissions.AC.PopulateOutput, L("Permission:AC.PopulateOutput"));

            //var medicalGasSetupPermission = group.AddPermission(EstimatePermissions.MedicalGas.Default, L("Permission:MedicalGas"));
            //medicalGasSetupPermission.AddChild(EstimatePermissions.MedicalGas.Create, L("Permission:MedicalGas.Create"));
            //medicalGasSetupPermission.AddChild(EstimatePermissions.MedicalGas.PopulateOutput, L("Permission:MedicalGas.PopulateOutput"));

            //var overheadDistributionSetupPermission = group.AddPermission(EstimatePermissions.OverheadDistribution.Default, L("Permission:OverheadDistribution"));
            //overheadDistributionSetupPermission.AddChild(EstimatePermissions.OverheadDistribution.Create, L("Permission:OverheadDistribution.Create"));
            //overheadDistributionSetupPermission.AddChild(EstimatePermissions.OverheadDistribution.PopulateOutput, L("Permission:OverheadDistribution.PopulateOutput"));

            //var lightFittingsSetupPermission = group.AddPermission(EstimatePermissions.LightFittings.Default, L("Permission:LightFittings"));
            //lightFittingsSetupPermission.AddChild(EstimatePermissions.LightFittings.Create, L("Permission:LightFittings.Create"));
            //lightFittingsSetupPermission.AddChild(EstimatePermissions.LightFittings.PopulateOutput, L("Permission:LightFittings.PopulateOutput"));

            //var stageLightSetupPermission = group.AddPermission(EstimatePermissions.StageLight.Default, L("Permission:StageLight"));
            //stageLightSetupPermission.AddChild(EstimatePermissions.StageLight.Create, L("Permission:StageLight.Create"));
            //stageLightSetupPermission.AddChild(EstimatePermissions.StageLight.PopulateOutput, L("Permission:StageLight.PopulateOutput"));

            //var woodFurnitureSetupPermission = group.AddPermission(EstimatePermissions.WoodFurniture.Default, L("Permission:WoodFurniture"));
            //woodFurnitureSetupPermission.AddChild(EstimatePermissions.WoodFurniture.Create, L("Permission:WoodFurniture.Create"));
            //woodFurnitureSetupPermission.AddChild(EstimatePermissions.WoodFurniture.PopulateOutput, L("Permission:WoodFurniture.PopulateOutput"));

            //var multimediaSystemSetupPermission = group.AddPermission(EstimatePermissions.MultimediaSystem.Default, L("Permission:MultimediaSystem"));
            //multimediaSystemSetupPermission.AddChild(EstimatePermissions.MultimediaSystem.Create, L("Permission:MultimediaSystem.Create"));
            //multimediaSystemSetupPermission.AddChild(EstimatePermissions.MultimediaSystem.PopulateOutput, L("Permission:MultimediaSystem.PopulateOutput"));

            //var solarSystemSetupPermission = group.AddPermission(EstimatePermissions.SolarSystem.Default, L("Permission:SolarSystem"));
            //solarSystemSetupPermission.AddChild(EstimatePermissions.SolarSystem.Create, L("Permission:SolarSystem.Create"));
            //solarSystemSetupPermission.AddChild(EstimatePermissions.SolarSystem.PopulateOutput, L("Permission:SolarSystem.PopulateOutput"));

            //var vehicleRepairSetupPermission = group.AddPermission(EstimatePermissions.VehicleRepair.Default, L("Permission:VehicleRepair"));
            //vehicleRepairSetupPermission.AddChild(EstimatePermissions.VehicleRepair.Create, L("Permission:VehicleRepair.Create"));
            //vehicleRepairSetupPermission.AddChild(EstimatePermissions.VehicleRepair.PopulateOutput, L("Permission:VehicleRepair.PopulateOutput"));

            //var fireDetectionSetupPermission = group.AddPermission(EstimatePermissions.FireDetection.Default, L("Permission:FireDetection"));
            //fireDetectionSetupPermission.AddChild(EstimatePermissions.FireDetection.Create, L("Permission:FireDetection.Create"));
            //fireDetectionSetupPermission.AddChild(EstimatePermissions.FireDetection.PopulateOutput, L("Permission:FireDetection.PopulateOutput"));

            //var surveillanceSetupPermission = group.AddPermission(EstimatePermissions.Surveillance.Default, L("Permission:Surveillance"));
            //surveillanceSetupPermission.AddChild(EstimatePermissions.Surveillance.Create, L("Permission:Surveillance.Create"));
            //surveillanceSetupPermission.AddChild(EstimatePermissions.Surveillance.PopulateOutput, L("Permission:Surveillance.PopulateOutput"));

            //var soundSystemSetupPermission = group.AddPermission(EstimatePermissions.SoundSystem.Default, L("Permission:SoundSystem"));
            //soundSystemSetupPermission.AddChild(EstimatePermissions.SoundSystem.Create, L("Permission:SoundSystem.Create"));
            //soundSystemSetupPermission.AddChild(EstimatePermissions.SoundSystem.PopulateOutput, L("Permission:SoundSystem.PopulateOutput"));

            //var internetSystemSetupPermission = group.AddPermission(EstimatePermissions.InternetSystem.Default, L("Permission:InternetSystem"));
            //internetSystemSetupPermission.AddChild(EstimatePermissions.InternetSystem.Create, L("Permission:InternetSystem.Create"));
            //internetSystemSetupPermission.AddChild(EstimatePermissions.InternetSystem.PopulateOutput, L("Permission:InternetSystem.PopulateOutput"));

            //var electricalItemsSetupPermission = group.AddPermission(EstimatePermissions.ElectricalItems.Default, L("Permission:ElectricalItems"));
            //electricalItemsSetupPermission.AddChild(EstimatePermissions.ElectricalItems.Create, L("Permission:ElectricalItems.Create"));
            //electricalItemsSetupPermission.AddChild(EstimatePermissions.ElectricalItems.PopulateOutput, L("Permission:ElectricalItems.PopulateOutput"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
