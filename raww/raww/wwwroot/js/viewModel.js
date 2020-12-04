define(['knockout'], function (ko) {
    //private part
    let currentComponent = ko.observable("home");
    let menuElements = ["Home", "Contact"];
    let selectedComponent = ko.observable('namesearch');


    let changeContent = () => {
        if (selectedComponent() === "namesearch") {
            selectedComponent('searchhistory');
        } else {
            selectedComponent('namesearch');
        }
    }

    //public part
    return {
        currentComponent,
        menuElements,
        selectedComponent,
        changeContent
    };
});