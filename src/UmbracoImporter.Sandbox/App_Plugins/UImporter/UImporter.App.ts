declare var angular: any;
var uImporterApp = angular.module("umbraco");

uImporterApp.service("uImporterAppService", [
    "$http", UImporter.Services.UImporterService
]);

uImporterApp.controller("uImporterCtrl", [
    "$scope", "uImporterAppService", "$filter", UImporter.Controllers.UImporterCtrl
]);
