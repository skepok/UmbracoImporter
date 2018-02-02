/// <reference path="DuplicateMemberManagement.Models.ts" />
var DuplicateMemberManagementApp;
(function (DuplicateMemberManagementApp) {
    var Controllers;
    (function (Controllers) {
        var DuplicateMemberManagementCtrl = (function () {
            function DuplicateMemberManagementCtrl($scope, duplicateMemberManagementService, $filter) {
                this.$scope = $scope;
                this.duplicateMemberManagementService = duplicateMemberManagementService;
                this.$filter = $filter;
                this.DuplicateMemberManagementService = duplicateMemberManagementService;
                this.DuplicateList = new Array();
                this.DirtyDuplicateList = new Array();
                this.PagedDuplicateList = new Array();
                this.StatusFilter = "unresolved";
                this.PaginationLength = 10;
                this.Filter();
            }
            DuplicateMemberManagementCtrl.prototype.SaveChanges = function () {
                var _this = this;
                //send dirty entries to service to be updated
                this.DuplicateMemberManagementService.UpdateDuplicates(this.DirtyDuplicateList).then(function (response) {
                    //clear dirty list
                    _this.DirtyDuplicateList = new Array();
                    //refresh list of duplicates to get fresh data
                    var that = _this;
                    _this.Filter();
                });
            };
            DuplicateMemberManagementCtrl.prototype.MarkAsDirty = function (duplicate) {
                if (this.DirtyDuplicateList.indexOf(duplicate) == -1) {
                    this.DirtyDuplicateList.push(duplicate);
                }
            };
            DuplicateMemberManagementCtrl.prototype.PaginationPrev = function () {
                if (this.PaginationCurrentPage > 1) {
                    this.PaginationCurrentPage--;
                    this.PaginationPopulatePage();
                }
            };
            DuplicateMemberManagementCtrl.prototype.PaginationGoToPage = function (pageNumber) {
                this.PaginationCurrentPage = pageNumber;
                this.PaginationPopulatePage();
            };
            DuplicateMemberManagementCtrl.prototype.PaginationNext = function () {
                if (this.PaginationCurrentPage < this.PaginationMaxPage) {
                    this.PaginationCurrentPage++;
                    this.PaginationPopulatePage();
                }
            };
            DuplicateMemberManagementCtrl.prototype.PaginationPopulatePage = function () {
                //empty current page
                this.PagedDuplicateList = new Array();
                //populate new page
                for (var i = ((this.PaginationCurrentPage * this.PaginationLength) - (this.PaginationLength)); i < this.DuplicateList.length && i < ((this.PaginationCurrentPage * this.PaginationLength) - 1); i++) {
                    this.PagedDuplicateList.push(this.DuplicateList[i]);
                }
            };
            DuplicateMemberManagementCtrl.prototype.Sort = function () {
                alert('ad');
            };
            DuplicateMemberManagementCtrl.prototype.Filter = function () {
                var _this = this;
                var that = this;
                this.DuplicateMemberManagementService.GetDuplicates().then(function (response) {
                    that.DuplicateList = response.data;
                    switch (_this.StatusFilter) {
                        case "unresolved":
                            that.DuplicateList = _this.$filter('filter')(that.DuplicateList, { 'Status': '0' });
                            if ((that.DateFilterStart == null || that.DateFilterEnd == null) && that.DuplicateList[that.DuplicateList.length - 1] != null) {
                                that.DateFilterStart = that.GetDateString(new Date(that.DuplicateList[0].CreatedDate));
                                that.DateFilterEnd = that.GetDateString(new Date(that.DuplicateList[that.DuplicateList.length - 1].CreatedDate));
                            }
                            break;
                        case "resolved":
                            that.DuplicateList = _this.$filter('filter')(that.DuplicateList, { 'Status': '1' });
                            if ((that.DateFilterStart == null || that.DateFilterEnd == null) && that.DuplicateList[that.DuplicateList.length - 1] != null) {
                                that.DateFilterStart = that.GetDateString(new Date(that.DuplicateList[0].CreatedDate));
                                that.DateFilterEnd = that.GetDateString(new Date(that.DuplicateList[that.DuplicateList.length - 1].CreatedDate));
                            }
                            break;
                        case "all":
                            if (that.DateFilterStart == null || that.DateFilterEnd == null) {
                                that.DateFilterStart = that.GetDateString(new Date(that.DuplicateList[0].CreatedDate));
                                that.DateFilterEnd = that.GetDateString(new Date(that.DuplicateList[that.DuplicateList.length - 1].CreatedDate));
                            }
                            break;
                    }
                    var tempDupArray = new Array();
                    var StartDate = new Date(that.DateFilterStart);
                    var EndDate = new Date(that.DateFilterEnd);
                    for (var i = 0; i < that.DuplicateList.length; i++) {
                        var CreatedDate = new Date(that.DuplicateList[i].CreatedDate);
                        if (CreatedDate >= StartDate && CreatedDate <= EndDate) {
                            tempDupArray.push(that.DuplicateList[i]);
                        }
                    }
                    that.DuplicateList = tempDupArray;
                    _this.PaginationCurrentPage = 1;
                    _this.PaginationMaxPage = Math.ceil(_this.DuplicateList.length / 10);
                    _this.Pagination = new Array();
                    for (var i = 0; i < _this.PaginationMaxPage; i++) {
                        _this.Pagination.push(i + 1);
                    }
                    //populate page 1 by default
                    _this.PaginationPopulatePage();
                });
            };
            DuplicateMemberManagementCtrl.prototype.ResetFilter = function () {
                this.DateFilterStart = null;
                this.DateFilterEnd = null;
                this.Filter();
            };
            DuplicateMemberManagementCtrl.prototype.GetDateString = function (date) {
                return date.getFullYear().toString() + "-" + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + "-" + date.getDate();
            };
            DuplicateMemberManagementCtrl.prototype.ExportToCsv = function () {
                var filteredData = new Array();
                for (var i = 0; i < this.DuplicateList.length; i++) {
                    filteredData.push(this.DuplicateList[i].Id.toString());
                }
                this.DuplicateMemberManagementService.ExportToCsv(filteredData);
            };
            return DuplicateMemberManagementCtrl;
        }());
        Controllers.DuplicateMemberManagementCtrl = DuplicateMemberManagementCtrl;
    })(Controllers = DuplicateMemberManagementApp.Controllers || (DuplicateMemberManagementApp.Controllers = {}));
})(DuplicateMemberManagementApp || (DuplicateMemberManagementApp = {}));
//# sourceMappingURL=DuplicateMemberManagement.Controllers.js.map