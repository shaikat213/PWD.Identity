using PWD.Identity.DtoModels;
using PWD.Identity.Interfaces;
using PWD.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace PWD.Identity
{
    public class OrganizationUnitAppService : ApplicationService, IOrganizationUnitAppService
    {
        private readonly IRepository<IdentityUser, Guid> _idRepository;
        private readonly IRepository<Posting, Guid> _postingRepository;
        private readonly IRepository<OrganizationUnit, Guid> _repository;
        private readonly IRepository<IdentityRole, Guid> _rolerepository;
        private readonly IdentityUserManager _userManager;

        public OrganizationUnitAppService(
            IRepository<OrganizationUnit, Guid> repository,
             IRepository<IdentityUser, Guid> idRepository,
             IRepository<Posting, Guid> postingRepository,
        IRepository<IdentityRole, Guid> rolerepository,
        IdentityUserManager userManager)
        {
            _idRepository = idRepository;
            _postingRepository = postingRepository;
            _rolerepository = rolerepository;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<bool> UserToOffice(UserToOfficeDto input)
        {
            var office = _repository.FirstOrDefault(o => o.Code == input.OfficeName);
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null || office == null) return false;
            if (input.Action)
                await _userManager.AddToOrganizationUnitAsync(user, office);
            else
                await _userManager.RemoveFromOrganizationUnitAsync(user, office);
            return true;
        }


        [HttpGet]

        public List<IdentityRoleDto> GetRoles()
        {
            var roles = _rolerepository.GetListAsync().Result;

            return ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles);
        }


        [HttpGet]
        public List<ColleagueDto> OfficeUsers(string userName)
        {
            var user = _idRepository.Include(u => u.OrganizationUnits).FirstOrDefault(u => u.UserName == userName);
            var unitId = user.OrganizationUnits.FirstOrDefault().OrganizationUnitId;
            var colleagues = new List<IdentityUser>();

            colleagues = _idRepository.Include(u => u.OrganizationUnits)
                .Where(u => u.UserName != null && u.OrganizationUnits.Count > 0)
                .Where(u => u.OrganizationUnits.FirstOrDefault().OrganizationUnitId == unitId)
                .ToList();

            return ObjectMapper.Map<List<IdentityUser>, List<ColleagueDto>>(colleagues);
        }

        [HttpGet]
        public List<PostingDto> OfficePosts(string userName)
        {
            var user = _idRepository.Include(u => u.OrganizationUnits).FirstOrDefault(u => u.UserName == userName);
            var unitId = user.OrganizationUnits.FirstOrDefault().OrganizationUnitId;
            var colleagues = new List<IdentityUser>();
            var postingIds = new List<PostingDto>();

            colleagues = _idRepository.Include(u => u.OrganizationUnits)
                .Where(u => u.UserName != null && u.OrganizationUnits.Count > 0)
                .Where(u => u.OrganizationUnits.FirstOrDefault().OrganizationUnitId == unitId)
                .ToList();

            colleagues.ForEach(x =>
            {
                x.ExtraProperties.TryGetValue("PostingId", out var postingId);
                if (postingId != null)
                    postingIds.Add(new PostingDto() { Id = x.Id, PostingId = Convert.ToInt32(postingId) });
            });

            var postings = _postingRepository.Where(p => postingIds.Select(x => x.PostingId).Contains(p.PostingId));
            var result = ObjectMapper.Map<IQueryable<Posting>, List<PostingDto>>(postings);
            var col = colleagues.FirstOrDefault();


            result.ForEach(r =>
            {
                r.OrgUniId = user.OrganizationUnits.FirstOrDefault().OrganizationUnitId;
                if (postingIds.Any(c => c.PostingId == r.PostingId))
                    r.Id = postingIds.FirstOrDefault(c => c.PostingId == r.PostingId).Id;
            });
            return result;
        }

        [HttpGet]
        public DateTime Latest()
        {
            var units = _repository.ToList();
            if (units.Max(u => u.LastModificationTime) >= units.Max(u => u.CreationTime))
                return (DateTime)units.Max(u => u.LastModificationTime);
            else
                return units.Max(u => u.CreationTime);
        }

        [HttpGet]
        public List<OrganizationUnitDto> Offices()
        {
            var units = _repository.Include(r => r.Roles).ToList();
            var result = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(units);
            result.ForEach(u =>
            {
                var unit = units.FirstOrDefault(x => x.Id == u.Id);
                if (unit != null)
                {
                    unit.ExtraProperties.TryGetValue("CivilEm", out var value);
                    if (value != null && value.GetType() == typeof(string)) { u.CivilEm = value.ToString(); }
                }
                var user = _idRepository.FirstOrDefault(i => i.UserName == u.Code);
                if (user != null) u.UserId = user.Id;
            });
            return result;
        }

        public List<string> MapUnit()
        {
            var ml = new List<string>() { "ace_psp:se_pc1", "ace_psp:se_pc2", "ace_psp:se_dc1", "ace_psp:se_dc2", "ace_psp:se_audit", "ace_psp:se_pecu", "ace_ppc:se_ppc", "ace_pnd:se_empd", "ace_em:se_emdk1", "ace_em:se_emdk2", "ace_em:se_emdk3", "ace_metro_zn:se_dhk1", "ace_metro_zn:se_dhk2", "ace_metro_zn:se_dhk3", "ace_dhk:se_dhk4", "ace_dhk:se_savar", "ace_mymen:se_mymen", "ace_ctg:se_comil", "ace_syl:se_syl", "ace_ctg:se_ctg1", "ace_ctg:se_ctg2", "ace_ctg:se_emctg", "ace_rngpr:se_rngpr", "ace_raj:se_bogra", "ace_raj:se_raj", "ace_khul:se_jeshr", "ace_khul:se_khul", "ace_bari:se_bari", ":se_emdk2", ":se_emdk3", "se_pc1:ee_pd1", "se_pc1:ee_pd2", "se_pc2:ee_pd3", "se_pc2:ee_pd4", "se_pc2:ee_pd5", "se_pc1:ee_survey", "se_dc1:ee_dd1", "se_dc1:ee_dd2", "se_dc1:ee_dd3", "se_dc2:ee_dd4", "se_dc2:ee_dd5", "se_dc2:ee_dd6", "se_audit:ee_monit", "se_audit:ee_audit", "se_pecu:ee_pecu", "se_ppc:ee_ppc", "se_empd:ee_empd1", "se_empd:ee_empd2", "se_emdk1:ee_emdk1", "se_emdk1:ee_emdk2", "se_emdk1:ee_emdk3", "se_emdk2:ee_emdk4", "se_emdk2:ee_emdk5", "se_emdk2:ee_emdk6", "se_emdk3:ee_emdk7", "se_emdk3:ee_emdk8", "se_emdk3:ee_wshop", "se_emdk3:ee_wood", "se_dhk1:ee_dhk1", "se_dhk1:ee_dhk2", "se_dhk1:ee_city", "se_dhk1:ee_arbor", "se_dhk2:ee_eden", "se_dhk2:ee_dhk3", "se_dhk2:ee_dhk4", "se_dhk2:ee_dmc", "se_dhk3:ee_sbn1", "se_dhk3:ee_sbn2", "se_dhk3:ee_sbn3", "se_dhk3:ee_mhkli", "se_dhk4:ee_mtjhl", "se_dhk4:ee_ajmpr", "se_dhk4:ee_nrsdi", "se_dhk4:ee_nganj", "se_dhk4:ee_munsi", "se_dhk4:ee_resrc", "se_savar:ee_manik", "se_savar:ee_savar", "se_savar:ee_mirpr", "se_savar:ee_gzipr", "se_mymen:ee_mymen", "se_mymen:ee_kishr", "se_mymen:ee_netro", "se_tangl:ee_tangl", "se_tangl:ee_jmlpr", "se_tangl:ee_shrpr", "se_comil:ee_comil", "se_comil:ee_chand", "se_comil:ee_bbari", "se_comil:ee_noakh", "se_comil:ee_feni", "se_comil:ee_laxmi", "se_syl:ee_syl", "se_syl:ee_sunam", "se_syl:ee_mlvbz", "se_syl:ee_hobi", "se_ctg1:ee_ctg1", "se_ctg1:ee_ctg2", "se_ranga:ee_khgra", "se_ranga:ee_ranga", "se_ctg2:ee_ctg3", "se_ctg2:ee_ctg4", "se_ranga:ee_bandr", "se_ctg2:ee_coxbz", "se_emctg:ee_emctgpd", "se_emctg:ee_emctg1", "se_emctg:ee_emctg2", "se_raj:ee_emrajpd", "se_rngpr:ee_rngpr", "se_rngpr:ee_kuri", "se_rngpr:ee_lmnht", "se_dinaj:ee_dinaj", "se_dinaj:ee_tkrga", "se_rngpr:ee_panch", "se_dinaj:ee_nilph", "se_rngpr:ee_gaiba", "se_bogra:ee_bogra", "se_bogra:ee_jprht", "se_pabna:ee_siraj", "se_raj:ee_raj1", "se_raj:ee_raj2", "se_pabna:ee_pabna", "se_raj:ee_naoga", "se_pabna:ee_nator", "se_jeshr:ee_jessr", "se_jeshr:ee_nrail", "se_jeshr:ee_magur", "se_jeshr:ee_khsht", "se_jeshr:ee_meher", "se_jeshr:ee_chuad", "se_gopal:ee_farid", "se_gopal:ee_rjbri", "se_jeshr:ee_jhnai", "se_khul:ee_khul1", "se_khul:ee_khul2", "se_khul:ee_satkh", "se_khul:ee_bgrht", "se_bari:ee_peroj", "se_gopal:ee_mdrpr", "se_gopal:ee_gopal", "se_gopal:ee_shtpr", "se_bari:ee_bari", "se_bari:ee_jhlkt", "se_bari:ee_bhola", "se_bari:ee_patua", "se_bari:ee_brgna", "se_khul:ee_emkhulpd", "se_khul:ee_emkhulpd", "se_raj:ee_chpai", "ee_survey:sde1_survey", "ee_pecu:sde_pecu", "ee_emdk1:sde1_emdk1", "ee_emdk1:sde2_emdk1", "ee_emdk11:sde_emdk1_bongovobon", "ee_emdk2:sde3_emdk2", "ee_emdk2:sde4_emdk2", "ee_emdk3:sde5_emdk3", "ee_emdk3:sde6_emdk3", "ee_emdk4:sde7_emdk4", "ee_emdk4:sde8_emdk4", "ee_emdk5:sde9_emdk5", "ee_emdk5:sde10_emdk5", "ee_emdk6:sde11_emdk6", "ee_emdk6:sde12_emdk6", "ee_emdk7:sde13_emdk7", "ee_emdk7:sde14_emdk7", "ee_emdk8:sde15_emdk8", "ee_emdk8:sde16_emdk8", "ee_wshop:sde1_em_wshop", "ee_wshop:sde2_em_wshop", "ee_wood:sde_em_wood", "ee_wshop:", "ee_dhk1:sde1_dhk1", "ee_dhk1:sde1_rajarbag", "ee_dhk1:sde2_rajarbag", "ee_dhk2:sde2_dhk2", "ee_dhk2:sde3_dhk3", "ee_dhk2:sde_dhanmondi", "ee_city:sde_ramna1_city", "ee_city:sde_ramna2_city", "ee_arbor:sde1_arbor", "ee_arbor:sde2_arbor", "ee_arbor:sdo_sbn_arbor", "ee_eden:sde1_eden", "ee_eden:sde2_eden", "ee_dhk3:sde4_dhk3", "ee_dhk3:sde_food_dhk3", "ee_dhk3:sde_tejgaon_dhk3", "ee_dhk4:sde5_dhk4", "ee_dhk4:sde6_dhk4", "ee_dhk4:sde_rail_dhk4", "ee_dmc:sde_dmc", "ee_dmc:sde_ssmc", "ee_sbn1:sde1_sbn1", "ee_sbn1:sde2_sbn1", "ee_sbn1:sde_sbn_medical", "ee_sbn2:sde3_sbn2", "ee_sbn2:sde4_sbn2", "ee_sbn2:sde5_sbn2", "ee_sbn3:sde6_sbn3", "ee_sbn3:sde7_sbn3", "ee_sbn3:sde8_sbn3", "ee_mhkli:sde_mhkli", "ee_mhkli:sde_uttara", "ee_mtjhl:sde7_mtjhl", "ee_mtjhl:sde1_mtjhl", "ee_mtjhl:sde2_mtjhl", "ee_ajmpr:sde1_ajmpr", "ee_ajmpr:sde2_ajmpr", "ee_ajmpr:sde_pilkhana", "ee_nrsdi:sde1_nrsdi", "ee_nrsdi:sde2_nrsdi", "ee_nganj:sde1_nganj", "ee_nganj:sde2_nganj", "ee_munsi:ae_munsi", "ee_emdk11:sde_em_munsi", "ee_resrc:sde_resrc", "ee_manik:sde1_manik", "ee_manik:sde2_manik", "ee_savar:sde1_savar", "ee_savar:sde2_savar", "ee_mirpr:sde1_mirpr", "ee_mirpr:sde2_mirpr", "ee_mirpr:sde3_mirpr", "ee_gzipr:sde1_gzipr", "ee_gzipr:sde2_gzipr", "ee_gzipr:sde3_gzipr", "ee_mymen:sde1_mymen", "ee_mymen:sde2_mymen", "ee_mymen:sde_em_mymen", "ee_kishr:sde1_kishr", "ee_kishr:sde2_kishr", "ee_kishr:sde_em_kishr", "ee_netro:sde1_netro", "ee_netro:sde2_netro", "ee_netro:sde_em_netro", "ee_tangl:sde_tangl", "ee_tangl:sde_gopalpur", "ee_tangl:sde_em_tangl", "ee_jmlpr:sde_jmlpr", "ee_jmlpr:sde_em_jmlpr", "ee_shrpr:sde1_shrpr", "ee_shrpr:sde_em_shrpr", "ee_comil:sde1_comil", "ee_comil:sde2_comil", "ee_comil:sde_em_comil", "ee_chand:sde_chand", "ee_chand:sde_em_chand", "ee_bbari:sde1_bbari", "ee_bbari:sde2_bbari", "ee_bbari:sde_em_bbari", "ee_noakh:sde1_nkhali", "ee_noakh:sde_em_nkhali", "ee_feni:sde_feni", "ee_feni:sde_em_feni", "ee_laxmi:sde_laxmi", "ee_laxmi:sde_em_laxmi", "ee_syl:sde1_syl", "ee_syl:sde2_syl", "ee_syl:sde3_syl", "ee_syl:sde_em_syl", "ee_sunam:sde1_sunam", "ee_sunam:sde2_sunam", "ee_sunam:sde_em_sunam", "ee_mlvbz:sde1_mlvbz", "ee_mlvbz:sde2_mlvbz", "ee_mlvbz:sde_em_mlvbz", "ee_hobi:sde1_hobi", "ee_hobi:sde2_hobi", "ee_hobi:sde_em_hobi", "ee_ctg1:sde1_ctg1", "ee_ctg1:sde2_ctg1", "ee_ctg2:sde3_ctg2", "ee_ctg2:sde4_ctg2", "ee_ctg1:sde_patiya", "ee_ctg2:sde_hathazari", "ee_khgra:ae_khgra", "ee_khgra:sde2_khgra", "ee_khgra:sde_em_khgra", "ee_ranga:sde_ranga", "ee_ranga:sde_em_ranga", "ee_ranga:sde_kaptai", "ee_ctg3:sde5_ctg3", "ee_ctg3:sde6_ctg3", "ee_ctg4:sde8_ctg4", "ee_ctg4:sde9_ctg4", "ee_ctg4:sde_resource_ctg4", "ee_ctg4:sde_arbor_ctg4", "ee_bandr:sde_bandr", "ee_bandr:sde_lama", "ee_bandr:sde_em_bandr", "ee_coxbz:sde1_coxbz", "ee_coxbz:sde2_coxbz", "ee_coxbz:sde_em_coxbz", "ee_emctg1:sde1_emcg1", "ee_emctg1:sde_wshop_emcg1", "ee_emctg2:sde2_emcg2", "ee_emctg2:sde3_emcg2", "ee_rngpr:sde1_rngpr", "ee_rngpr:sde2_rngpr", "ee_rngpr:sde_maint_rngpr", "ee_rngpr:sde_em_rngpr", "ee_kuri:sde1_kuri", "ee_kuri:sde2_kuri", "ee_kuri:sde_em_kuri", "ee_lmnht:sde_panch", "ee_lmnht:sde2_lmnht", "ee_lmnht:sde_em_lmnht", "ee_dinaj:sde1_dinaj", "ee_dinaj:sde2_dinaj", "ee_dinaj:sde_fulbari_dinaj", "ee_dinaj:sde_em_dinaj", "ee_tkrga:sde1_tkrga", "ee_tkrga:sde2_tkrga", "ee_tkrga:sde_em_tkrga", "ee_panch:sde_panch", "ee_panch:sde_em_panch", "ee_nilph:sde_nilph", "ee_nilph:sde_em_nilph", "ee_gaiba:sde_gaiba", "ee_gaiba:sde_em_gaiba", "ee_bogra:sde1_bogra", "ee_bogra:sde2_bogra", "ee_bogra:sde_em_bogra", "ee_bogra:sde3_bogra", "ee_jprht:sde_jprht", "ee_jprht:sde_em_jprht", "ee_siraj:sde_siraj", "ee_siraj:sde_ullapara_siraj", "ee_siraj:sde_em_siraj", "ee_raj1:sde_raj1", "ee_raj2:sde1_raj2", "ee_raj1:sde_maint_raj1", "ee_raj1:sde_saroda_raj1", "ee_raj1:sde_em_raj1", "ee_raj2:sde_em_raj2", "ee_chpai:sde_chpai", "ee_pabna:sde1_pabna", "ee_pabna:sde2_pabna", "ee_pabna:sde_em_pabna", "ee_naoga:sde_em_naoga", "ee_naoga:sde1_naoga", "ee_nator:sde_nator", "ee_jessr:sde1_jessr", "ee_jessr:sde2_jessr", "ee_jessr:sde_em_jessr", "ee_nrail:sde1_nrail", "ee_nrail:sde_em_nrail", "ee_magur:sde1_magur", "ee_magur:sde_em_magur", "ee_khsht:sde_khsht", "ee_khsht:sde_em_khsht", "ee_meher:sde1_meher", "ee_meher:sde_em_meher", "ee_chuad:sde1_chuad", "ee_chuad:sde_em_chuad", "ee_farid:sde1_farid", "ee_farid:sde_em_farid", "ee_rjbri:sde_rjbri", "ee_rjbri:sde_em_rjbri", "ee_jhnai:sde1_jhnai", "ee_jhnai:sde_em_jhnai", "ee_khul1:sde1_khul1", "ee_khul1:sde2_khul1", "ee_khul2:sde2_khul2", "ee_khul1:sde_em_khul1", "ee_satkh:sde1_satkh", "ee_satkh:sde2_satkh", "ee_satkh:sde_em_satkh", "ee_bgrht:sde1_bgrht", "ee_bgrht:sde2_bgrht", "ee_bgrht:sde_em_bgrht", "ee_khul2:sde_em_khul2", "ee_khul2:sde_maint_khul2", "ee_peroj:sde_peroj", "ee_peroj:sde_em_peroj", "ee_mdrpr:sde_mdrpr", "ee_mdrpr:sde_em_mdrpr", "ee_gopal:sde_gopal", "ee_gopal:sde_em_gopal", "ee_shtpr:sde_shtpr", "ee_shtpr:sde_em_shtpr", "ee_bari:sde__bari", "ee_bari:sde_gnadi_bari", "ee_bari:sde_medical_bari", "ee_bari:sde_em_bari", "ee_jhlkt:sde_jhlkt", "ee_jhlkt:sde_em_jhlkt", "ee_bhola:sde1_bhola", "ee_bhola:sde2_bhola", "ee_bhola:sde_em_bhola", "ee_patua:sde1_patua", "ee_patua:sde2_patua", "ee_patua:sde_em_patua", "ee_brgna:sde_brgna", "ee_brgna:sde_em_brgna", "ee_chpai:sde_em_chpai", "ee_survey:sde2_survey", "ee_nator:sde_em_nator", "ace_est:se_est", "ace_est:se_coord", "ace_est:se_dev", "ace_est:se_maint", "se_est:ee_mis", "ee_noakh:sde2_nkhali", "ee_farid:sde2_farid", "ee_gopal:sde_arbor_gopal", "se_maint:ee_maint", "se_maint:ee_mnctg", "ee_maint:sde1_maint_dhk", "ee_maint:sde2_maint_dhk", "ee_maint:sde3_maint_dhk", "ee_mnctg:sde1_maint_ctg", "ee_mnctg:sde2_maint_ctg", "ee_mnctg:sde3_maint_ctg", "se_est:ee_om", "se_est:ee_enq", "se_est:ee_est", "ace_est:se_pwdta", "ee_emdk10:sde_em_gzipr", "ee_emdk10:sde_em_manik", "ee_em_uttara:ee_emup", "se_dc2:ee_sdu", "se_coord:ee_coord", "ee_emdk9:sde_em_nganj", "ee_emdk9:sde_em_nrsdi", "ee_dhk4:sde7_dhk4", "se_dc1:ee_sdu2", "ace_mymen:se_tangl", "ace_gopal:se_gopal", "ace_raj:se_pabna", "ace_ctg:se_ranga", "ace_pnd:se_emdk4", "ace_rngpr:se_dinaj", "ace_pnd:se_empln", "ace_pnd:se_emdc", "ace_pnd:se_mis", "se_empln:ee_empln1", "se_empln:ee_empln2", "se_empln:ee_empln3", "se_emdc:ee_emdd1", "se_emdc:ee_emdd2", "se_emdc:ee_emdd3", "se_mis:ee_mis1", "se_mis:ee_mis2", "se_mis:ee_mis3", "se_emdk4:ee_emdk9", "se_emdk4:ee_emdk10", "se_emdk4:ee_emdk11", "ace_gopal:ee_emgopalpd", "ee_emgopalpd:", "se_empd:ee_empd3", "se_est:branch1", "se_est:branch2", "se_est:branch3", "se_est:branch4", "se_est:account2", "se_est:law", "se_est:lenden", "se_coord:account1", "se_coord:account3", "se_coord:progati", "se_coord:resource", "se_dev:unnoyan1", "se_dev:unnoyan4", "se_dev:unnoyan2 ", "ace_mymen:ee_emmymenpd", "se_dev:ee_dev", "ee_em_uttara:ee_civil2_uttara", "ee_em_uttara:ee_civil1_uttara", };
            var ou = _repository.Where(o => o.IsDeleted == false).ToList();
            var res = new List<string>();
            ml.ForEach(m =>
            {
                var p = m.Split(":").First();
                var c = m.Split(":").Last();
                if (ou.Any(o => o.Code == c))
                {
                    var cu = ou.FirstOrDefault(o => o.Code == c);
                    var pu = ou.FirstOrDefault(o => o.Code == p);
                    if (pu != null)
                        res.Add(cu?.Code + ":" + pu?.Id);
                    //Console.WriteLine(cu?.Code+"\t"+pu?.Id);
                }

            });

            return res;
        }

        //public async Task<bool> test()
        //{
        //    var idList = "";
        //    List<string> list = new List<string>(idList.Split(","));
        //    list = list.Where(l => l.Length > 3).ToList();
        //    foreach (var l in list)
        //    {
        //        var id = Guid.NewGuid();
        //        var values = l.Split('*');
        //        //var userInfo = values[0];
        //        var userName = l;// userInfo.Split("@")[0];
        //        //var officeName = values[1].Split("@")[0];

        //        var user = new IdentityUser(id, userName, (userName + "@pwd.gov.bd"));
        //        var pass = "1" + char.ToUpper(userName[0]) + userName.Substring(1);
        //        var result = await _userManager.CreateAsync(user, pass);
        //        if (result.Succeeded)
        //        {
        //            //var ou = _repository.FirstOrDefault(i => i.Code == officeName);
        //            //if (ou != null)
        //            //{
        //            //    await _userManager.AddToOrganizationUnitAsync(id, ou.Id);
        //            //    await _userManager.AddToRoleAsync(user, "EstimateUser");
        //            //}
        //            await _userManager.AddToRoleAsync(user, "EstimateUser");
        //            await _userManager.AddToRoleAsync(user, "PrepStructure");
        //            await _userManager.AddToRoleAsync(user, "PrepFinish");
        //            await _userManager.AddToRoleAsync(user, "PrepPlumbing");
        //            //await _userManager.AddToRoleAsync(user, "SuppPlumbing");
        //            await _userManager.AddToRoleAsync(user, "PrepCE");

        //        }
        //        else
        //        {
        //            Console.WriteLine(l);
        //            Console.WriteLine(result.Errors);
        //        }
        //        //return result.Succeeded;
        //    };
        //    return true;
        //}

        //public async Task<bool> delu()
        //{
        //    var user = await _userManager.FindByEmailAsync("test@abc.com");
        //    var result = await _userManager.DeleteAsync(user);
        //    return result.Succeeded;
        //}
        public void pnd()
        {
            var ids = "ee_empd3,ee_empd2,ee_emrajpd,ee_emgopalpd,ee_empd1,ace_pnd,se_empd,ee_emctgpd,ee_emkhulpd,ee_emmymenpd".Split(',');
            var pnds = _repository.Where(r => ids.Contains(r.Code));
            pnds.ToList().ForEach(p =>
            {
                p.ExtraProperties.Add("isPnD", true);
            });


        }


    }
}
