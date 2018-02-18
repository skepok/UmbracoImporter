/// <reference path="UImporter.Models.ts" />

module UImporter.Controllers {
    export class UImporterCtrl {
        public UImporterService: UImporter.Services.UImporterService;

     


        constructor(public $scope: any, public uImporterService: UImporter.Services.UImporterService, public $filter: any) {
            this.UImporterService = uImporterService;
		}

        public Test() {

            var t = this.UImporterService.TestParse();
            console.log('t', t);
        }

	
    }
}