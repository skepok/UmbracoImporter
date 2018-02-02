/// <reference path="DuplicateMemberManagement.Models.ts" />

module DuplicateMemberManagementApp.Controllers {
	export class DuplicateMemberManagementCtrl {
		public DuplicateMemberManagementService: DuplicateMemberManagementApp.Services.DuplicateMemberManagementService;

		public DuplicateList: Array<DuplicateMemberManagementApp.Models.DuplicateBase>;

		public PagedDuplicateList: Array<DuplicateMemberManagementApp.Models.DuplicateBase>;

		public DirtyDuplicateList: Array<DuplicateMemberManagementApp.Models.DuplicateBase>;

        public StatusFilter: string;

        public DateFilterStart: string;
        public DateFilterEnd: string;

        public PaginationLength: number;
        public PaginationCurrentPage: number;
        public PaginationMaxPage: number;
        public Pagination: Array<number>;

		constructor(public $scope: any, public duplicateMemberManagementService: DuplicateMemberManagementApp.Services.DuplicateMemberManagementService, public $filter: any) {
			this.DuplicateMemberManagementService = duplicateMemberManagementService;
			this.DuplicateList = new Array<DuplicateMemberManagementApp.Models.DuplicateBase>();
			this.DirtyDuplicateList = new Array<DuplicateMemberManagementApp.Models.DuplicateBase>();
			this.PagedDuplicateList = new Array<DuplicateMemberManagementApp.Models.DuplicateBase>();
            this.StatusFilter = "unresolved";
            this.PaginationLength = 10;

            this.Filter();
		}

        public SaveChanges(): void {
            //send dirty entries to service to be updated
            this.DuplicateMemberManagementService.UpdateDuplicates(this.DirtyDuplicateList).then((response) => {
                //clear dirty list
				this.DirtyDuplicateList = new Array<DuplicateMemberManagementApp.Models.DuplicateBase>();
                //refresh list of duplicates to get fresh data
                var that = this;
                this.Filter();
            });         
        }

		public MarkAsDirty(duplicate: DuplicateMemberManagementApp.Models.DuplicateBase): void
        {
            if (this.DirtyDuplicateList.indexOf(duplicate) == -1) {
                this.DirtyDuplicateList.push(duplicate);
            }
        }

        public PaginationPrev(): void {
            if (this.PaginationCurrentPage > 1) {
                this.PaginationCurrentPage--;
                this.PaginationPopulatePage();
            }
        }

        public PaginationGoToPage(pageNumber: number): void {
            this.PaginationCurrentPage = pageNumber;
            this.PaginationPopulatePage();
        }
        
        public PaginationNext(): void {
            if (this.PaginationCurrentPage < this.PaginationMaxPage) {
                this.PaginationCurrentPage++
                this.PaginationPopulatePage();
            }
        }

        public PaginationPopulatePage(): void {
            //empty current page
			this.PagedDuplicateList = new Array<DuplicateMemberManagementApp.Models.DuplicateBase>();

            //populate new page
            for (var i = ((this.PaginationCurrentPage * this.PaginationLength) - (this.PaginationLength));
                i < this.DuplicateList.length && i < ((this.PaginationCurrentPage * this.PaginationLength) -1);
                i++)
            {
                this.PagedDuplicateList.push(this.DuplicateList[i]);
			}
        }

        public Sort(): void {
            alert('ad');
        }

        public Filter(): void {
            var that = this;
			this.DuplicateMemberManagementService.GetDuplicates().then((response) => {
                that.DuplicateList = response.data;
                switch (this.StatusFilter) {
                    case "unresolved":
                        that.DuplicateList = this.$filter('filter')(that.DuplicateList, { 'Status': '0' });
						if ((that.DateFilterStart == null || that.DateFilterEnd == null) && that.DuplicateList[that.DuplicateList.length - 1] != null) {
							that.DateFilterStart = that.GetDateString(new Date(that.DuplicateList[0].CreatedDate));
							that.DateFilterEnd = that.GetDateString(new Date(that.DuplicateList[that.DuplicateList.length - 1].CreatedDate));
                        }
                        break;
                    case "resolved":
                        that.DuplicateList = this.$filter('filter')(that.DuplicateList, { 'Status': '1' });
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

                var tempDupArray = new Array<DuplicateMemberManagementApp.Models.DuplicateBase>();

                var StartDate = new Date(that.DateFilterStart);
                var EndDate = new Date(that.DateFilterEnd);

                for (var i = 0; i < that.DuplicateList.length; i++) {

                    var CreatedDate = new Date(that.DuplicateList[i].CreatedDate);

                    if (CreatedDate >= StartDate && CreatedDate <= EndDate) {
                        tempDupArray.push(that.DuplicateList[i]);
                    }
                }
                that.DuplicateList = tempDupArray;

                this.PaginationCurrentPage = 1;
                this.PaginationMaxPage = Math.ceil(this.DuplicateList.length / 10);
                this.Pagination = new Array<number>();
                for (var i = 0; i < this.PaginationMaxPage; i++) {
                    this.Pagination.push(i + 1);
                }
                //populate page 1 by default
                this.PaginationPopulatePage();
            });
        }

        public ResetFilter(): void {
            this.DateFilterStart = null;
            this.DateFilterEnd = null;

            this.Filter();
        }

        public GetDateString(date: Date): string {
            return date.getFullYear().toString() + "-" + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + "-" + date.getDate();
        }

        public ExportToCsv() {
            var filteredData = new Array<string>();

            for (var i = 0; i < this.DuplicateList.length; i++) {
                filteredData.push(this.DuplicateList[i].Id.toString());
            }

            this.DuplicateMemberManagementService.ExportToCsv(filteredData);
        }
    }
}