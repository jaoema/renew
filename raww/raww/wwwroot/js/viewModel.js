define(['knockout'], function (ko) {
    //private part

    //components with a title and a filename
    let nameSearchComp = { titleName: "Find Person", fileName: "namesearch" }
    let searchHistoryComp = { titleName: "Search History", fileName: "searchhistory" }
    let simpleSearchComp = {titleName: "Find Title", fileName: "simplesearch"}



    let currentComponent = ko.observable(nameSearchComp.fileName);
    let menuElements = [nameSearchComp, simpleSearchComp, searchHistoryComp];
    let selectedComponent = ko.observable('namesearch');

    let changeContent = element => {
        currentComponent(element.fileName.toLowerCase());
    }

    let isActive = element => {
        return element.fileName.toLowerCase() === currentComponent() ? "active" : "";
    }



    //public part
    return {
        currentComponent,
        menuElements,
        selectedComponent,
        changeContent,
        isActive
    };
});