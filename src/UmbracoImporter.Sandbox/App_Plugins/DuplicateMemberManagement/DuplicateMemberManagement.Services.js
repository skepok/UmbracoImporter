/// <reference path="DuplicateMemberManagement.Interfaces.ts" />
var DuplicateMemberManagementApp;
(function (DuplicateMemberManagementApp) {
    var Services;
    (function (Services) {
        var DuplicateMemberManagementService = (function () {
            function DuplicateMemberManagementService(httpService) {
                this.$http = httpService;
            }
            DuplicateMemberManagementService.prototype.GetDuplicates = function () {
                return this.$http.get("backoffice/DuplicateMemberManagement/DuplicateMemberManagementApi/GetDuplicateMembers");
            };
            DuplicateMemberManagementService.prototype.UpdateDuplicates = function (duplicates) {
                return this.$http({
                    url: "backoffice/DuplicateMemberManagement/DuplicateMemberManagementApi/SaveDuplicateMembers",
                    method: "POST",
                    data: duplicates,
                    headers: { contenttype: JSON }
                });
            };
            DuplicateMemberManagementService.prototype.ExportToCsv = function (data) {
                var jsonString = JSON.stringify(data);
                window.location.href = "/umbraco/backoffice/Api/ExportDataApi/ExportDuplicateMembers?exportData=" + jsonString;
            };
            return DuplicateMemberManagementService;
        }());
        Services.DuplicateMemberManagementService = DuplicateMemberManagementService;
    })(Services = DuplicateMemberManagementApp.Services || (DuplicateMemberManagementApp.Services = {}));
})(DuplicateMemberManagementApp || (DuplicateMemberManagementApp = {}));
//# sourceMappingURL=DuplicateMemberManagement.Services.js.map