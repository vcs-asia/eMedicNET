using eMedicEntityModel.Models.v1;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eMedicNETv7.Services
{
    public interface ILoadData
    {
        public SelectList GetPrnMrgstToSelectList();
        public SelectList GetPrnGendrToSelectList();
        public SelectList GetPrnNtionToSelectList();
        public SelectList GetPrnIraceToSelectList();
        public SelectList GetPrnBgropToSelectList();
        public SelectList GetPrnPanelToSelectList();
        public SelectList GetPrnRelatToSelectList();
        public SelectList GetPrnPtypeToSelectList();
    }
}
