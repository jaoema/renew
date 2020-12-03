define(['knockout'], function (ko) {
    //private part
    let name = ko.observable("Peter");
    let names = ko.observableArray(
        [{ name: "Peter" }, { name: "Rasmus" }, { name: "Jakob" }]);

    fetch("api/")

    //public part
    return {
        name,
        names
    };
});