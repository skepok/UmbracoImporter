var duplicateMemberManagementApp = angular.module("umbraco");

duplicateMemberManagementApp.service("duplicateMemberManagementAppService", [
    "$http", DuplicateMemberManagementApp.Services.DuplicateMemberManagementService
]);

duplicateMemberManagementApp.controller("duplicateMemberManagementCtrl", [
	"$scope", "duplicateMemberManagementAppService", "$filter", DuplicateMemberManagementApp.Controllers.DuplicateMemberManagementCtrl
]);
