﻿var app = angular.module("holApp", ["chart.js", "ui.bootstrap", "xeditable", "ngAnimate"]);

angular.module("holApp", ["chart.js", "ui.bootstrap", "xeditable", "ngAnimate"])
    .service("templateService", ["$http", "$compile", "$templateCache", templateService])
    .service("viewService", ["templateService", viewService])
    .service("helperService", [helperService])
    .service("notificationService", [notificationService])
    .service("userService", ["dataService", "loginService", userService])
    .service("loginService", ["$rootScope", loginService])
    .service("authService", ["$http", authService])
    .service("dataService", ["$http", "notificationService", dataService])

    .controller("MainController", MainController)
    .controller("CalendarController", CalendarController)
    .controller("LoginController", LoginController)
    .controller("ManagementController", ManagementController)

    .directive("calendar", calendarDirective)
    .directive("management", managementDirective)
    .directive("calendarcontrols", calendarControlsDirective)
    .directive("tooltip", tooltipDirective)
    .directive("infobox", infoBoxDirective)
    .directive("sidemenu", sideMenuDirective)

    .factory("templates", templates);