using Microsoft.AspNetCore.Mvc;
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
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;

namespace PWD.Identity
{
    public static class PermissionHelper
    {
        public static readonly string _authority = "https://auth.mis1pwd.com";
        //public static readonly string _authority = "https://localhost:44380";
        public static readonly string _authorityDev = "https://localhost:44380";
        public static readonly string _identityClientUrl = "http://idapi.mis1pwd.com";
        //public static readonly string _identityClientUrl = "https://localhost:44392";
        public static readonly string _identityClientUrlDev = "https://localhost:44392";
        public static readonly string _identityApiName = "/api/app/permission-map";
        public static readonly string _permissionGroupKey = "permissionGroupKey";
        public static readonly string _permissionGroupValue = "PWDEstimate";
        public static readonly string _providerName = "providerName";
        public static readonly string _providerValue = "R";
        public static readonly string _providerKey = "providerKeys";
        public static readonly string _clientId = "Schedule_App";
        public static readonly string _clientSecret = "1q2w3e*";
        public static readonly string _scope = "Schedule";
    }
    public class PostingAppService : ApplicationService, IPostingAppService
    {
        private readonly IRepository<Posting, Guid> _repository;
        private readonly IRepository<IdentityUser, Guid> _idRepository;
        private readonly IdentityUserManager _userManager;

        public PostingAppService(
            IRepository<Posting, Guid> repository,
            IdentityUserManager userManager,
             IRepository<IdentityUser, Guid> idRepository)
        {
            _repository = repository;
            _idRepository = idRepository;
            _userManager = userManager;

        }

        string clientUrl = PermissionHelper._identityClientUrl;
        string authUrl = PermissionHelper._authority;

        private async Task<TokenResponse> GetToken()
        {
            var authorityUrl = $"{PermissionHelper._authority}";

            var authority = new HttpClient();
            var discoveryDocument = await authority.GetDiscoveryDocumentAsync(authorityUrl);
            if (discoveryDocument.IsError)
            {
                //return null;
            }

            // Request Token
            var tokenResponse = await authority.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = PermissionHelper._clientId,
                ClientSecret = PermissionHelper._clientSecret,
                Scope = PermissionHelper._scope
            });

