using Commands.Query;
using Domains.DBModels;
using MediatR;
using MongoDB.Driver;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetCountByDate : IRequestHandler<GetCountByDateQuery, decimal>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IBaseRepository<Income> _baseIncomeRepositoryProperties;
        private readonly IBaseRepository<Expenditure> _baseIExpendRepositoryProperties;

        private readonly IMongoScolptioDBContext _mongoContext;
        private readonly IMongoCollection<Income> _dbCollection;
        private readonly IMongoCollection<Expenditure> _dbExpenseCollection;
        private readonly IMongoCollection<Properties> _dbPropertiesCollection;
        private readonly IMongoCollection<Campaign> _dbCampaginCollection;

        public GetCountByDate(
          IBaseRepository<Properties> baseRepositoryProperties,
          IBaseRepository<Income> baseIncomeRepositoryProperties,
          IBaseRepository<Expenditure> baseIExpendRepositoryProperties,
          IMongoScolptioDBContext context
        )
        {
            _baseRepositoryProperties = baseRepositoryProperties;
            _baseIncomeRepositoryProperties = baseIncomeRepositoryProperties;
            _baseIExpendRepositoryProperties = baseIExpendRepositoryProperties;
           

            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<Income>($"{typeof(Income).Name}");
            _dbExpenseCollection = _mongoContext.GetCollection<Expenditure>($"{typeof(Expenditure).Name}");
            _dbPropertiesCollection = _mongoContext.GetCollection<Properties>($"{typeof(Properties).Name}");
            _dbCampaginCollection = _mongoContext.GetCollection<Campaign>($"{typeof(Campaign).Name}");
        }

        public Task<decimal> Handle(GetCountByDateQuery request, CancellationToken cancellationToken)
        {
            decimal count = 0;
            switch (request.SectionName)
            {
                case "PropertyLeads":
                    var query = _dbPropertiesCollection.AsQueryable().Where(m=>m.OrgId == request.OrganizationId);
                    if(request.StartDate!=null && request.EndDate != null)
                    {
                        query = (MongoDB.Driver.Linq.IMongoQueryable<Properties>)query.Where(f => f.ImportedTime >= ((DateTime?)request.StartDate).Value.Date && f.ImportedTime < ((DateTime?)request.EndDate).Value.Date).AsQueryable();
                    }
                    count = query.Count();
                    break;
                case "Income":
                    var incomeQuery = _dbCollection.AsQueryable().Where(m => m.OrgId == request.OrganizationId);
                    if (request.StartDate != null && request.EndDate != null)
                    {
                        incomeQuery = (MongoDB.Driver.Linq.IMongoQueryable<Income>)incomeQuery.Where(f => f.CreatedDate >= ((DateTime?)request.StartDate).Value.Date && f.CreatedDate < ((DateTime?)request.EndDate).Value.Date).AsQueryable();
                    }
                    count = incomeQuery.Select(m => m.Amount).Sum();
                    break;
                case "Expense":
                    var expenseQuery = _dbExpenseCollection.AsQueryable().Where(m => m.OrgId == request.OrganizationId);
                    if (request.StartDate != null && request.EndDate != null)
                    {
                        expenseQuery = (MongoDB.Driver.Linq.IMongoQueryable<Expenditure>)expenseQuery.Where(f => f.CreatedDate >= ((DateTime?)request.StartDate).Value.Date && f.CreatedDate < ((DateTime?)request.EndDate).Value.Date).AsQueryable();
                    }
                    count = expenseQuery.Select(m => m.Amount).Sum();
                    break;
                case "total-campagin":
                    var totalCampaginQuery = _dbCampaginCollection.AsQueryable().Where(m => m.OrganizationId == request.OrganizationId);
                    if (request.StartDate != null && request.EndDate != null)
                    {
                        totalCampaginQuery = (MongoDB.Driver.Linq.IMongoQueryable<Campaign>)totalCampaginQuery.Where(f => f.AddedDate >= ((DateTime?)request.StartDate).Value.Date && f.AddedDate < ((DateTime?)request.EndDate).Value.Date).AsQueryable();
                    }
                    count = totalCampaginQuery.Count();
                    break;
                case "active-campagin":
                    var activeCampaignQuery = _dbCampaginCollection.AsQueryable().Where(m => m.Status == "Active" && m.OrganizationId == request.OrganizationId);
                    if (request.StartDate != null && request.EndDate != null)
                    {
                        activeCampaignQuery = (MongoDB.Driver.Linq.IMongoQueryable<Campaign>)activeCampaignQuery.Where(f =>  f.AddedDate >= ((DateTime?)request.StartDate).Value.Date && f.AddedDate < ((DateTime?)request.EndDate).Value.Date).AsQueryable();
                    }
                    count = activeCampaignQuery.Count();
                    break;
                case "completed-campagin":
                    var completedCampaignQuery = _dbCampaginCollection.AsQueryable().Where(m => m.Status == "Completed" && m.OrganizationId == request.OrganizationId);
                    if (request.StartDate != null && request.EndDate != null)
                    {
                        completedCampaignQuery = (MongoDB.Driver.Linq.IMongoQueryable<Campaign>)completedCampaignQuery.Where(f => f.AddedDate >= ((DateTime?)request.StartDate).Value.Date && f.AddedDate < ((DateTime?)request.EndDate).Value.Date).AsQueryable();
                    }
                    count = completedCampaignQuery.Count();
                    break;
            }

            return Task.FromResult(count);
        }
    }
}
