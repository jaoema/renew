define(['knockout'], function (ko) {
    //private part

    //components with a title and a filename
    let nameSearchComp = { titleName: "Find Person", fileName: "namesearch" }
    let searchHistoryComp = { titleName: "Search History", fileName: "searchhistory" }



    let currentComponent = ko.observable(nameSearchComp.fileName);
    let menuElements = [nameSearchComp, searchHistoryComp];
    let selectedComponent = ko.observable('namesearch');

    let changeContent = element => {
        currentComponent(element.fileName.toLowerCase());
    }

    let isActive = element => {
        return element.fileName.toLowerCase() === currentComponent() ? "active" : "";
    }


    //let changeContent = () => {
    //    if (selectedComponent() === "namesearch") {
    //        selectedComponent('searchhistory');
    //    } else {
    //        selectedComponent('namesearch');
    //    }
    //}

    //public part
    return {
        currentComponent,
        menuElements,
        selectedComponent,
        changeContent,
        isActive
    };
});