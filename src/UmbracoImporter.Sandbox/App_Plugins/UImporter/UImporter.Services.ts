/// <reference path="UImporter.Interfaces.ts" />

module UImporter.Services {
    export class UImporterService {
        public $http: any;

        public constructor(httpService) {
            this.$http = httpService;
        }

        public TestParse(): UImporter.Interfaces.IPromise<UImporter.Models.ImportNode> {
            return this.$http.get("/umbraco/backoffice/api/UImporterApi/MockImportJson");
        }

    //    public UpdateDuplicates(duplicates: Array<UImporterApp.Models.DuplicateBase>): DuplicateMemberManagementApp.Interfaces.IPromise<boolean> {
    //        return this.$http({
				//url: "backoffice/DuplicateMemberManagement/DuplicateMemberManagementApi/SaveDuplicateMembers",
    //            method: "POST",
    //            data: duplicates,
    //            headers: {contenttype: JSON }
    //        });
    //    }

    //    public ExportToCsv(data: any): void {
    //        var jsonString = JSON.stringify(data);
    //        window.location.href = "/umbraco/backoffice/Api/ExportDataApi/ExportDuplicateMembers?exportData=" + jsonString;
    //    }
    }
}