            if (tokenResponse.IsError)
            {
                //return null;
            }
            return tokenResponse;
        }

        public async Task AddPostingToUser(string userName, int postingId)
        {
            var user = await _idRepository.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null)
            {
                if (user.ExtraProperties.ContainsKey("PostingId"))
                {
                    user.ExtraProperties["PostingId"] = postingId;
                }
                else
                    user.ExtraProperties.Add("PostingId", postingId);
                var result = await _idRepository.UpdateAsync(user);
            }
        }



        //public async Task AddPosting()
        //{
        //    var uers = _idRepository.GetListAsync().Result;
        //    var users = await _idRepository.GetListAsync();

        //    users.ForEach(async x =>
        //    {
        //        x.ExtraProperties.TryGetValue("PostingId", out var postingId);
        //        if (postingId != null && (int)postingId > 0) { };
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("http://hrapi.mis1pwd.com/");
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            //GET Method
        //            HttpResponseMessage response = await client.GetAsync($"api/hr/posting/{postingId}/posting-info");
        //            if (response.IsSuccessStatusCode)
        //            {

        //                var department = await response.Content.ReadAsStringAsync();
        //                var post = JsonSerializer.Deserialize<PostingConsumeDto>(department);
        //                if (!await _repository.AnyAsync(x => x.PostingId == (int)postingId))
        //                {
        //                    var p = new Posting()
        //                    {
        //                        PostingId = post.id,
        //                        Post = post.post,
        //                        Name = post.name,
        //                        NameBn = post.nameBn,
        //                        Office = post.office,
        //                        OfficeBn = post.officeBn,
        //                        Designation = post.designation,
        //                        DesignationBn = post.designationBn,
        //                        EmployeeId = post.employeeId,
        //                    };
        //                    var result = await _repository.InsertAsync(p);
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("Internal server Error");
        //            }
        //        }
        //    });


        //}

        public async Task SyncPosting()
        {
            //var postingId = 5;
            for (int i = 1; i < 8; i++)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://hrapi.mis1pwd.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.GetAsync($"api/app/posting/{i}/active-postings");
                    if (response.IsSuccessStatusCode)
                    {

                        var department = await response.Content.ReadAsStringAsync();
                        var posts = JsonSerializer.Deserialize<List<PostingConsumeDto>>(department);
                        posts.ForEach(async post =>
                        {
                            var p = new Posting()
                            {
                                PostingId = post.id,
                                Post = post.post,
                                Name = post.name,
                                NameBn = post.nameBn,
                                Office = post.office,
                                OfficeBn = post.officeBn,
                                Designation = post.designation,
                                DesignationBn = post.designationBn,
                                EmployeeId = post.employeeId,
                            };
                            if (!_repository.Any(r => r.PostingId == p.PostingId))
                            {
                                var result = await _repository.InsertAsync(p);
                            }

                        });

                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }
                }
            }
        }

        public async Task<employeeDto> GetEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await GetToken();
                client.SetBearerToken(tokenResponse.AccessToken);
                client.BaseAddress = new Uri("http://hrapi.mis1pwd.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync($"api/app/employee/{id}");
                if (response.IsSuccessStatusCode)
                {

                    var department = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<employeeDto>(department);

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            return new employeeDto();
        }
        public async Task AttachToUser()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://hrapi.mis1pwd.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync($"api/app/posting/user-post-email");
                if (response.IsSuccessStatusCode)
                {

                    var userString = await response.Content.ReadAsStringAsync();
                    var users = userString.Split('\u002C').ToList();
                    users.ForEach(u =>
                    {
                        var un = u.Split(":").Last();
                        var uid = u.Split(":").First();
                        if (_idRepository.Any(u => u.UserName == un))
                        {
                            var id = _idRepository.FirstOrDefault(u => u.UserName == un);
                            if (id.ExtraProperties.ContainsKey("PostingId"))
                                id.ExtraProperties.Remove("PostingId");
                            id.ExtraProperties.Add("PostingId", uid);
                        }
                    });
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }


            //var users = new List<string>() { "12336:sde6_sbn3", "12337:sde1_eden", "12342:ee_sbn1", "16070:ee_siraj", "16072:ee_chpai", "16075:ee_dhk1", "16077:ee_netro", "16088:sde_saroda_raj1", "16089:sde_dmc", "16094:sde3_ctg2", "16095:sde3_bogra", "16096:sde_rjbri", "16105:sde_peroj", "16108:sde2_coxbz", "16109:sde_uttara", "16119:ee_dhk2", "16122:sde15_emdk8", "16144:ace_dhk", "16168:ee_coord", "16171:sde_resource_ctg4", "16173:sde5_ctg3", "16176:sde6_ctg3", "16222:ee_emkhulpd", "16229:ee_naoga", "16232:ee_emdk6", "16245:sde_em_manik", "16247:sde_wshop_emcg1", "16258:sde2_emdk1", "16259:sde6_emdk3", "16269:sde_em_gzipr", "16272:sde2_em_wshop", "16273:sde_em_jhnai", "16274:sde11_emdk6", "16275:sde_em_bbari", "16276:sde_em_nrsdi", "16277:sde_em_panch", "16285:ee_sdu2", "16297:se_rngpr", "16320:ee_sbn2", "16322:ee_savar", "16323:ee_eden", "16328:progm_cc", "16359:se_maint", "16363:ee_bhola", "16365:ee_nrail", "16368:sde2_ajmpr", "16371:ee_meher", "16373:ee_dd5", "16385:ee_emrajpd", "16387:ee_emdk10", "16388:ee_empln2", "16406:sde1_patua", "16421:ee_est", "16423:ee_enq", "16442:sde_food_dhk3", "16448:sde_em_nganj", "16450:sde5_emdk3", "16451:sde_maint_rngpr", "16455:ee_khul2", "16475:sde_em_munsi", "16478:ee_farid", "16484:sde1_ctg1", "16487:sde8_emdk4", "16491:ee_mdrpr", "16496:sde_em_wood", "16531:ace_em", "16534:sde2_mtjhl", "16536:sde1_coxbz", "16538:sde2_nganj", "16544:se_emdk2", "16557:ee_emdk11", "16558:sde_pilkhana", "16559:sde_jmlpr", "16560:sde1_shrpr", "16561:sde1_dhk1", "16607:ace_pnd", "16611:sde2_nrsdi", "16623:ee_feni", "16632:ee_ctg1", "16634:ee_syl", "16636:ee_mtjhl", "16637:ee_dinaj", "16643:ee_empln1", "16644:ee_emdk4", "16645:sde1_maint_dhk", "16646:sde6_dhk4", "16647:sde1_bogra", "16648:sde1_rngpr", "16649:sde2_mirpr", "16650:sde_gopal", "16651:sde1_syl", "16653:sde1_farid", "16654:sde_chpai", "16655:sde1_jhnai", "16656:sde_em_khul1", "16658:sde7_emdk4", "16659:sde_em_gopal", "16663:ace_rngpr", "16667:sde2_mymen", "16669:ace_metro_zn", "16670:se_dc2", "16671:se_pwdta", "16672:se_dc1", "16677:se_emdk4", "16678:se_empln", "16696:ee_emdd2", "16699:sde14_emdk7", "16700:sde_em_raj1", "16705:ee_dd4", "16708:ee_dd3", "16717:se_dhk1", "16720:se_dhk4", "16736:sde2_syl", "16737:sde1_tkrga", "16738:sde1_dinaj", "16739:sde2_pabna", "16741:sde2_netro", "16742:ee_gaiba", "16743:ee_nilph", "16745:sde1_hobi", "16748:sde_em_chpai", "16750:ee_ranga", "16751:sde_em_jhlkt", "16753:sde1_kuri", "16759:sde1_raj2", "16761:sde_em_pabna", "16762:sde4_ctg2", "16764:sde_hathazari", "16765:sde_gnadi_bari", "16766:sde1_nrsdi", "16768:sde2_bgrht", "16788:ace_psp", "16790:ee_gzipr", "16813:ee_dhk3", "16825:ee_raj2", "16828:ee_shrpr", "16832:ee_khsht", "16833:ee_nganj", "16835:ee_ctg2", "16878:se_mis", "16879:sde7_sbn3", "16884:sde2_emcg2", "16885:sde10_emdk5", "16887:sde_em_comil", "16888:sde_emdk1_bongovobon", "16892:sde1_emdk1", "16894:sde1_em_wshop", "16896:sde4_emdk2", "16900:sde_em_jmlpr", "16901:ee_mis1", "16905:se_emdc", "16906:se_empd", "16910:se_pc1", "16911:progm_cc", "16914:se_dhk2", "16919:se_emctg", "16920:se_emdk1", "16924:se_ranga", "16932:ee_chand", "16933:ee_bgrht", "16934:ee_brgna", "16945:ae_munsi", "16946:sde1_rajarbag", "16947:sde1_satkh", "16948:sde_mhkli", "16952:sde2_jessr", "16953:sde1_ajmpr", "16956:sde2_ctg1", "16958:se_coord", "16960:se_khul", "16961:se_dinaj", "16963:se_bogra", "16970:ee_ctg4", "16971:sde1_survey", "16972:sde_siraj", "16973:sde1_maint_ctg", "16974:sde_resrc", "16975:sde9_ctg4", "16976:ee_jessr", "17055:sde_em_tangl", "17056:sde1_emcg1", "17061:sde_em_naoga", "17097:ee_dd1", "17102:se_syl", "17112:ee_audit", "17113:ee_pd1", "17114:ee_sunam", "17117:ee_bogra", "17127:ee_satkh", "17128:sde_dhanmondi", "17129:sde_maint_raj1", "17146:ee_city", "17149:sde16_emdk8", "17150:sde9_emdk5", "17151:sde_em_gaiba", "17152:sde_em_khsht", "17156:sde2_bogra", "17158:sde2_eden", "17159:sde_shtpr", "17160:sde2_sunam", "17163:sde2_farid", "17164:sde2_manik", "17165:sde1_nganj", "17178:sde_mdrpr", "17179:sde2_sbn1", "17181:se_dev", "17194:ee_mymen", "17195:ee_munsi", "17196:ee_pd4", "17197:ee_khul1", "17204:sde5_sbn2", "17206:ee_mhkli", "17207:ee_mirpr", "17214:ee_khgra", "17215:ee_pabna", "17218:ace_raj", "17219:ace_khul", "17221:ee_raj1", "17237:ee_jhlkt", "17239:ee_shtpr", "17240:ee_laxmi", "17241:ee_chuad", "17243:sde1_mirpr", "17245:sde2_maint_ctg", "17246:sde3_maint_ctg", "17248:ee_nrsdi", "17249:ee_sbn3", "17250:ee_dmc", "17251:ee_bbari", "17252:ee_peroj", "17253:ee_tangl", "17254:ee_resrc", "17256:ee_survey", "17257:sde_raj1", "17259:sde2_maint_dhk", "17260:se_comil", "17263:ace_ctg", "17266:ace_est", "17268:ace_syl", "17269:ace_gopal", "17271:sde5_dhk4", "17273:sde4_sbn2", "17274:sde1_naoga", "17275:sde1_manik", "17276:sde_rail_dhk4", "17278:sde_ssmc", "17279:sde_ullapara_siraj", "17280:sde1_sbn1", "17281:sde3_gzipr", "17282:sde1_comil", "17283:sde3_maint_dhk", "17284:sde1_savar", "17285:sde2_gzipr", "17287:sde_ramna1_city", "17291:sde_jhlkt", "17295:sde1_nkhali", "17390:ee_empln3", "17392:ee_wshop", "17394:ee_mis3", "17430:ee_emctgpd", "17434:ee_emdd3", "17437:sde_em_siraj", "17460:sde_em_bgrht", "17462:sde_em_kuri", "17463:sde_em_bogra", "17466:sde2_dinaj", "17484:sde_ramna2_city", "17485:sde3_sbn2", "17486:sde2_savar", "17488:sde1_mtjhl", "17503:ee_rngpr", "17514:ace_bari", "17522:ee_empd1", "17523:ee_emdk5", "17524:ee_hobi", "17525:ee_monit", "17526:ee_mlvbz", "17527:sde2_nkhali", "17589:sde_chand", "17591:sde3_syl", "17610:ee_rjbri", "17644:ee_gopal", "17645:ee_tkrga", "17646:ee_jprht", "17647:ee_lmnht", "17649:ee_nator", "17650:ee_coxbz", "17651:ee_mnctg", "17653:ee_ctg3", "17654:ee_bandr", "17688:se_raj", "17689:ee_jhnai", "17698:se_savar", "17699:se_ppc", "17700:se_ctg1", "4349:ee_pd2", "4725:ee_dd6", "4730:ee_om", "4789:ee_ppc", "4897:sde1_bhola", "4899:sde1_khul1", "5428:ee_sdu", "5458:ee_pd5", "5465:sde1_netro", "6441:sde_pecu", "6508:ee_pecu", "6512:se_pecu", "6546:sde3_mirpr", "6548:sde_bangabhaban_city", "6561:ee_ajmpr", "6562:ee_maint", "6584:se_audit", "6585:se_dhk3", "6615:sde_em_rngpr", "6624:sde12_emdk6", "6643:ee_dd2", "6661:sde8_ctg4", "6682:sde2_kishr", "6744:se_pc2", "6747:sde1_sunam", "6760:se_jeshr", "6763:sde_em_khgra", "6765:sde_em_raj2", "6809:se_mymen", "6815:se_pabna", "6826:se_est", "6852:ee_emdk3", "6861:ee_emdk1", "6863:ee_emdk2", "6866:ee_empd3", "6874:ee_emdk7", "6889:sde_em_mdrpr", "6893:sde_em_bari", "6900:sde_em_jessr", "6902:sde_em_nkhali", "6935:ee_noakh", "6939:ee_comil", "7010:sde__bari", "7125:sde13_emdk7", "7147:ee_emdk9", "7173:sde_em_mymen", "7175:sde2_survey", "7176:sde_tejgaon_dhk3", "7197:sde2_khul1", "7581:sde2_dhk2", "7600:se_emdk3", "7615:ace_ppc", "7622:ee_pd3", "7639:ace_mymen", "7686:sde_sbn_medical", "7689:ee_emctg1", "7707:sde2_rngpr", "7708:sde_panch", "7712:sde_khsht", "7744:ee_wood", "7748:sde1_gzipr", "7749:sde4_dhk3", "7754:sde_fulbari_dinaj", "7756:sde1_mymen", "7764:ee_empd2", "7788:sde2_khgra", "7789:sde_em_nator", "7796:sde_tangl", "7798:sde2_comil", "7799:sde1_magur", "7800:sde_nilph", "7809:sde3_dhk3", "7811:se_gopal", "7812:se_bari", "7813:se_ctg2", "7822:ee_patua", "7823:sde_em_khul2", "7824:sde1_mlvbz", "7830:se_tangl", "7836:sde_em_laxmi", "7841:sde3_emcg2", "7930:ee_emctg2", "7951:sde_em_farid", "7956:sde_em_jprht", "7969:ee_kishr", "7970:ee_panch", "7973:ee_dhk4", "8005:ee_manik", "8008:ee_jmlpr", "8217:ee_mis2", "8218:ee_emdd1", "8220:ee_emdk8", };



        }

        [HttpGet]
        public async Task<PostingDto> UserInfo(string userName)
        {
            var user = await _idRepository.Include(u => u.OrganizationUnits).FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null)
            {
                if (user.ExtraProperties.ContainsKey("PostingId"))
                {
                    user.ExtraProperties.TryGetValue("PostingId", out var postingId);
                    var id = Convert.ToInt32(postingId);
                    if (id > 0)
                    {
                        if (_repository.Any(r => r.PostingId == id))
                        {
                            var post = _repository.FirstOrDefault(r => r.PostingId == id);
                            var unitId = user.OrganizationUnits?.FirstOrDefault()?.OrganizationUnitId;
                            var result = ObjectMapper.Map<Posting, PostingDto>(post);
                            result.OrgUniId = unitId;
                            result.Id = user.Id;
                            result.UserName = userName;
                            return result;
                        }
                    }
                }
                else
                {
                    var dto = new PostingDto()
                    {
                        Id = user.Id,
                        OrgUniId = user.OrganizationUnits?.FirstOrDefault()?.OrganizationUnitId,

                    };
                    return dto;

                }
            }
            return null;
        }
        [AllowAnonymous]
        [HttpGet]
        public PostingDto PostingInfo(int id)
        {
            if (_repository.Any(r => r.PostingId == id))
            {

                var post = _repository.FirstOrDefault(r => r.PostingId == id);
                var result = ObjectMapper.Map<Posting, PostingDto>(post);

                var users = _idRepository.Include(u => u.OrganizationUnits).ToList();
                users.ForEach(user =>
                {
                    var postingId = user.ExtraProperties.GetValueOrDefault("PostingId");
                    //{"PostingId":"7811"}
                    if (post.PostingId.ToString() == postingId?.ToString())
                    {
                        var unitId = user.OrganizationUnits?.FirstOrDefault()?.OrganizationUnitId;
                        result.OrgUniId = unitId;
                        result.Id = user.Id;
                        result.UserName= user.UserName;
                    }
                });
                return result;
            }
            return null;
        }

        [AllowAnonymous]
        [HttpGet]
        public List<PostingDto> PostingList(int[] idList)
        {
            //var idList = ids.Split(',').Select(int.Parse).ToArray(); 
            var postings = _repository.Where(r => idList.Contains(r.PostingId)).ToList();
            var result = ObjectMapper.Map<List<Posting>, List<PostingDto>>(postings);
            var users = _idRepository.Include(u => u.OrganizationUnits).ToList();

            result.ForEach(r =>
            {
                users.ForEach(user =>
                {
                    var postingId = user.ExtraProperties.GetValueOrDefault("PostingId");
                    if (r.PostingId.ToString() == postingId?.ToString())
                    {
                        var unitId = user.OrganizationUnits?.FirstOrDefault()?.OrganizationUnitId;
                        r.OrgUniId = unitId;
                        r.Id = user.Id;
                    }
                });
            });
            return result;
        }

        [HttpGet]
        public async Task<IdentityUserDto> GetUser(string userId)
        {
            try
            {
                return ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _userManager.FindByIdAsync(userId));
            }
            catch (Exception)
            {
                throw new UserFriendlyException("User Id not found.");
            }

        }
        [HttpGet]
        public async Task<IdentityUserDto> GetUserByName(string userName)
        {
            try
            {
                return ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _userManager.FindByNameAsync(userName));
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Username not found.");
            }

        }

        [HttpPost]
        public async Task<bool> UpdatePassword(ChangePass input)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(input.UserName);
                if (user == null) {
                throw new UserFriendlyException("Username not found.");
                }
                var r = await _userManager.ChangePasswordAsync(user, input.OldPass,input.NewPass);
                if (!r.Succeeded)
                {
                    throw new Exception(r.Errors.FirstOrDefault().Description);
                }
                return r.Succeeded;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        

    }
}
