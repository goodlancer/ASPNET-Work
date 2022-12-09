
using Commands;

using Domains.DBModels;
using Domains.Dtos;

using Infrastructure;

using MediatR;

using Services.Repository;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class MapPropertiesColumnCommandHandler : IRequestHandler<MapPropertiesColumnCommand, ColumnMapResult>
    {

        private IBaseRepository<PhatchConfiguration> _baseRepositoryScolptioConfiguration;
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;

        public MapPropertiesColumnCommandHandler(IBaseRepository<PhatchConfiguration> baseRepositoryScolptioConfiguration
            , IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport)
        {
            _baseRepositoryScolptioConfiguration = baseRepositoryScolptioConfiguration;
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
        }

        public async Task<ColumnMapResult> Handle(MapPropertiesColumnCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            propertiesFileImport.ListProvider = request.ListProvider;
            propertiesFileImport.PropertyType = request.PropertyType;
            await _baseRepositoryPropertiesFileImport.UpdateAsync(propertiesFileImport);

            var dbColumnStatus = new List<DbColumnStatus>();
            var columnDisplayNames = new List<string>();

            if (propertiesFileImport.Extension.ToLower() == Const.PROPERTY_LIST_IMPORT_FILE_TYPE_CSV && propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
            {
                var fileContent = System.Text.Encoding.UTF8.GetString(propertiesFileImport.FileContent).Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                if (request.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_AGENT_PRO)
                {
                    columnDisplayNames = fileContent.First().Split(',').ToList();
                    var propertyConfig = await _baseRepositoryScolptioConfiguration.GetSingleAsync(x => x.ConfigKey == $"{Const.PROPERTY_LIST_PROVIDER_AGENT_PRO}_{Const.PROPERTY_LIST_IMPORT_FILE_TYPE_CSV}");

                    var propertyList = (IList)propertyConfig.ConfigValue;
                    foreach (dynamic data in propertyList)
                    {
                        IDictionary<string, object> propertyValues = data;
                        var colName = propertyValues["ColumnName"].ToString();
                        dbColumnStatus.Add(new DbColumnStatus
                        {
                            ColumnName = propertyValues["ColumnName"].ToString(),
                            DisplayName = propertyValues["DisplayName"].ToString(),
                            IsDefault = Const.DEFAULT_COLUMNS.Contains(colName, StringComparer.OrdinalIgnoreCase),
                            IsMapped = columnDisplayNames.Contains(propertyValues["DisplayName"].ToString())
                        });
                    }
                }
            }
            else if (propertiesFileImport.Extension.ToLower() == Const.PROPERTY_LIST_IMPORT_FILE_TYPE_CSV && propertiesFileImport.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_PRYCD)
            {
                var fileContent = System.Text.Encoding.UTF8.GetString(propertiesFileImport.FileContent).Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                if (request.ListProvider.ToLower() == Const.PROPERTY_LIST_PROVIDER_PRYCD)
                {
                    columnDisplayNames = fileContent.First().Split(',').ToList();
                    var propertyConfig = await _baseRepositoryScolptioConfiguration.GetSingleAsync(x => x.ConfigKey == $"{Const.PROPERTY_LIST_PROVIDER_PRYCD}_{Const.PROPERTY_LIST_IMPORT_FILE_TYPE_CSV}");

                    var propertyList = (IList)propertyConfig.ConfigValue;
                    foreach (dynamic data in propertyList)
                    {
                        IDictionary<string, object> propertyValues = data;
                        var colName = propertyValues["ColumnName"].ToString();
                        dbColumnStatus.Add(new DbColumnStatus
                        {
                            ColumnName = propertyValues["ColumnName"].ToString(),
                            DisplayName = propertyValues["DisplayName"].ToString(),
                            IsDefault = Const.DEFAULT_COLUMNS.Contains(colName, StringComparer.OrdinalIgnoreCase),
                            IsMapped = columnDisplayNames.Contains(propertyValues["DisplayName"].ToString())
                        }); ;
                    }
                }
            }

            var columnMapResult = new ColumnMapResult
            {
                CollumnsInCsv = columnDisplayNames,
                DbColumnsStatus = dbColumnStatus
            };

            return columnMapResult;
        }

    }
}
