var DuplicateMemberManagementApp;
(function (DuplicateMemberManagementApp) {
    var Models;
    (function (Models) {
        var DuplicateBase = (function () {
            function DuplicateBase() {
                this.Id = "";
                this.Status = "";
                this.Name = "";
                this.DuplicateMemberIds = new Array();
                this.CreatedDate = new Date();
                this.ResolutionDate = new Date();
            }
            return DuplicateBase;
        }());
        Models.DuplicateBase = DuplicateBase;
    })(Models = DuplicateMemberManagementApp.Models || (DuplicateMemberManagementApp.Models = {}));
})(DuplicateMemberManagementApp || (DuplicateMemberManagementApp = {}));
//# sourceMappingURL=DuplicateMemberManagement.Models.js.map