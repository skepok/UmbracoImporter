/// <reference path="DuplicateMemberManagement.Interfaces.ts" />

module DuplicateMemberManagementApp.Services {
	export class DuplicateMemberManagementService {
        public $http: any;

        public constructor(httpService) {
            this.$http = httpService;
        }

		public GetDuplicates(): DuplicateMemberManagementApp.Interfaces.IPromise<Array<DuplicateMemberManagementApp.Models.DuplicateBase>> {
			return this.$http.get("backoffice/DuplicateMemberManagement/DuplicateMemberManagementApi/GetDuplicateMembers");
        }

		public UpdateDuplicates(duplicates: Array<DuplicateMemberManagementApp.Models.DuplicateBase>): DuplicateMemberManagementApp.Interfaces.IPromise<boolean> {
            return this.$http({
				url: "backoffice/DuplicateMemberManagement/DuplicateMemberManagementApi/SaveDuplicateMembers",
                method: "POST",
                data: duplicates,
                headers: {contenttype: JSON }
            });
        }

        public ExportToCsv(data: any): void {
            var jsonString = JSON.stringify(data);
            window.location.href = "/umbraco/backoffice/Api/ExportDataApi/ExportDuplicateMembers?exportData=" + jsonString;
        }
    }
}