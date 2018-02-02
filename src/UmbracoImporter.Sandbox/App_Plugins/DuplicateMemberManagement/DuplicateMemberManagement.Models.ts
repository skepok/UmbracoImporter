module DuplicateMemberManagementApp.Models {
	export class DuplicateBase {
		
		public Id: string = "";
		public MemberId: number;
        public CreatedDate: Date;
        public Status: string = "";
        public ResolutionDate: Date;
        public Name: string = "";
		public DuplicateMemberIds: Array<string>;


        constructor() {
			this.DuplicateMemberIds = new Array<string>();
            this.CreatedDate = new Date();
            this.ResolutionDate = new Date();
        }
    }
}