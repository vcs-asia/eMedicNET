using eMedicEntityModel.Models.v1;
using eMedicNETv6.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace eMedicNETv6.Services
{
    public class LoadData : ILoadData
    {
        private readonly IConfiguration _configuration;
        public LoadData(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public SelectList GetPrnMrgstToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "MARITAL STATUS").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }

        }

        public SelectList GetPrnGendrToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "GENDER").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }

        }

        public SelectList GetPrnNtionToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "NATIONALITY").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }

        }

        public SelectList GetPrnIraceToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "RACE").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }
        }

        public SelectList GetPrnBgropToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "BLOOD GROUP").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }
        }

        public SelectList GetPrnPanelToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Panel").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Panel>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PnlAutid", "PnlSname");
            }
            else
            {
                return new SelectList(new List<Panel>(), "PnlAutid", "PnlSname");
            }

        }

        public SelectList GetPrnRelatToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "RELATION").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }
        }

        public SelectList GetPrnPtypeToSelectList()
        {
            var res = ApiExecute.GetAsync(_configuration.GetSection("Api:Uri").Value + "Parameter/GetByType/" + "PATIENT TYPE").GetAwaiter().GetResult();
            if (res != null && res.Flag == true)
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<Parameter>>(JsonConvert.SerializeObject(res.Data));
                return new SelectList(data, "PrmAutid", "PrmPdesc");
            }
            else
            {
                return new SelectList(new List<Parameter>(), "PrmAutid", "PrmPdesc");
            }
        }
    }
}
