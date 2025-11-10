using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class RankRepo : IRank
    {
        private readonly Context context;
        public RankRepo(Context context) 
        {
            this.context = context;

        }
        public async Task<List<RankDto>>GetAll()
        {
            var renk = await context.Ranks.ToListAsync();
            var rankdro=new List<RankDto>();
            foreach (var r in renk)
            {
                var ran=new RankDto();
                ran.name=r.name;
                ran.minVisits = r.minVisits;
                ran.discountPercentage = r.discountPercentage;
                ran.workShopId = r.workShopId;
                rankdro.Add(ran);
            }
            return rankdro;
        }
        public async Task<RankDto>GetbyId(int id)
        {
            var rank = await context.Ranks.FirstOrDefaultAsync(x => x.id == id);
            if (rank == null)
                throw new InvalidOperationException($"The Rank Id:{id} are not found");
            RankDto rankDto = new RankDto();
            rankDto.name= rank.name;
            rankDto.minVisits = rank.minVisits;
            rankDto.discountPercentage = rank.discountPercentage;
            rankDto.workShopId = rank.workShopId;
            return rankDto;
        }
        public async Task<bool>Creat(RankDto rankDto)
        {
            var rank = new Rank();
            rank.name= rankDto.name;
            rank.workShopId = rankDto.workShopId;
            rank.minVisits = rankDto.minVisits;
            rank.discountPercentage = rankDto.discountPercentage;
            context.Ranks.Add(rank);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool>Delet(int id)
        {
            var rank = await context.Ranks.FirstOrDefaultAsync(x => x.id == id);
            if (rank == null)
                throw new InvalidOperationException($"The Rank With Id:{id} Not found");
            context.Ranks.Remove(rank);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
