ManagementController = function($scope, dataService) {
    "use strict";

    init();

    function init() {
        dataService.userGet()
            .then(function(response) {
                $scope.data = response.data.ListOfCalendarViewModels;
                $scope.roles = response.data.ListOfIdentityRoles;
                $scope.teams = response.data.ListOfTeams;
            });
    };

    $scope.deleteUser = function(user) {
        dataService.userDelete(user)
            .then(function() {
                dataService.userGet()
                    .then(function(response) {
                        $scope.data = response.data.ListOfCalendarViewModels;
                    });
            });
    };
    $scope.deleteTeam = function(team) {
        dataService.teamDelete(team)
            .then(function() {
                dataService.userGet()
                    .then(function(response) {
                        $scope.teams = response.data.ListOfTeams;
                    });
            });
    };
    $scope.userRegister = function(user) {
        dataService.userRegister(user)
            .then(function() {
                $scope.resetRegister();
                dataService.userGet()
                    .then(function(response) {
                        $scope.data = response.data.ListOfCalendarViewModels;
                    });
            });
    };

    $scope.teamRegister = function(team) {
        dataService.teamRegister(team)
            .then(function() {
                $scope.resetRegister();
                dataService.userGet()
                    .then(function(response) {
                        $scope.teams = response.data.ListOfTeams;
                    });
            });
    };

    $scope.updateUser = function (user) {
        user.Team = this.user.Team;
        dataService.employeeUpdate(user);
    };

    $scope.updateTeam = function(team) {
        dataService.teamUpdate(team);
    };

    $scope.userSetRole = function(user, role) {
        dataService.userSetRole(user, role)
            .then(function() {
                dataService.userGet()
                    .then(function(response) {
                        $scope.data = response.data.ListOfCalendarViewModels;
                    });
            });
    };

    $scope.teamSetEmployee = function(user, team) {
        dataService.teamSetEmployee(user, team)
            .then(function() {
                dataService.userGet()
                    .then(function(response) {
                        $scope.data = response.data.ListOfCalendarViewModels;
                    });
            });
    };
};
ManagementController.$inject = ["$scope", "dataService"];