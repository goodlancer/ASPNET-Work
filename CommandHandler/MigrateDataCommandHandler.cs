using AutoMapper;

using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Services.Repository;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class MigrateDataCommandHandler : IRequestHandler<MigrateDataCommand, string>
    {
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;
        private IBaseRepository<AgentPro> _baseRepositoryPropertiesAgentPro;
        private IBaseRepository<Prycd> _baseRepositoryPropertiesPrycd;
        private IBaseRepository<Properties> _baseRepositoryProperties;
        private IMapper _mapper;
        private IBaseRepository<PhatchConfiguration> _baseRepositoryScolptioConfiguration;
        public MigrateDataCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport
            , IBaseRepository<AgentPro> baseRepositoryPropertiesAgentPro
            , IMapper mapper
            , IBaseRepository<Properties> baseRepositoryProperties
            , IBaseRepository<Prycd> baseRepositoryPropertiesPrycd
            , IBaseRepository<PhatchConfiguration> baseRepositoryScolptioConfiguration)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
            _baseRepositoryPropertiesAgentPro = baseRepositoryPropertiesAgentPro;
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
            _baseRepositoryPropertiesPrycd = baseRepositoryPropertiesPrycd;
            _baseRepositoryScolptioConfiguration = baseRepositoryScolptioConfiguration;
        }
        public async Task<string> Handle(MigrateDataCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            int successRecordCount = 0;
            int failedRecordCount = 0;
            int totalRecordCount = 0;
            int alreadyExistCount = 0;

            var propertyConfig = await _baseRepositoryScolptioConfiguration.GetSingleAsync(x => x.ConfigKey == $"{Const.PROPERTY_SEQUENCE_NUMBER}");
            if (propertyConfig == null)
            {
                propertyConfig = new PhatchConfiguration()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConfigKey = Const.PROPERTY_SEQUENCE_NUMBER,
                    ConfigValue = 1000000
                };
                await _baseRepositoryScolptioConfiguration.Create(propertyConfig);
            }

            if (propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
            {
                var agentProData = await _baseRepositoryPropertiesAgentPro.GetAllAsync(x => x.ImportFileId == request.FileId);
                int phatchNumber = Convert.ToInt32(propertyConfig.ConfigValue);

                if (agentProData.Count() > 0)
                {
                    propertyConfig.ConfigValue = (phatchNumber + agentProData.Count() + 100);
                    await _baseRepositoryScolptioConfiguration.UpdateAsync(propertyConfig);
                }

                foreach (AgentPro agentPro in agentProData)
                {
                    try
                    {
                        var properties = _mapper.Map<AgentPro, Properties>(agentPro);
                        properties.PhatchNumber = phatchNumber;

                        if (await IsPropertyNotAvaliable(properties.APN, request.OrgId))
                        {
                            await _baseRepositoryProperties.Create(properties);
                            successRecordCount++;
                        }
                        else
                        {
                            alreadyExistCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        failedRecordCount++;
                    }
                    totalRecordCount++;
                    phatchNumber++;
                }
            }
            else if (propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_PRYCD)
            {
                var prycdList = await _baseRepositoryPropertiesPrycd.GetAllAsync(x => x.ImportFileId == request.FileId);
                int phatchNumber = Convert.ToInt32(propertyConfig.ConfigValue);

                if (prycdList.Count() > 0)
                {
                    propertyConfig.ConfigValue = (phatchNumber + prycdList.Count() + 100);
                    await _baseRepositoryScolptioConfiguration.UpdateAsync(propertyConfig);
                }

                foreach (Prycd prycd in prycdList)
                {
                    try
                    {
                        var properties = _mapper.Map<Prycd, Properties>(prycd);
                        properties.PhatchNumber = phatchNumber;

                        await _baseRepositoryProperties.Create(properties);
                        if (await IsPropertyNotAvaliable(properties.APN, request.OrgId))
                        {
                            await _baseRepositoryProperties.Create(properties);
                            successRecordCount++;
                        }
                        else
                        {
                            alreadyExistCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        failedRecordCount++;
                    }
                    totalRecordCount++;
                    phatchNumber++;
                }
            }
            propertiesFileImport.Status = "File Migrated";
            propertiesFileImport.Message = $"Total Records: {totalRecordCount} | Successfully migrated: {successRecordCount} | Existing properties: {alreadyExistCount} | Failed to migrate: {failedRecordCount}";
            await _baseRepositoryPropertiesFileImport.UpdateAsync(propertiesFileImport);
            return propertiesFileImport.Message;
        }

        private async Task<bool> IsPropertyNotAvaliable(string apn, string orgId)
        {
            var property = await _baseRepositoryProperties.GetSingleAsync(it => it.APN == apn && it.OrgId == orgId);
            return property == null;
        }
    }
}
